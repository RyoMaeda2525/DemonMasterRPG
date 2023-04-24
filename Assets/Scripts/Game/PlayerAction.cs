using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    #region 変数
    PlayerInput _input;
    Player _player;
    UiManager _uiManager;
    TacticSlot _tacticSlot;
    ItemSlot _itemSlot;
    PauseManager _pauseManager;
    CameraChange _cameraChange;
    /// <summary>作戦指示中の停止用</summary>
    bool _actionBool = false;
    /// <summary> アイテムスロットを表示しているか判定する</summary>
    bool _itemSlotBool = false;
    #endregion

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _player = Player.Instance;
        _uiManager = UiManager.Instance;
        _tacticSlot = _uiManager.TacticSlot;
        _itemSlot = _uiManager.ItemSlot;
        _pauseManager = GameManager.Instance.PauseManager;
        _cameraChange = GameManager.Instance.CameraChange;
    }

    private void OnEnable()
    {
        if (_input == null) return;

        // デリゲート登録
        _pauseManager.onCommandMenu += PauseCommand;
        _input.onActionTriggered += OnScrollWheel;
        _input.onActionTriggered += OnSlotChange;
        _input.onActionTriggered += OnFire ;
        _input.onActionTriggered += OnFire2;
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
        _input.onActionTriggered -= OnFire2;
        _input.onActionTriggered -= OnLockLeft;
        _input.onActionTriggered -= OnLockRight;
        _input.onActionTriggered -= OnRetreat;
    }

    /// <summary>スロットのスクロール</summary>
    private void OnScrollWheel(InputAction.CallbackContext context)
    {
        if (context.action.name == "ScrollWheel" && context.performed)
        {
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
    }

    /// <summary>スロットの切り替え</summary>
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

    /// <summary>アイテムの使用や作戦の実行などを判定</summary>
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

    /// <summary>ロックオン処理</summary>
    /// <param name="context"></param>
    private void OnFire2(InputAction.CallbackContext context)
    {
        // ホイールクリックとperformedコールバックだけ受け取る
        if (context.action.name == "Fire2" && context.performed)
        {
            _cameraChange.LockOnOff();
        }
    }

    /// <summary>ターゲットを前のモンスターに変更する</summary>
    private void OnLockLeft(InputAction.CallbackContext context)
    {
        if (context.action.name == "LockLeft" && context.performed && _cameraChange._isLockOn)
        {
            _cameraChange.LockOnChangeLeft();
        }
    }

    /// <summary>ターゲットを次のモンスターに変更する</summary>
    private void OnLockRight(InputAction.CallbackContext context)
    {
        if (context.action.name == "LockRight" && context.performed && _cameraChange._isLockOn)
        {
            _cameraChange.LockOnChangeRight();
        }
    }

    /// <summary>退却の判定<summary>
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

        _player.ConductTactics(index);
    }

    /// <summary>手持ちのアイテムを使用する </summary>
    private void UseItems()
    {
        int index = _itemSlot._selectIndex;

        _player.UseItems(index);
    }

    private void Retreat() 
    {
        _player.Retreat();

        _uiManager.Retreat();
    }

    /// <summary>ポーズ中は操作できないようにしている</summary>
    void PauseCommand(bool onPause)
    {
        if (onPause)
        {
            _input.SwitchCurrentActionMap("System");
        }
        else
        {
            _input.SwitchCurrentActionMap("Player");
        }
    }
}
