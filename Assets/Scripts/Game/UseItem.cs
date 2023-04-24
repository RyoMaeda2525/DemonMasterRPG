using System;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    [SerializeField , Tooltip("モンスターを捕獲するアイテムを使用するのに必須")]
    ScoutManager _sm = null;

    public void UseItemEffect(Item item) 
    {
        if (item.type == ItemType.CollectItem) 
        {
            switch (item.kind) 
            {
                case ItemKind.Heal:
                    foreach (var monster in Player.Instance.MonstersStatus)
                    {
                        monster.Heal(item.value);
                    }
                    break;
            }
        }
        else if(item.type == ItemType.KeyItem) 
        {
            switch (item.name)
            {
                case "ScoutRing":
                    ScoutRing();
                    break;
            }
        }
    }

    private void ScoutRing() 
    {
        if (Player.Instance.target) 
        {
             _sm.Scout(Player.Instance.target);
        }
        else { Debug.Log("No Target"); }
    }
}
