using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPanel : MonoBehaviour
{
    [SerializeField]
    MenuManager _menuManager;

    [SerializeField, Header("�Z���N�g��ʂ̐�����")]
    string _selectText;

    void OnEnable()
    {
        _menuManager.TextSet(_selectText);
    }
}
