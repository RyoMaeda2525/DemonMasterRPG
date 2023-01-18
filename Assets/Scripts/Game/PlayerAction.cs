using System.Collections;
using TMPro.EditorUtilities;
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

    GameManager GameManager => GameManager.Instance;

    CameraChange CameraChange => GameManager.CameraChange;
    
    ItemSlot ItemSlot => GameManager.ItemSlot;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        if (_input == null) return;

        // デリゲート登録
        _input.onActionTriggered += OnScrollWheel;
        _input.onActionTriggered += OnSlotChange;
        _input.onActionTriggered += OnFire ;
        _input.onActionTriggered += OnFire3;
        _input.onActionTriggered += OnLockLeft;
        _input.onActionTriggered += OnLockRight;
    }

    private void OnDisable()
    {
        if (_input == null) return;

        // デリゲート登録解除
        _input.onActionTriggered -= OnScrollWheel;
        _input.onActionTriggered -= OnSlotChange;
        _input.onActionTriggered -= OnFire;
        _input.onActionTriggered -= OnFire3;
        _input.onActionTriggered -= OnLockLeft;
        _input.onActionTriggered -= OnLockRight;
    }

    //スロットのスクロール
    private void OnScrollWheel(InputAction.CallbackContext context)
    {
        if (context.action.name != "ScrollWheel") { return; }
            float scrollWheel = context.action.ReadValue<Vector2>().y;

            if (!_itemSlotBool)
            {
                if (scrollWheel > 0) { GameManager.TacticSlot.WheelUp(); }
                else if (scrollWheel < 0) { GameManager.TacticSlot.WheelDown(); }
            }
            else
            {
                if (scrollWheel > 0) { ItemSlot.WheelUp(); }
                else if (scrollWheel < 0) { ItemSlot.WheelDown(); }
            }
    }

    private void OnSlotChange(InputAction.CallbackContext context) 
    {
        if(context.action.name != "SlotChange") { return; }

        if (context.started)
        {
            SlotChange(true);
        }
        else if(context.canceled)
        {
            SlotChange(false);
        }
    }

    private void OnFire(InputAction.CallbackContext context) 
    {
        // 左クリックとperformedコールバックだけ受け取る
        if (context.action.name == "Fire" && context.performed && !_actionBool)
        {
            if (_itemSlotBool)
            {
                UseItems();
            }
            else
            {
                ChangeTactics();
            }
        }
    }

    //ロックオン処理
    private void OnFire3(InputAction.CallbackContext context)
    {
        // ホイールクリックとperformedコールバックだけ受け取る
        if (context.action.name == "Fire3" && context.performed)
        {
            CameraChange.LockOnOff();
        }
    }

    private void OnLockLeft(InputAction.CallbackContext context)
    {
        if (context.action.name == "LockLeft" && context.performed && CameraChange._isLockOn)
        {
            CameraChange.LockOnChangeLeft();
        }
    }

    private void OnLockRight(InputAction.CallbackContext context)
    {
        if (context.action.name == "LockRight" && context.performed && CameraChange._isLockOn)
        {
            CameraChange.LockOnChangeRight();
        }
    }

    /// <summary>表示するスロットを切り替える</summary>
    private void SlotChange(bool itemSlotactive)
    {
        if (_itemSlotBool != itemSlotactive)
        {
            GameManager.Instance.TacticSlot.TacticsSlotActiveChange();
            ItemSlot.ItemSlotActiveChange();
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

        int index = ItemSlot._selectIndex;

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
