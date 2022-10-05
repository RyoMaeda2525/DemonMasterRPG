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

    public ItemType _type;
    public String _infomation; //������

    public Item(Item item)
    {
        this._type = item._type;
        this._infomation = item._infomation;
    }
}
