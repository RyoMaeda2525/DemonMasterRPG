using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>
{
    #region 変数
    [SerializeField, Tooltip("アイテムを使用するのに必要")]
     UseItem _useItem;
    [SerializeField, Tooltip("作戦を入れる仮配列")]
     int[] _tacticsSetArray;

    [SerializeField, Tooltip("所持しているアイテム")] List<Item> _itemList = new List<Item>();

    /// <summar>所持しているモンスターを保持するためのリスト</summar>
    List<MonsterStatus> _pms = new List<MonsterStatus>();

    /// <summary>プレイヤーモンスターの作戦リスト</summary>
    List<TacticsClass> _tacticsList;

    /// <summary>現在設定している作戦リスト</summary>
    TacticsClass[] _tacticsArray = new TacticsClass[4];

    /// <summary>戦闘範囲内にいる敵のリスト</summary>
    List<MonsterStatus> _enemyList = new List<MonsterStatus>();
    /// <summary>現在の作戦</summary>
    TacticsClass tacticsNow;
    /// <summary>撤退終了時に戻す前の作戦</summary>
    TacticsClass backTactics;
    /// <summary>最後尾で追従しているキャラクターのindex
    /// 0 = プレイヤー
    /// 1 ～ 3 = モンスター
    /// </summary>
    int _followIndex = 0;
    /// <summary>モンスターの誰かが戦闘中かどうか</summary>
    bool inBattle = false;
    /// <summary>現在ターゲットしている敵</summary>
    public MonsterStatus target;


    #endregion

    #region プロパティ
    TacticSlot TacticSlot => UiManager.Instance.TacticSlot;

    ItemSlot ItemSlot => UiManager.Instance.ItemSlot;

    ItemInventoryManager InventoryManager =>  UiManager.Instance.InventoryManager;

    /// <summar>所持しているモンスターのリスト</summar>
    public List<MonsterStatus> MonstersStatus => _pms;

    /// <summary>プレイヤーの周囲にいるモンスター</summary>
    public List<MonsterStatus> EnemyList => _enemyList;

    /// <summary>プレイヤーモンスターの作戦リスト</summary>
    public List<TacticsClass> TacticsList => _tacticsList;

    /// <summary>現在設定している作戦リスト</summary>
    public TacticsClass[] TacticsArray => _tacticsArray;

    /// <summary>追従するプレイヤーや仲間を返す</summary>
    public GameObject FollowGameObject 
    {
        get 
        {
            if (_followIndex == 0 || _followIndex == _pms.Count) 
            {
                _followIndex = 1;
                return this.gameObject;
            }

            GameObject followTarget = _pms[_followIndex - 1].gameObject;

            _followIndex++;

            return followTarget;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        foreach (var tree in GameManager.Instance.TacticsTrees)
        {
            if (tree.name == "PlayerMonster")
            {
                _tacticsList = Instantiate(tree)._tactics;
            }
        }
        _tacticsArray = new TacticsClass[4].Select(x => { return new TacticsClass(); }).ToArray();
        SetTacticsSlotIndex(_tacticsSetArray);
        SetItemSlot();
        tacticsNow = _tacticsArray.First();
    }

    void SetTacticsSlotIndex(int[] tacticsNumber)
    {
        for (int i = 0; i < tacticsNumber.Length; i++)
        {
            _tacticsArray[i] = _tacticsList[tacticsNumber[i]];
        }
        TacticSlot.TacticSlotSet(_tacticsArray);
    }

    void SetItemSlot()
    {
        ItemSlot.ItemSlotSet(_itemList);
    }

    /// <summary>
    /// アイテムの取得
    /// </summary>
    /// <param name="item"></param>
    public void GetItems(Item item)
    {
        if (_itemList.Count < 4)
        {
            _itemList.Add(item);
            SetItemSlot();
        }
        else 
        { 
            InventoryManager.ItemInventorySet = item; 
        }

    }

    /// <summary>
    /// 作戦をスロットに入れる
    /// </summary>
    /// <param name="tacticsArray"></param>
    public void SetTacticsSlot(TacticsClass[] tacticsArray)
    {
        _tacticsArray = tacticsArray;
        TacticSlot.TacticSlotSet(_tacticsArray);
    }

    /// <summary>
    /// アイテムの使用
    /// </summary>
    /// <param name="i"></param>
    public void UseItems(int i)
    {
        if (_itemList.Count > 0)
        {
            _useItem.UseItemEffect(_itemList[i]);
            _itemList.Remove(_itemList[i]);
            SetItemSlot();
        }
    }

    /// <summary>作戦をモンスターに伝える</summary>
    /// <param name="i"></param>
    public void ConductTactics(int i)
    {
        if (tacticsNow != _tacticsList.First())
        {
            tacticsNow = _tacticsArray[i];

            foreach (var monster in _pms)
            {
                monster.TacticsSet(tacticsNow);
            }
        }
    }

    /// <summary>退却の作戦のON・OFF</summary>
    public void Retreat()
    {
        if (tacticsNow != _tacticsList.First())
        {
            backTactics = tacticsNow;

            tacticsNow = _tacticsList.First();

            foreach (var monster in _pms)
            {
                monster.TacticsSet(_tacticsList.First());
            }
        }
        else
        {
            tacticsNow = backTactics;

            foreach (var monster in _pms)
            {
                monster.TacticsSet(tacticsNow);
            }
        }
    }

    /// <summary>接敵した敵を取得</summary>
    /// <param name="monster"></param>
    public void EnemyDiscover(MonsterStatus monster)
    {
        if (!_enemyList.Contains(monster))
        {
            _enemyList.Add(monster);
        }
    }

    //敵モンスターが範囲外に出たもしくは倒れたとき
    public void ExitDetectObject(GameObject other)
    {
        if (_enemyList.Contains(other.GetComponent<MonsterStatus>()))
        {
            _enemyList.Remove(other.GetComponent<MonsterStatus>());
        }
    }

    public void StartBattle(MonsterStatus target) 
    {
        _followIndex = 0;
        inBattle = true;
    }
}
