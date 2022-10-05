using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataBase", menuName = "CreateItemDataBase")]
public class ItemBase : ScriptableObject
{
    public List<Item> items = new List<Item>(); 
}
