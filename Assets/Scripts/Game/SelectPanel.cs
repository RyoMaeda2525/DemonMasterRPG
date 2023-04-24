using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPanel : MonoBehaviour
{
    [SerializeField]
    MenuManager _menuManager;

    [SerializeField, Header("セレクト画面の説明文")]
    string _selectText;

    void OnEnable()
    {
        _menuManager.TextSet(_selectText);
    }
}
