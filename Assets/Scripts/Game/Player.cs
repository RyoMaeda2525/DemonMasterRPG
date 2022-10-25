using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : SingletonMonoBehaviour<Player>
{
    [SerializeField, Tooltip("�������鉼�z��")]
    private int[] _tacticsSetArray;

    [SerializeField, Tooltip("�������Ă���A�C�e��")]
    private List<Item> _itemList = new List<Item>();

    /// <summar>�������Ă��郂���X�^�[��ێ����邽�߂̃��X�g</summar>
    public List<PlayerMonsterStatus> _pms = new List<PlayerMonsterStatus>();

    /// <summary>���ݐݒ肵�Ă����탊�X�g</summary>
    private TacticsList[] _tacticsArray;

    /// <summary>���ݏo���Ă�����</summary>
    private TacticsList _tactics;

    /// <summary>�퓬�͈͓��ɂ���G�̃��X�g</summary>
    public List<EnemyMonsterMove> _emmList;

    /// <summary>���݃^�[�Q�b�g���Ă���G</summary>
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

    //�G���͈͓��ɓ������Ƃ��A�����X�^�[�Ɏ����U�����w�����Ă����
    //���E�ɓ������Ƃ��_���悤�ɂ���
    public void OnDetectObject(GameObject other)
    {
        if (other.GetComponent<EnemyMonsterMove>())
        {
            if (_tactics.tactics_name == "�U������")
            {
                foreach (var monster in _pms)
                {
                    monster.gameObject.GetComponent<PlayerMonsterCamera>().CameraEnemyFind(other);
                }
            }
        }
    }

    //�G�����X�^�[���͈͊O�ɏo���������͓|�ꂽ�Ƃ�
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
