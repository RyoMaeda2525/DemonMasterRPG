using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class TacticsButton : MonoBehaviour
{
    [SerializeField]
    Button _button;
    [SerializeField]
    Text _text;

    TacticsClass _tacticsClass;
    TacticsPanelManager _panelManager;
    MenuManager _menuManager;

    /// <summary>PanelManager�N���X���̔z��ԍ�</summary>
    public int buttonIndex = 0;

    public TacticsClass TacticsClass 
    {
        set 
        { 
           _tacticsClass = value;
            _text.text = _tacticsClass.tactics_name;
        }
    }

    private void Start()
    {
        _panelManager = GetComponentInParent<TacticsPanelManager>();
        _menuManager = GetComponentInParent<MenuManager>();
        _button?.OnPointerEnterAsObservable()
            .Subscribe(_ => _menuManager.TextSet(_tacticsClass.tactics_info));
    }

    public void OnClickPlayerTactics() 
    {
        _button?.OnClickAsObservable()
            .Subscribe(_ => _panelManager.TacticsChange(buttonIndex));
    }

    public void OnClickAllTactics() 
    {
        _button?.OnClickAsObservable()
           .Subscribe(_ => _panelManager.TacticsSelect(_tacticsClass));
    }
}