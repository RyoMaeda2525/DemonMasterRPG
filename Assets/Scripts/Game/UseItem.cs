using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UseItem : MonoBehaviour
{
    int _portion = 150;

    public void UseItemEffect(Item item) 
    {
        if (item.type == Item.ItemType.CollectItem) 
        {
            switch (item.name) 
            {
                case "Portion":
                    foreach (var monster in Player.Instance._pms) 
                    {
                        Debug.Log(monster.name);
                        monster.GetComponent<ChangeStatus>().Heal(_portion);
                    }
                    break;
            }
        }
    }

    public static UseItem instance;

    public static UseItem Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(UseItem);

                instance = (UseItem)FindObjectOfType(t);
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
