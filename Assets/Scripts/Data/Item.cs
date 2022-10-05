using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{
    /// <summary>キーアイテムや拾い集めるアイテムなのかを区別するもの</summary>
    public enum ItemType 
    {
        KeyItem,
        CollectItem
    }

    public ItemType _type;
    public String _infomation; //説明文

    public Item(Item item)
    {
        this._type = item._type;
        this._infomation = item._infomation;
    }
}
