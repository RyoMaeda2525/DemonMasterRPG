using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    Player _player = null;

    PlayerInput _input;

    /// <summary>作戦指示中の停止用</summary>
    private bool _actionBool = false;

    /// <summary> アイテムスロットを表示しているか判定する</summary>
    bool _itemSlotBool = false;

    private GameManager _gameManager;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }


    private void OnEnable()
    {
        if (_input == null) return;

        // デリゲート登録
        _input.onActionTriggered += OnFire3;
        _input.onActionTriggered += OnScrollWheel;
    }

    private void OnDisable()
    {
        if (_input == null) return;

        // デリゲート登録解除
        _input.onActionTriggered -= OnFire3;
        _input.onActionTriggered -= OnScrollWheel;
    }

    void FixedUpdate()
    {
        if (CameraChange.Instance._isLockOn)
        {
            if (Input.GetKeyDown(KeyCode.Q)) { CameraChange.Instance.LockOnChangeLeft(); }
            else if (Input.GetKeyDown(KeyCode.E)) { CameraChange.Instance.LockOnChangeRight(); }
        }

        if (Input.GetButton("Jump"))
        {
            SlotChange(true);
        }
        else
        {
            SlotChange(false);
        }

        if (!_itemSlotBool)
        {
            if (!_actionBool && Input.GetKeyDown(KeyCode.F))
            {
                ChangeTactics();
            }
        }
        else
        {
            if (!_actionBool && Input.GetKeyDown(KeyCode.F))
            {
                UseItems();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            GameManager.Instance.MenuOpenOrClose();
            ActionStop(0.5f);
        }
    }

    //スロットのスクロール
    private void OnScrollWheel(InputAction.CallbackContext context)
    {
        if (context.action.name != "ScrollWheel") { return; }
            float scrollWheel = context.action.ReadValue<Vector2>().y;

            if (!_itemSlotBool)
            {
                if (scrollWheel > 0) { GameManager.Instance.TacticSlot.WheelUp(); }
                else if (scrollWheel < 0) { GameManager.Instance.TacticSlot.WheelDown(); }
            }
            else
            {
                if (scrollWheel > 0) { ItemSlot.Instance.WheelUp(); }
                else if (scrollWheel < 0) { ItemSlot.Instance.WheelDown(); }
            }
    }

    //ロックオン処理
    private void OnFire3(InputAction.CallbackContext context)
    {
        // ホイールクリックとperformedコールバックだけ受け取る
        if (context.action.name == "Fire3" && context.performed)
        {
            CameraChange.Instance.LockOnOff();
        }
    }


    /// <summary>表示するスロットを切り替える</summary>
    private void SlotChange(bool itemSlotactive)
    {
        if (_itemSlotBool != itemSlotactive)
        {
            GameManager.Instance.TacticSlot.TacticsSlotActiveChange();
            ItemSlot.Instance.ItemSlotActiveChange();
            _itemSlotBool = itemSlotactive;
        }
    }

    /// <summary>作戦を指示する</summary>
    void ChangeTactics()
    {
        int index = GameManager.Instance.TacticSlot._selectIndex;

        StartCoroutine(ActionStop(3.0f));

        _player.ConductTactics(index);
    }

    /// <summary>手持ちのアイテムを使用する </summary>
    private void UseItems()
    {

        int index = ItemSlot.Instance._selectIndex;

        StartCoroutine(ActionStop(3.0f));

        _player.UseItems(index);
    }

    private IEnumerator ActionStop(float waitTime)
    {
        _actionBool = true;
        yield return new WaitForSeconds(waitTime);
        _actionBool = false;
    }
}
