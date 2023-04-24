using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UiManager : SingletonMonoBehaviour<UiManager>
{
    #region 変数
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

    #region プロパティ
    /// <summary>作戦を視覚、使用するためのスロット</summary>
    public TacticSlot TacticSlot => _tacticSlot;
    /// <summary>アイテムを視覚、使用するためのスロット</summary>
    public ItemSlot ItemSlot => _itemSlot;
    /// <summary>味方モンスターの体力などを表示するためのパネル</summary>
    public MonsterPanelManger MonsterPanel => _monsterPanel;
    /// <summary>アイテムを管理するメニュ-パネル</summary>
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

    /// <summary>メニュ-画面のON・OFFをする</summary>
    public void MenuOpenOrClose()
    {
        //撤退中は開けないようにする
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

    /// <summary>味方モンスターが全滅したら呼び出す</summary>
    public void GameOver()
    {
        _gameOverText.gameObject.SetActive(true);
    }

    /// <summary>退却時にテキストを表示する</summary>
    public void Retreat()
    {
        _retreatText.enabled = !_retreatText.enabled;
    }
}
