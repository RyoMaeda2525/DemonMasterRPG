using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    Player _player = null;

    PlayerInput _input;

    /// <summary>���w�����̒�~�p</summary>
    private bool _actionBool = false;

    /// <summary> �A�C�e���X���b�g��\�����Ă��邩���肷��</summary>
    bool _itemSlotBool = false;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        if (_input == null) return;

        // �f���Q�[�g�o�^
        _input.onActionTriggered += OnScrollWheel;
        _input.onActionTriggered += OnSlotChange;
        _input.onActionTriggered += OnFire ;
        _input.onActionTriggered += OnFire3;
    }

    private void OnDisable()
    {
        if (_input == null) return;

        // �f���Q�[�g�o�^����
        _input.onActionTriggered -= OnScrollWheel;
        _input.onActionTriggered -= OnSlotChange;
        _input.onActionTriggered -= OnFire;
        _input.onActionTriggered -= OnFire3;
    }

    void FixedUpdate()
    {
        if (CameraChange.Instance._isLockOn)
        {
            if (Input.GetKeyDown(KeyCode.Q)) { CameraChange.Instance.LockOnChangeLeft(); }
            else if (Input.GetKeyDown(KeyCode.E)) { CameraChange.Instance.LockOnChangeRight(); }
        }

        
        
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            GameManager.Instance.MenuOpenOrClose();
            StartCoroutine(ActionStop(0.5f));
        }
    }

    //�X���b�g�̃X�N���[��
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
            CameraChange.Instance.LockOnOff();
        }
    }


    /// <summary>�\������X���b�g��؂�ւ���</summary>
    private void SlotChange(bool itemSlotactive)
    {
        if (_itemSlotBool != itemSlotactive)
        {
            GameManager.Instance.TacticSlot.TacticsSlotActiveChange();
            ItemSlot.Instance.ItemSlotActiveChange();
            _itemSlotBool = itemSlotactive;
        }
    }

    /// <summary>�����w������</summary>
    void ChangeTactics()
    {
        int index = GameManager.Instance.TacticSlot._selectIndex;

        StartCoroutine(ActionStop(3.0f));

        _player.ConductTactics(index);
    }

    /// <summary>�莝���̃A�C�e�����g�p���� </summary>
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
