using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryText : MonoBehaviour
{
    [SerializeField]
    private�@Text _text = null;

    [SerializeField]
    private Image[] _image;

    /// <summary>�A�C�e���̐�����</summary>
    public string _itemInformation = "";

    public void ImageOnOff() 
    {
        foreach(var image in _image) 
        {
            image.enabled = !image.enabled;
        }
    }

    /// <summary>�A�C�e���̏��������擾���\������</summary>
    public void ItemCountSet(int value) 
    {
        _text.text = value.ToString();
    }
}
