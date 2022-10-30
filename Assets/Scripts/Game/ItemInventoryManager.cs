using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class ItemInventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _inventoryPanel = null;

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

            Debug.Log($"_itemInventory[{value}] = {_itemInventory[value]}");
        }
    }

    public void OpenInventory() 
    {
        if (!_inventoryPanel.activeSelf) { _inventoryPanel.SetActive(true); }

        foreach (var keyValue in _itemInventory) 
        {
            GameObject item = Instantiate((GameObject)Resources.Load($"Images/{keyValue.Key}"), _inventoryPanel.transform);
            InventoryText it = item.GetComponent<InventoryText>();
            it.ItemCountSet(keyValue.Value);
            it._itemInformation = keyValue.Key.infomation;
        }
    }

    public static ItemInventoryManager instance;

    public static ItemInventoryManager Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(ItemInventoryManager);

                instance = (ItemInventoryManager)FindObjectOfType(t);
                if (instance == null)
                {
                    Debug.LogWarning($"{t}をアタッチしているオブジェクトがありません");
                }
            }

            return instance;
        }
    }

    protected bool CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
            return true;
        }
        else if (Instance == this)
        {
            return true;
        }
        Destroy(gameObject);
        return false;
    }
}
