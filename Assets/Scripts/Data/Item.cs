using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{
    /// <summary>�L�[�A�C�e����E���W�߂�A�C�e���Ȃ̂�����ʂ������</summary>
    public enum ItemType 
    {
        KeyItem,
        CollectItem
    }

    public ItemType type;
    public String infomation; //������

    public Item(Item item)
    {
        this.type = item.type;
        this.infomation = item.infomation;
    }
}
