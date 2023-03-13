using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    PlayerInput _input;

    PauseManager _pauseManager;

    /// <summary>作戦指示中の停止用</summary>
    private bool _actionBool = false;

    /// <summary> アイテムスロットを表示しているか判定する</summary>
    bool _itemSlotBool = false;

    Player _player => Player.Instance;

    UiManager _uiManager => UiManager.Instance;

    TacticSlot _tacticSlot => _uiManager.TacticSlot;

    ItemSlot _itemSlot => _uiManager.ItemSlot;

    CameraChange _cameraChange => GameManager.Instance.CameraChange;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _pauseManager = _uiManager.PauseManager;
    }

    private void OnEnable()
    {
        if (_input == null) return;

        // デリゲート登録
        _pauseManager.onCommandMenu += PauseCommand;
        _input.onActionTriggered += OnScrollWheel;
        _input.onActionTriggered += OnSlotChange;
        _input.onActionTriggered += OnFire ;
        _input.onActionTriggered += OnFire3;
        _input.onActionTriggered += OnLockLeft;
        _input.onActionTriggered += OnLockRight;
        _input.onActionTriggered += OnRetreat;
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
        _input.onActionTriggered -= OnRetreat;
    }

    //スロットのスクロール
    private void OnScrollWheel(InputAction.CallbackContext context)
    {
        if (context.action.name != "ScrollWheel") { return; }
            float scrollWheel = context.action.ReadValue<Vector2>().y;

            if (!_itemSlotBool)
            {
                if (scrollWheel > 0) { _tacticSlot.WheelUp(); }
                else if (scrollWheel < 0) { _tacticSlot.WheelDown(); }
            }
            else
            {
                if (scrollWheel > 0) { _itemSlot.WheelUp(); }
                else if (scrollWheel < 0) { _itemSlot.WheelDown(); }
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
            _cameraChange.LockOnOff();
        }
    }

    private void OnLockLeft(InputAction.CallbackContext context)
    {
        if (context.action.name == "LockLeft" && context.performed && _cameraChange._isLockOn)
        {
            _cameraChange.LockOnChangeLeft();
        }
    }

    private void OnLockRight(InputAction.CallbackContext context)
    {
        if (context.action.name == "LockRight" && context.performed && _cameraChange._isLockOn)
        {
            _cameraChange.LockOnChangeRight();
        }
    }

    private void OnRetreat(InputAction.CallbackContext context) 
    {
        //左コントロールキーを押したときのみ
        if (context.action.name == "Retreat" && context.performed)
        {
            Retreat();
        }
    }

    /// <summary>表示するスロットを切り替える</summary>
    private void SlotChange(bool itemSlotactive)
    {
        if (_itemSlotBool != itemSlotactive)
        {
            _tacticSlot.TacticsSlotActiveChange();
            _itemSlot.ItemSlotActiveChange();
            _itemSlotBool = itemSlotactive;
        }
    }

    /// <summary>作戦を指示する</summary>
    void ChangeTactics()
    {
        int index = _tacticSlot._selectIndex;

        StartCoroutine(ActionStop(3.0f));

        _player.ConductTactics(index);
    }

    /// <summary>手持ちのアイテムを使用する </summary>
    private void UseItems()
    {
        int index = _itemSlot._selectIndex;

        StartCoroutine(ActionStop(3.0f));

        _player.UseItems(index);
    }

    private void Retreat() 
    {
        _player.Retreat();

        _uiManager.Retreat();
    }

    private IEnumerator ActionStop(float waitTime)
    {
        _actionBool = true;
        yield return new WaitForSeconds(waitTime);
        _actionBool = false;
    }

    void PauseCommand(bool onPause)
    {
        if (onPause)
        {
            OnDisable();
        }
        else
        {
            OnEnable();
        }
    }

    void Pause() //停止処理
    {
        _input.onActionTriggered -= OnScrollWheel;
        _input.onActionTriggered -= OnSlotChange;
        _input.onActionTriggered -= OnFire;
        _input.onActionTriggered -= OnFire3;
        _input.onActionTriggered -= OnLockLeft;
        _input.onActionTriggered -= OnLockRight;
        _input.onActionTriggered -= OnRetreat;
    }

    void Resum() //再開
    {
        _input.onActionTriggered += OnScrollWheel;
        _input.onActionTriggered += OnSlotChange;
        _input.onActionTriggered += OnFire;
        _input.onActionTriggered += OnFire3;
        _input.onActionTriggered += OnLockLeft;
        _input.onActionTriggered += OnLockRight;
        _input.onActionTriggered += OnRetreat;
    }
}
