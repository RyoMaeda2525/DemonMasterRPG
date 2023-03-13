using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    PlayerInput _input;

    PauseManager _pauseManager;

    /// <summary>���w�����̒�~�p</summary>
    private bool _actionBool = false;

    /// <summary> �A�C�e���X���b�g��\�����Ă��邩���肷��</summary>
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

        // �f���Q�[�g�o�^
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

        // �f���Q�[�g�o�^����
        _input.onActionTriggered -= OnScrollWheel;
        _input.onActionTriggered -= OnSlotChange;
        _input.onActionTriggered -= OnFire;
        _input.onActionTriggered -= OnFire3;
        _input.onActionTriggered -= OnLockLeft;
        _input.onActionTriggered -= OnLockRight;
        _input.onActionTriggered -= OnRetreat;
    }

    //�X���b�g�̃X�N���[��
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

    //���b�N�I������
    private void OnFire3(InputAction.CallbackContext context)
    {
        // �z�C�[���N���b�N��performed�R�[���o�b�N�����󂯎��
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

        StartCoroutine(ActionStop(3.0f));

        _player.ConductTactics(index);
    }

    /// <summary>�莝���̃A�C�e�����g�p���� </summary>
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

    void Pause() //��~����
    {
        _input.onActionTriggered -= OnScrollWheel;
        _input.onActionTriggered -= OnSlotChange;
        _input.onActionTriggered -= OnFire;
        _input.onActionTriggered -= OnFire3;
        _input.onActionTriggered -= OnLockLeft;
        _input.onActionTriggered -= OnLockRight;
        _input.onActionTriggered -= OnRetreat;
    }

    void Resum() //�ĊJ
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
