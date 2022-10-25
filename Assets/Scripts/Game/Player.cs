using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : SingletonMonoBehaviour<Player>
{
    [SerializeField, Tooltip("作戦を入れる仮配列")]
    private int[] _tacticsSetArray;

    [SerializeField, Tooltip("所持しているアイテム")]
    private List<Item> _itemList = new List<Item>();

    /// <summar>所持しているモンスターを保持するためのリスト</summar>
    public List<PlayerMonsterStatus> _pms = new List<PlayerMonsterStatus>();

    /// <summary>現在設定している作戦リスト</summary>
    private TacticsList[] _tacticsArray;

    /// <summary>現在出している作戦</summary>
    private TacticsList _tactics;

    /// <summary>戦闘範囲内にいる敵のリスト</summary>
    public List<EnemyMonsterMove> _emmList;

    /// <summary>現在ターゲットしている敵</summary>
    public EnemyMonsterMove _target;

    // Start is called before the first frame update
    void Start()
    {
        SetTacticsSlot(_tacticsSetArray);
        SetItemSlot();
    }

    public void ConductTactics(int i)
    {
        _tactics = _tacticsArray[i];

        Debug.Log($"{_tacticsArray[i].tactics_id} {_tacticsArray[i].tactics_name} {_tacticsArray[i].tactics_info} {_tacticsArray[i].tactics_type}");


        foreach (var monster in _pms)
        {
            if (monster.gameObject.activeSelf)
            {
                monster.TacticsSet(_tacticsArray[i]);
                monster.gameObject.GetComponent<PlayerMonsterMove>().TacticsOnAction();
            }
        }
    }

    public void UseItems(int i) 
    {
        Debug.Log($"{_itemList[i].name} {_itemList[i].infomation} {_itemList[i].type}");

        UseItem.Instance.UseItemEffect(_itemList[i]);
    }

    //敵が範囲内に入ったとき、モンスターに自動攻撃を指示していれば
    //視界に入ったとき狙うようにする
    public void OnDetectObject(GameObject other)
    {
        if (other.GetComponent<EnemyMonsterMove>())
        {
            if (_tactics.tactics_name == "攻撃しろ")
            {
                foreach (var monster in _pms)
                {
                    monster.gameObject.GetComponent<PlayerMonsterCamera>().CameraEnemyFind(other);
                }
            }
        }
    }

    //敵モンスターが範囲外に出たもしくは倒れたとき
    public void ExitDetectObject(GameObject other)
    {
        if (_emmList.Contains(other.GetComponent<EnemyMonsterMove>()))
        {
            _emmList.Remove(other.GetComponent<EnemyMonsterMove>());
        }
    }

    private void SetTacticsSlot(int[] tacticsNumber)
    {
        _tacticsArray = TacticsManager.Instance.TacticsSet(tacticsNumber);
        TacticSlot.Instance.TacticSlotSet(_tacticsArray);
    }

    private void SetItemSlot()
    {
        ItemSlot.Instance.ItemSlotSet(_itemList);
    }

    public void GetItems(Item item) 
    {
        _itemList.Add(item);
        SetItemSlot();
    }

    public void UseItems(Item item) 
    {
        _itemList.Remove(item);
        SetItemSlot();
    }

    public void PartyAdd(PlayerMonsterStatus pms) 
    {
        _pms.Add(pms);
    }
}
