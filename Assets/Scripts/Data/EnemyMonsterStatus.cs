using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMonsterStatus : MonoBehaviour
{
    [SerializeField, Tooltip("Status�Ȃǂ��擾����ۂɎg�p���鋤�ʔԍ�")]
    int _charaId = 0;

    [SerializeField]
    int _firstLv = 1;

    ///// <summary>�g����X�L��</summary>
    [SerializeField, Tooltip("�g����X�L��")]
    internal List<SKILL> _skillList;

    /// <summary>�^����ꂽ���</summary>
    internal TacticsList _tactics = default;

    //�����ȃX�e�[�^�X

    /// <summary>���x��</summary>
    public int LV;
    /// <summary>���O</summary>
    public string NAME;
    /// <summary>����</summary>
    public int ATTRIBUTE;
    /// <summary>�R���X�e�B�`���[�V����,�̗�</summary>
    private int CON;
    /// <summary>�}�W�b�N�p���[,����</summary>
    private int MAG;
    /// <summary>�����I�ȗ�</summary>
    private int STR;
    /// <summary>Vitality,�����I�Ȋ拭���A��Ԉُ�ւ̒�R��</summary>
    private int VIT;
    /// <summary>Intelligence,�m��</summary>
    private int INT;
    /// <summary>Evasion,���</summary>
    private int EVA;
    /// <summary>Luck , �^</summary>
    private int LUK;

    void LevelSet(int level)
    {
        LV = level;
        StatusSet();
    }
    //------------�X�e�[�^�X�ւ̃o�t�E�f�o�t�{��---------------
    /// <summary>�q�b�g�|�C���g</summary>
    public float HP_Buff = 1.0f;
    /// <summary>�}�W�b�N�|�C���g</summary>
    public float MP_Buff = 1.0f;
    /// <summary>�����I�ȍU���͂�</summary>
    public float ATK_Buff = 1.0f;
    /// <summary>�����I�Ȋ拭��</summary>
    public float DEF_Buff = 1.0f;
    /// <summary>�m��</summary>
    public float MAT_Buff = 1.0f;
    /// <summary>���</summary>
    public float AVD_Buff = 1.0f;
    /// <summary>Critical,�N���e�B�J���̔�����</summary>
    public float CRI_Buff = 1.0f;

    //------------�v�Z��̃X�e�[�^�X�Ȃ�---------------

    /// <summary>�q�b�g�|�C���g</summary>
    public int HP;
    /// <summary>�q�b�g�|�C���g�̍ő�l</summary>
    public int HPMax;
    /// <summary>�}�W�b�N�|�C���g</summary>
    public int MP;
    /// <summary>�}�W�b�N�|�C���g�̍ő�l</summary>
    public int MPMax;
    /// <summary>�����I�ȍU���͂�</summary>
    public int ATK;
    /// <summary>�����I�Ȋ拭��</summary>
    public int DEF;
    /// <summary>�m��</summary>
    public int MAT;
    /// <summary>���</summary>
    public int AVD;
    /// <summary>Critical,�N���e�B�J���̔�����</summary>
    public int CRI;
    /// <summary>�����Ă���o���l�̑���</summary>
    public int EXP;

    // Start is called before the first frame update
    void Start()
    {
        NAME = SetStatus.Instance.GetName(_charaId);
        ATTRIBUTE = SetStatus.Instance.GetAttribute(_charaId);
        LevelSet(_firstLv);
        HP = HPMax;
        MP = MPMax;
        SkillSet();
        EXP = ExpTable.instance._expTable[_charaId].enemy_exp;
    }

    private void SkillSet()
    {
        _skillList = MonsterSkill.instance.SkillSet(_charaId, LV);
    }

    public void TacticsSet(TacticsList tactics)
    {
        _tactics = tactics;
    }

    void StatusSet()
    {
        int[] setStatus = SetStatus.instance.GetStatus(_charaId, LV);
        CON = setStatus[0];
        MAG = setStatus[1];
        STR = setStatus[2];
        VIT = setStatus[3];
        INT = setStatus[4];
        EVA = setStatus[5];
        LUK = setStatus[6];

        HPMax = (int)(CON * HP_Buff);
        MPMax = (int)(MAG * MP_Buff);
        ATK = (int)(STR * ATK_Buff);
        DEF = (int)(VIT * DEF_Buff);
        MAT = (int)(INT * MAT_Buff);
        AVD = (int)(EVA * AVD_Buff);
        CRI = (int)(LUK * CRI_Buff);
    }
}
