using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>キーアイテムや拾い集めるアイテムなのかを区別するもの</summary>
public enum ItemType
{
    KeyItem,
    CollectItem
}

public enum ItemKind 
{
    None,
    Heal
}

[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{
    public ItemType type;
    public ItemKind kind;
    public new string name;
    public int value;
    public String infomation; //説明文

    public Item(Item item)
    {
        this.type = item.type;
        this.name = item.name;
        this.value = item.value;
        this.infomation = item.infomation;
    }
}
