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

    /// <summar>�������Ă��郂���X�^�[��ێ����邽�߂̃��X�g</summar>
    public List<PlayerMonsterStatus> _pms = new List<PlayerMonsterStatus>();

    public List<Item> _items = new List<Item>();

    /// <summary>���ݐݒ肵�Ă����탊�X�g</summary>
    public TacticsList[] _tacticsArray;

    /// <summary>���ݏo���Ă�����</summary>
    private TacticsList _tactics;

    /// <summary>�퓬�͈͓��ɂ���G�̃��X�g</summary>
    public List<EnemyMonsterMove> _emmList;

    private void SetTactics(int[] tacticsNumber)
    {
        _tacticsArray = TacticsManager.Instance.TacticsSet(tacticsNumber);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetTactics(_tacticsSetArray);
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
        Debug.Log($"{_items[i].name} {_items[i].infomation} {_items[i].type}");

        UseItem.Instance.UseItemEffect(_items[i]);
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

    //�����X�^�[���͈͊O�ɏo���Ƃ�
    public void ExitDetectObject(GameObject other)
    {
        if (_emmList.Contains(other.GetComponent<EnemyMonsterMove>()))
        {
            _emmList.Remove(other.GetComponent<EnemyMonsterMove>());
        }
    }
}
