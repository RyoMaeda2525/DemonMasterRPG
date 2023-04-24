using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UiManager : SingletonMonoBehaviour<UiManager>
{
    #region �ϐ�
    [SerializeField]
    Text _gameOverText = default;
    [SerializeField]
    Text _retreatText = default;
    [SerializeField]
    GameObject _menuPanel = null;
    [SerializeField]
    TacticSlot _tacticSlot = null;
    [SerializeField]
    ItemSlot _itemSlot = null;
    [SerializeField]
    MonsterPanelManger _monsterPanel;
    [SerializeField]
    PauseManager _pauseManager;
    [SerializeField]
    CinemachineInputProvider _cinemachineInput;
    [SerializeField]
    ItemInventoryManager _inventoryManager;
    #endregion

    #region �v���p�e�B
    /// <summary>�������o�A�g�p���邽�߂̃X���b�g</summary>
    public TacticSlot TacticSlot => _tacticSlot;
    /// <summary>�A�C�e�������o�A�g�p���邽�߂̃X���b�g</summary>
    public ItemSlot ItemSlot => _itemSlot;
    /// <summary>���������X�^�[�̗̑͂Ȃǂ�\�����邽�߂̃p�l��</summary>
    public MonsterPanelManger MonsterPanel => _monsterPanel;
    /// <summary>�A�C�e�����Ǘ����郁�j��-�p�l��</summary>
    public ItemInventoryManager InventoryManager => _inventoryManager;
    #endregion

    void Start()
    {
        _retreatText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_menuPanel.activeSelf)
        {
            Cursor.visible = true;
        }
        else { Cursor.visible = false; }
    }

    /// <summary>���j��-��ʂ�ON�EOFF������</summary>
    public void MenuOpenOrClose()
    {
        //�P�ޒ��͊J���Ȃ��悤�ɂ���
        if (_menuPanel != null && !_retreatText.enabled)
        {
            if (!_menuPanel.activeSelf)
            {
                _pauseManager.OnCommandMenu(!_menuPanel.activeSelf);
                _menuPanel.SetActive(true);
                _cinemachineInput.enabled = false;
            }
            else
            {
                _menuPanel.SetActive(!_menuPanel.activeSelf);
                _pauseManager.OnCommandMenu(false);
                _cinemachineInput.enabled = true;
            }
        }
    }

    /// <summary>���������X�^�[���S�ł�����Ăяo��</summary>
    public void GameOver()
    {
        _gameOverText.gameObject.SetActive(true);
    }

    /// <summary>�ދp���Ƀe�L�X�g��\������</summary>
    public void Retreat()
    {
        _retreatText.enabled = !_retreatText.enabled;
    }
}
