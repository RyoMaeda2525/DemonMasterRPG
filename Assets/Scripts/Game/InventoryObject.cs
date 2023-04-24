using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryObject : MonoBehaviour
{
    [SerializeField]
    Text _text = null;

    [SerializeField]
    Image[] _image;

    /// <summary>�A�C�e���̐�����</summary>
    public string _itemInformation = "";

    /// <summary>�A�C�e���̏��������擾���\������</summary>
    public void ItemCountSet(int value) 
    {
        _text.text = value.ToString();
    }
}
