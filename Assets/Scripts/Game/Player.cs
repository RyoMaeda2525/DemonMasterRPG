using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>
{
    [SerializeField, Tooltip("�������鉼�z��")]
    private int[] _tacticsSetArray;

    /// <summar>�������Ă��郂���X�^�[��ێ����邽�߂̃��X�g</summar>
    public List<PlayerMonsterStatus> _pms = new List<PlayerMonsterStatus>();

    /// <summary>���w�����̒�~�p</summary>
    private bool _actionBool = false;
 
    /// <summary>���ݐݒ肵�Ă����탊�X�g</summary>
    private TacticsList[] _tacticsArray;

    /// <summary>���ݏo���Ă�����</summary>
    private TacticsList _tactics;

    /// <summary>�퓬�͈͓��ɂ���G�̃��X�g</summary>
    public List<EnemyMonsterMove> _emmList;

    void SetTactics(int[] tacticsNumber)
    {
        _tacticsArray = TacticsManager.Instance.TacticsSet(tacticsNumber);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetTactics(_tacticsSetArray);
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("HorizontalKey");              // ���L�[�̐�������h�Œ�`
        float v = Input.GetAxis("VerticalKey");                // ���L�[�̐�������v�Œ�`

        if (!_actionBool && h != 0 || !_actionBool && v != 0)
        {
            ChangeTactics(h, v);
        }
    }

    void ChangeTactics(float h, float v)
    {
        StartCoroutine(ActionStop(3.0f));

        int i = -1;
        if (h > 0) { i = 0; }
        else if (v > 0) { i = 1; }
        else if (h < 0) { i = 2; }
        else if (v < 0) { i = 3; }

        if (i == -1) { Debug.Log("Error01"); return; }

        if (_pms.Count == 0) { Debug.Log("Error02"); return; }

        Debug.Log($"{_tacticsArray[i].tactics_id} {_tacticsArray[i].tactics_name} {_tacticsArray[i].tactics_info} {_tacticsArray[i].tactics_type}");

        _tactics = _tacticsArray[i];

        foreach (var monster in _pms)
        {
            monster.TacticsSet(_tacticsArray[i]);
        }
    }

    public void OnDetectObject(GameObject other)
    {
        if (other.GetComponent<EnemyMonsterMove>()) 
        {
            if (!_emmList.Contains(other.GetComponent<EnemyMonsterMove>()))
            {
                _emmList.Add(other.GetComponent<EnemyMonsterMove>());
            }

            if (_tactics.tactics_name == "�U������")
            {
                foreach(var monster in _pms) 
                {
                    monster.gameObject.GetComponent<PlayerMonsterCamera>().CameraEnemyFind(other);
                }
            }
        }
    }

    public void ExitDetectObject(GameObject other) 
    {
        if (_emmList.Contains(other.GetComponent<EnemyMonsterMove>()))
        {
            _emmList.Remove(other.GetComponent<EnemyMonsterMove>());
        }
    }

    private IEnumerator ActionStop(float waitTime)
    {
        _actionBool = true;
        yield return new WaitForSeconds(waitTime);
        _actionBool = false;
    }
}
