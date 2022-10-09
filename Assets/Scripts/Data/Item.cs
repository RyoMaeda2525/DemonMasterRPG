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

    public ItemType type;
    public new string name;
    public String infomation; //説明文

    public Item(Item item)
    {
        this.type = item.type;
        this.name = item.name;
        this.infomation = item.infomation;
    }
}
