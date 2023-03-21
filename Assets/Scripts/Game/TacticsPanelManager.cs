using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TacticsPanelManager : MonoBehaviour
{
    [SerializeField]
    TacticsButton[] _tacticsButtons;

    [SerializeField]
    Image[] _selectObjects;

    [SerializeField]
    GameObject tacticsPrefab;

    [SerializeField]
    GameObject _buttonParent;

    [SerializeField]
    MenuManager _menuManager;

    [SerializeField, Header("パネルが開いたときの説明文")]
    string _panelText;

    TacticsClass[] _playerTactics = new TacticsClass[4];

    List<TacticsClass> _tacticsList = new List<TacticsClass>();

    Player _player => Player.Instance;

    int _selectIndex = 0;

    void Awake()
    {
        _tacticsList = _player.TacticsList;

        for (int i = 1; i < _tacticsList.Count; i++)
        {
            var tactics = _tacticsList[i];

            GameObject button = Instantiate(tacticsPrefab, _buttonParent.transform);
            TacticsButton tacticsButton = button.GetComponent<TacticsButton>();
            tacticsButton.TacticsClass = tactics;
            tacticsButton.OnClickAllTactics();
        }
    }

    void OnEnable()
    {
        _playerTactics = _player.TacticsArray;

        for (int i = 0; i < _playerTactics.Length; i++)
        {
            _tacticsButtons[i].TacticsClass = _playerTactics[i];
            _tacticsButtons[i].buttonIndex = i;
            _tacticsButtons[i].OnClickPlayerTactics();
        }

        foreach(var cursor in _selectObjects)
        {
            cursor.enabled = false;
        }
        _buttonParent.SetActive(false);
        _menuManager.TextSet(_panelText);
    }

    void OnDisable()
    {
       _buttonParent.SetActive(false);
        Player.Instance.SetTacticsSlot(_playerTactics);
    }

    /// <summary>変更する作戦を選択する</summary>
    public void TacticsChange(int index) 
    {
        _selectObjects[_selectIndex].enabled = false;
        _selectIndex = index;
        _selectObjects[_selectIndex].enabled = true;
        _buttonParent.SetActive(true);
    }

    /// <summary>どの作戦にするのか選択する</summary>
    public void TacticsSelect(TacticsClass changeTactics) 
    {
        if (_playerTactics.Contains(changeTactics))
        {
            for (int i = 0; i < _playerTactics.Length; i++)
            {
                if (_playerTactics[i] == changeTactics)
                {
                    TacticsClass hoge = _playerTactics[_selectIndex];
                    _playerTactics[_selectIndex] = changeTactics;
                    _playerTactics[i] = hoge;
                }
            }
        }
        else 
        {
            _playerTactics[_selectIndex] = changeTactics;
        }

        foreach (var button in _tacticsButtons)
        {
            button.TacticsClass = _playerTactics[button.buttonIndex];
        }
        _selectObjects[_selectIndex].enabled = false;
        _buttonParent.SetActive(false);
    }
}
