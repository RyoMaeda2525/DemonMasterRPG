using System;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    [SerializeField , Tooltip("�����X�^�[��ߊl����A�C�e�����g�p����̂ɕK�{")]
    ScoutManager _sm = null;

    public void UseItemEffect(Item item) 
    {
        if (item.type == ItemType.CollectItem) 
        {
            switch (item.kind) 
            {
                case ItemKind.Heal:
                    foreach (var monster in Player.Instance.MonsterStatus)
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
        if (Player.Instance._target) 
        {
             _sm.Scout(Player.Instance._target);
        }
        else { Debug.Log("No Target"); }
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
                    Debug.LogWarning($"{t}���A�^�b�`���Ă���I�u�W�F�N�g������܂���");
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
