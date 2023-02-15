using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>
{
    [SerializeField, Tooltip("�������鉼�z��")]
    private int[] _tacticsSetArray;

    [SerializeField, Tooltip("�������Ă���A�C�e��")]
    private List<Item> _itemList = new List<Item>();

    [SerializeField, Tooltip("�v���C���[�����X�^�[�̍�탊�X�g�̃A�Z�b�g")]
    private TacticsTree _tacticsTree;

    /// <summar>�������Ă��郂���X�^�[��ێ����邽�߂̃��X�g</summar>
    private List<MonsterStatus> _pms = new List<MonsterStatus>();

    /// <summary>�v���C���[�����X�^�[�̍�탊�X�g</summary>
    private List<TacticsClass> _tacticsList;

    /// <summary>���ݐݒ肵�Ă����탊�X�g</summary>
    private TacticsClass[] _tacticsArray = new TacticsClass[4];

    private TacticSlot TacticSlot => UiManager.Instance.TacticSlot;

    private ItemSlot ItemSlot => UiManager.Instance.ItemSlot;

    /// <summary>�퓬�͈͓��ɂ���G�̃��X�g</summary>
    private List<MonsterStatus> _enemyList = new List<MonsterStatus>();

    /// <summary>���݃^�[�Q�b�g���Ă���G</summary>
    public MonsterStatus _target;

    public List<MonsterStatus> MonsterStatus => _pms;

    public List<MonsterStatus> EnemyList => _enemyList;

    // Start is called before the first frame update
    void Start()
    {
        _tacticsArray = new TacticsClass[4].Select(x => { return new TacticsClass(); }).ToArray();
        _tacticsList = _tacticsTree._tactics;
        SetTacticsSlot(_tacticsSetArray);
        SetItemSlot();
    }

    public void ConductTactics(int i)
    {
        Debug.Log($"{_tacticsArray[i].tactics_id} {_tacticsArray[i].tactics_name}");

        foreach (var monster in _pms) 
        {
            monster.TacticsSet(_tacticsArray[i]);
        }
    }

    public void EnemyDiscover(MonsterStatus monster) 
    {
        if (!_enemyList.Contains(monster))
        {
            _enemyList.Add(monster);
        }
    }

    public void UseItems(int i)
    {
        Debug.Log($"{_itemList[i].name} {_itemList[i].infomation} {_itemList[i].type}");

        UseItem.Instance.UseItemEffect(_itemList[i]);
    }

    //�G�����X�^�[���͈͊O�ɏo���������͓|�ꂽ�Ƃ�
    public void ExitDetectObject(GameObject other)
    {
        if (_enemyList.Contains(other.GetComponent<MonsterStatus>()))
        {
            _enemyList.Remove(other.GetComponent<MonsterStatus>());
        }
    }

    private void SetTacticsSlot(int[] tacticsNumber)
    {
        for (int i = 0; i < tacticsNumber.Length; i++)
        {
            _tacticsArray[i].tactics_id = tacticsNumber[i];
            _tacticsArray[i].tactics_name = _tacticsList[tacticsNumber[i]].tactics_name;
            _tacticsArray[i].tactics_info = _tacticsList[tacticsNumber[i]].tactics_info;
            _tacticsArray[i].tactics_type = _tacticsList[tacticsNumber[i]].tactics_type;
        }
        TacticSlot.TacticSlotSet(_tacticsArray);
    }

    private void SetItemSlot()
    {
        ItemSlot.ItemSlotSet(_itemList);
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

        //�߂܂������X�^�[�̃^�O�ƍ��̂ݕς��Ă��̂܂܎g������
    }
}
