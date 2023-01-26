using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : SingletonMonoBehaviour<UiManager>
{
    [SerializeField]
    MonsterPanelManger _monsterPanel;


    public MonsterPanelManger MonsterPanel => _monsterPanel;
}
