using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    }

    private void OnDisable()
    {
        if (_input == null) return;

        // デリゲート登録解除
        _input.onActionTriggered -= OnFire3;
    }

    void FixedUpdate()
    {
        float scrollWheel = Input.GetAxis("MouseScrollWheel");

        if (Input.GetButtonDown("Fire3"))
        {
            
        }
        else if (CameraChange.Instance._isLockOn)
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
            else if (scrollWheel > 0) { TacticSlot.Instance.WheelUp(); }
            else if (scrollWheel < 0) { TacticSlot.Instance.WheelDown(); }
        }
        else
        {
            if (!_actionBool && Input.GetKeyDown(KeyCode.F))
            {
                UseItems();
            }
            else if (scrollWheel > 0) { ItemSlot.Instance.WheelUp(); }
            else if (scrollWheel < 0) { ItemSlot.Instance.WheelDown(); }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            GameManager.Instance.MenuOpenOrClose();
            ActionStop(0.5f);
        }
    }

    //ロックオン処理
    public void OnFire3(InputAction.CallbackContext context)
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
            TacticSlot.Instance.TacticsSlotActiveChange();
            ItemSlot.Instance.ItemSlotActiveChange();
            _itemSlotBool = itemSlotactive;
        }
    }

    /// <summary>作戦を指示する</summary>
    void ChangeTactics()
    {
        int index = TacticSlot.Instance._selectIndex;

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
