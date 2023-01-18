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

    /// <summary>���w�����̒�~�p</summary>
    private bool _actionBool = false;

    /// <summary> �A�C�e���X���b�g��\�����Ă��邩���肷��</summary>
    bool _itemSlotBool = false;

    private GameManager _gameManager;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }


    private void OnEnable()
    {
        if (_input == null) return;

        // �f���Q�[�g�o�^
        _input.onActionTriggered += OnFire3;
        _input.onActionTriggered += OnScrollWheel;
    }

    private void OnDisable()
    {
        if (_input == null) return;

        // �f���Q�[�g�o�^����
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
