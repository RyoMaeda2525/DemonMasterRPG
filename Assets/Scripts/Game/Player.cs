using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>
{
    [SerializeField, Tooltip("作戦を入れる仮配列")]
    private int[] _tacticsSetArray;

    [SerializeField, Tooltip("所持しているアイテム")]
    private List<Item> _itemList = new List<Item>();

    /// <summar>所持しているモンスターを保持するためのリスト</summar>
    private List<MonsterStatus> _pms = new List<MonsterStatus>();

    /// <summary>現在設定している作戦リスト</summary>
    private TacticsList[] _tacticsArray;

    /// <summary>戦闘範囲内にいる敵のリスト</summary>
    public List<MonsterStatus> _enemyList;

    /// <summary>現在ターゲットしている敵</summary>
    public MonsterStatus _target;

    public List<MonsterStatus> MonsterStatus => _pms;

    // Start is called before the first frame update
    void Start()
    {
        SetTacticsSlot(_tacticsSetArray);
        SetItemSlot();
    }

    public void ConductTactics(int i)
    {
        Debug.Log($"{_tacticsArray[i].tactics_id} {_tacticsArray[i].tactics_name} {_tacticsArray[i].tactics_info} {_tacticsArray[i].tactics_type}");


        //foreach (var monster in _pms)
        //{
        //    if (monster.gameObject.activeSelf)
        //    {
        //        monster.TacticsSet(_tacticsArray[i]);
        //        monster.gameObject.GetComponent<PlayerMonsterMove>().TacticsOnAction();
        //    }
        //}
    }

    public void UseItems(int i) 
    {
        Debug.Log($"{_itemList[i].name} {_itemList[i].infomation} {_itemList[i].type}");

        UseItem.Instance.UseItemEffect(_itemList[i]);
    }

    //敵モンスターが範囲外に出たもしくは倒れたとき
    public void ExitDetectObject(GameObject other)
    {
        if (_enemyList.Contains(other.GetComponent<MonsterStatus>()))
        {
            _enemyList.Remove(other.GetComponent<MonsterStatus>());
        }
    }

    private void SetTacticsSlot(int[] tacticsNumber)
    {
        _tacticsArray = TacticsManager.Instance.TacticsSet(tacticsNumber);
        GameManager.Instance.TacticSlot.TacticSlotSet(_tacticsArray);
    }

    private void SetItemSlot()
    {
        GameManager.Instance.ItemSlot.ItemSlotSet(_itemList);
    }

    public void GetItems(Item item) 
    {
        if (_itemList.Count < 4)
        {
            _itemList.Add(item);
            SetItemSlot();
        }
        else { ItemInventoryManager.Instance.ItemInventorySet = item; }
        
    }

    public void UseItems(Item item) 
    {
        _itemList.Remove(item);
        SetItemSlot();
    }

    public void ScoutSuccess(MonsterStatus monster)
    {
        monster.tag = "PlayerMonster";

        _pms.Add(monster);

        //捕まえモンスターのタグと作戦のみ変えてそのまま使いたい
    }
}
