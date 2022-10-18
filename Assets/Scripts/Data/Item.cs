using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�L�[�A�C�e����E���W�߂�A�C�e���Ȃ̂�����ʂ������</summary>
public enum ItemType
{
    KeyItem,
    CollectItem
}

[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{
    public ItemType type;
    public new string name;
    public String infomation; //������

    public Item(Item item)
    {
        this.type = item.type;
        this.name = item.name;
        this.infomation = item.infomation;
    }
}
