using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
using static UnityEditor.Progress;

public class ItemInventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _inventoryPanel = null;

    private Dictionary<Item, int> _itemInventory = new Dictionary<Item, int>();

    private List<GameObject> _items = new List<GameObject>();

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

    public void OpenOrCloseInventory() 
    {
        //if (!_inventoryPanel.activeSelf) { _inventoryPanel.SetActive(true); }
        //else 
        //{
        //    _inventoryPanel.SetActive(false);

            foreach (Transform n in _inventoryPanel.transform)
            {
                n.GetComponent<InventoryObject>().ImageOnOff();
            }
        //    return;
        //}

        foreach (var keyValue in _itemInventory) 
        {
            if (_inventoryPanel.transform.Find($"{keyValue.Key.name}(Clone)"))
            {
                var item = _inventoryPanel.transform.Find($"{keyValue.Key.name}(Clone)");
                InventoryObject it = item.GetComponent<InventoryObject>();
                it.ImageOnOff();
                it.ItemCountSet(keyValue.Value);
                it._itemInformation = keyValue.Key.infomation;
            }
            else
            {
                GameObject item = Instantiate((GameObject)Resources.Load($"Images/{keyValue.Key.name}"), _inventoryPanel.transform);
                _items.Add(item);
                InventoryObject it = item.GetComponent<InventoryObject>();
                it.ItemCountSet(keyValue.Value);
                it._itemInformation = keyValue.Key.infomation;
            }
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
