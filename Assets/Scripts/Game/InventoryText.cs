using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryText : MonoBehaviour
{
    [SerializeField]
    private�@Text _text = null;

    /// <summary>�A�C�e���̐�����</summary>
    public string _itemInformation = "";

    /// <summary>�A�C�e���̏��������擾���\������</summary>
    public void ItemCountSet(int value) 
    {
        _text.text = value.ToString();
    }
}
