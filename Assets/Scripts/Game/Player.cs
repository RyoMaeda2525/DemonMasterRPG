using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>
{
    [SerializeField, Tooltip("�������鉼�z��")]
    private int[] _tacticsSetArray;

    /// <summary>�q�b�g�|�C���g</summary>
    private int HP;
    /// <summary>�����I�ȍU���͂ւ̃o�t</summary>
    private int ATK_Buff;
    /// <summary>�����I�Ȋ拭�ւ̃o�t</summary>
    private int DEF_Buff;
    /// <summary>���@�ɑ΂��Ă̒�R�͂ւ̃o�t</summary>
    private int MDEF_Buff;
    /// <summary>��𗦂ւ̃o�t</summary>
    private int EVA_Buff;
    /// <summary>�N���e�B�J���̔������ւ̃o�t</summary>
    private int CRI_Buff;
    /// <summary>�����Ă���o���l�̑���</summary>
    private int EXP;
    /// <summary>���̃��x���ւ̌o���l�̑���</summary>
    private int NEXT_EXP;

    /// <summar>�������Ă��郂���X�^�[��ێ����邽�߂̃��X�g</summar>
    public List<PlayerMonsterStatus> _pms = new List<PlayerMonsterStatus>();

    private bool _actionBool = false;

    private TacticsList[] _tacticsArray;

    void SetTactics(int[] tacticsNumber)
    {
        _tacticsArray = TacticsManager.Instance.TacticsSet(tacticsNumber);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetTactics(_tacticsSetArray);
        foreach (var tactics in _tacticsArray) { Debug.Log(tactics.tactics_name); }
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

        foreach (var monster in _pms)
        {
            monster.TacticsSet(_tacticsArray[i]);
        }
    }

    private IEnumerator ActionStop(float waitTime)
    {
        _actionBool = true;
        yield return new WaitForSeconds(waitTime);
        _actionBool = false;
    }
}
