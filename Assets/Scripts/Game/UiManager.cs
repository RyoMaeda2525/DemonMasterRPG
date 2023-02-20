using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : SingletonMonoBehaviour<UiManager>
{
    [SerializeField]
    private Text _gameOverText = default;
    [SerializeField]
    private Text _retreatText = default;
    [SerializeField]
    private GameObject _menuPanel = null;
    [SerializeField]
    private TacticSlot _tacticSlot = null;
    [SerializeField]
    private ItemSlot _itemSlot = null;
    [SerializeField]
    MonsterPanelManger _monsterPanel;

    public TacticSlot TacticSlot => _tacticSlot;
    public ItemSlot ItemSlot => _itemSlot;
    public MonsterPanelManger MonsterPanel => _monsterPanel;

    private void Start()
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

    public void MenuOpenOrClose()
    {
        //�P�ޒ��͊J���Ȃ��悤�ɂ���
        if (_menuPanel != null && !_retreatText.enabled)
        {
            if (_menuPanel.activeSelf)
            {
                _menuPanel.SetActive(!_menuPanel.activeSelf);
            }
            else
            {
                _menuPanel.SetActive(!_menuPanel.activeSelf);
                ItemInventoryManager.Instance.OpenOrCloseInventory();
            }
        }
    }

    /// <summary>���������X�^�[���S�ł�����Ăяo��</summary>
    public void GameOver()
    {
        _gameOverText.gameObject.SetActive(true);
    }

    public void Retreat() 
    {
        _retreatText.enabled = !_retreatText.enabled;
    }
}
