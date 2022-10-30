using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryText : MonoBehaviour
{
    [SerializeField]
    private　Text _text = null;

    /// <summary>アイテムの説明文</summary>
    public string _itemInformation = "";

    /// <summary>アイテムの所持数を取得し表示する</summary>
    public void ItemCountSet(int value) 
    {
        _text.text = value.ToString();
    }
}
