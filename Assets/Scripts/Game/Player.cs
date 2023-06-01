using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>
{
    #region �ϐ�
    [SerializeField, Tooltip("�A�C�e�����g�p����̂ɕK�v")]
     UseItem _useItem;
    [SerializeField, Tooltip("�������鉼�z��")]
     int[] _tacticsSetArray;

    [SerializeField, Tooltip("�������Ă���A�C�e��")] List<Item> _itemList = new List<Item>();

    /// <summar>�������Ă��郂���X�^�[��ێ����邽�߂̃��X�g</summar>
    List<MonsterStatus> _pms = new List<MonsterStatus>();

    /// <summary>�v���C���[�����X�^�[�̍�탊�X�g</summary>
    List<TacticsClass> _tacticsList;

    /// <summary>���ݐݒ肵�Ă����탊�X�g</summary>
    TacticsClass[] _tacticsArray = new TacticsClass[4];

    /// <summary>�퓬�͈͓��ɂ���G�̃��X�g</summary>
    List<MonsterStatus> _enemyList = new List<MonsterStatus>();
    /// <summary>���݂̍��</summary>
    TacticsClass tacticsNow;
    /// <summary>�P�ޏI�����ɖ߂��O�̍��</summary>
    TacticsClass backTactics;
    /// <summary>�Ō���ŒǏ]���Ă���L�����N�^�[��index
    /// 0 = �v���C���[
    /// 1 �` 3 = �����X�^�[
    /// </summary>
    int _followIndex = 0;
    /// <summary>�����X�^�[�̒N�����퓬�����ǂ���</summary>
    bool inBattle = false;
    /// <summary>���݃^�[�Q�b�g���Ă���G</summary>
    public MonsterStatus target;


    #endregion

    #region �v���p�e�B
    TacticSlot TacticSlot => UiManager.Instance.TacticSlot;

    ItemSlot ItemSlot => UiManager.Instance.ItemSlot;

    ItemInventoryManager InventoryManager =>  UiManager.Instance.InventoryManager;

    /// <summar>�������Ă��郂���X�^�[�̃��X�g</summar>
    public List<MonsterStatus> MonstersStatus => _pms;

    /// <summary>�v���C���[�̎��͂ɂ��郂���X�^�[</summary>
    public List<MonsterStatus> EnemyList => _enemyList;

    /// <summary>�v���C���[�����X�^�[�̍�탊�X�g</summary>
    public List<TacticsClass> TacticsList => _tacticsList;

    /// <summary>���ݐݒ肵�Ă����탊�X�g</summary>
    public TacticsClass[] TacticsArray => _tacticsArray;

    /// <summary>�Ǐ]����v���C���[�⒇�Ԃ�Ԃ�</summary>
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
    /// �A�C�e���̎擾
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
    /// �����X���b�g�ɓ����
    /// </summary>
    /// <param name="tacticsArray"></param>
    public void SetTacticsSlot(TacticsClass[] tacticsArray)
    {
        _tacticsArray = tacticsArray;
        TacticSlot.TacticSlotSet(_tacticsArray);
    }

    /// <summary>
    /// �A�C�e���̎g�p
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

    /// <summary>���������X�^�[�ɓ`����</summary>
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

    /// <summary>�ދp�̍���ON�EOFF</summary>
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

    /// <summary>�ړG�����G���擾</summary>
    /// <param name="monster"></param>
    public void EnemyDiscover(MonsterStatus monster)
    {
        if (!_enemyList.Contains(monster))
        {
            _enemyList.Add(monster);
        }
    }

    //�G�����X�^�[���͈͊O�ɏo���������͓|�ꂽ�Ƃ�
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
