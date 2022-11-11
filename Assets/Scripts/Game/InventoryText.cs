using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryText : MonoBehaviour
{
    [SerializeField]
    private　Text _text = null;

    [SerializeField]
    private Image[] _image;

    /// <summary>アイテムの説明文</summary>
    public string _itemInformation = "";

    public void ImageOnOff() 
    {
        foreach(var image in _image) 
        {
            image.enabled = !image.enabled;
        }
    }

    /// <summary>アイテムの所持数を取得し表示する</summary>
    public void ItemCountSet(int value) 
    {
        _text.text = value.ToString();
    }
}
