using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>�q�b�g�|�C���g</summary>
    public int HP;
    /// <summary>�����I�ȍU���͂ւ̃o�t</summary>
    public int ATK_Buff;
    /// <summary>�����I�Ȋ拭�ւ̃o�t</summary>
    public int DEF_Buff;
    /// <summary>���@�ɑ΂��Ă̒�R�͂ւ̃o�t</summary>
    public int MDEF_Buff;
    /// <summary>��𗦂ւ̃o�t</summary>
    public int EVA_Buff;
    /// <summary>�N���e�B�J���̔������ւ̃o�t</summary>
    public int CRI_Buff;
    /// <summary>�����Ă���o���l�̑���</summary>
    public int EXP;
    /// <summary>���̃��x���ւ̌o���l�̑���</summary>
    public int NEXT_EXP;

    /// <summar>�������Ă��郂���X�^�[��ێ����邽�߂̃��X�g</summar>
    List<PlayerMonsterStatus> _pms = default;

    Tactics[] _tacticsArray = default;

    void SetTactics(int[] tacticsNumber)
    {
        _tacticsArray = TacticsManager.Instance.TacticsSet(tacticsNumber);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("HorizontalKey");              // ���L�[�̐�������h�Œ�`
        float v = Input.GetAxis("VerticalKey");                // ���L�[�̐�������v�Œ�`

        if (h != 0 || v != 0)
        {
            ChangeTactics(h, v);
        }
    }

    void ChangeTactics(float h , float v)
    {
        int i = -1;
        if (h > 0) { i = 0; }
        else if (v > 0) { i = 1; }
        else if (h < 0) { i = 2; }
        else if (v < 0) { i = 3; }

        if(i == -1) { Debug.Log("Error"); return; }

        foreach (var monster in _pms) 
        {
            monster.TacticsSet(_tacticsArray[i]);
        }
    }
}
