using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    #region �ϐ�
    PlayerInput _input;
    Player _player;
    UiManager _uiManager;
    TacticSlot _tacticSlot;
    ItemSlot _itemSlot;
    PauseManager _pauseManager;
    CameraChange _cameraChange;
    /// <summary>���w�����̒�~�p</summary>
    bool _actionBool = false;
    /// <summary> �A�C�e���X���b�g��\�����Ă��邩���肷��</summary>
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

        // �f���Q�[�g�o�^
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

        // �f���Q�[�g�o�^����
        _input.onActionTriggered -= OnScrollWheel;
        _input.onActionTriggered -= OnSlotChange;
        _input.onActionTriggered -= OnFire;
        _input.onActionTriggered -= OnFire2;
        _input.onActionTriggered -= OnLockLeft;
        _input.onActionTriggered -= OnLockRight;
        _input.onActionTriggered -= OnRetreat;
    }

    /// <summary>�X���b�g�̃X�N���[��</summary>
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

    /// <summary>�X���b�g�̐؂�ւ�</summary>
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

    /// <summary>�A�C�e���̎g�p����̎��s�Ȃǂ𔻒�</summary>
    private void OnFire(InputAction.CallbackContext context) 
    {
        // ���N���b�N��performed�R�[���o�b�N�����󂯎��
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

    /// <summary>���b�N�I������</summary>
    /// <param name="context"></param>
    private void OnFire2(InputAction.CallbackContext context)
    {
        // �z�C�[���N���b�N��performed�R�[���o�b�N�����󂯎��
        if (context.action.name == "Fire2" && context.performed)
        {
            _cameraChange.LockOnOff();
        }
    }

    /// <summary>�^�[�Q�b�g��O�̃����X�^�[�ɕύX����</summary>
    private void OnLockLeft(InputAction.CallbackContext context)
    {
        if (context.action.name == "LockLeft" && context.performed && _cameraChange._isLockOn)
        {
            _cameraChange.LockOnChangeLeft();
        }
    }

    /// <summary>�^�[�Q�b�g�����̃����X�^�[�ɕύX����</summary>
    private void OnLockRight(InputAction.CallbackContext context)
    {
        if (context.action.name == "LockRight" && context.performed && _cameraChange._isLockOn)
        {
            _cameraChange.LockOnChangeRight();
        }
    }

    /// <summary>�ދp�̔���<summary>
    private void OnRetreat(InputAction.CallbackContext context) 
    {
        //���R���g���[���L�[���������Ƃ��̂�
        if (context.action.name == "Retreat" && context.performed)
        {
            Retreat();
        }
    }

    /// <summary>�\������X���b�g��؂�ւ���</summary>
    private void SlotChange(bool itemSlotactive)
    {
        if (_itemSlotBool != itemSlotactive)
        {
            _tacticSlot.TacticsSlotActiveChange();
            _itemSlot.ItemSlotActiveChange();
            _itemSlotBool = itemSlotactive;
        }
    }

    /// <summary>�����w������</summary>
    void ChangeTactics()
    {
        int index = _tacticSlot._selectIndex;

        _player.ConductTactics(index);
    }

    /// <summary>�莝���̃A�C�e�����g�p���� </summary>
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

    /// <summary>�|�[�Y���͑���ł��Ȃ��悤�ɂ��Ă���</summary>
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
