using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemInventoryManager : MonoBehaviour
{
    [SerializeField]
    ItemBase _itemBase;

    [SerializeField]
    MenuManager _menuManager;

    [SerializeField, Header("ÉpÉlÉãÇÃê‡ñæï∂")]
    string _panelText;

    List<InventoryObject> items = new List<InventoryObject>();

    private Dictionary<Item, int> _itemInventory = new Dictionary<Item, int>();

    public Dictionary<Item, int> ItemInventoryGet
    {
        get { return _itemInventory; }
    }

    public Item ItemInventorySet
    {
        set
        {
            if (!_itemInventory.ContainsKey(value))
            {
                _itemInventory.Add(value, 1);
            }
            else { _itemInventory[value]++; }
        }
    }

    private void Awake()
    {
        foreach (var item in _itemBase.items)
        {
            GameObject itemObject = Instantiate((GameObject)Resources.Load($"Images/{item.name}"), this.transform);
            items.Add(itemObject.GetComponent<InventoryObject>());
            itemObject.name = item.name;
            itemObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        OpenInvntory();
        _menuManager.TextSet(_panelText);
    }

    public void OpenInvntory()
    {
        foreach (var keyValue in _itemInventory)
        {
            foreach (var item in items)
            {
                if (item.name == keyValue.Key.name)
                {
                    item.gameObject.SetActive(true);
                    item.ItemCountSet(keyValue.Value);
                    item._itemInformation = keyValue.Key.infomation;
                }
            }
        }
    }
}
