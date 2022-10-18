using System;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    [SerializeField , Tooltip("�����X�^�[��ߊl����A�C�e�����g�p����̂ɕK�{")]
    ScoutManager _sm = null;
    
    [SerializeField]
    int _portion = 150;

    public void UseItemEffect(Item item) 
    {
        if (item.type == ItemType.CollectItem) 
        {
            switch (item.name) 
            {
                case "Portion":
                    Portion();
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

    private void Portion() 
    {
        foreach (var monster in Player.Instance._pms)
        {
            monster.GetComponent<ChangeStatus>().Heal(_portion);
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
