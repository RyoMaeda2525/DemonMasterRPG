using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMonsterStatus : MonoBehaviour
{
    [SerializeField, Tooltip("Status�Ȃǂ��擾����ۂɎg�p���鋤�ʔԍ�")]
    int _charaId = 0;

    [SerializeField, Tooltip("Status�Ȃǂ��擾����ۂɎg�p���鋤�ʔԍ�")]
    int _firstLv = 0;

    //�����ȃX�e�[�^�X

    /// <summary>���x��</summary>
    public int LV;
    /// <summary>���O</summary>
    public string NAME;
    /// <summary>����</summary>
    public int ATTRIBUTE;
    /// <summary>�R���X�e�B�`���[�V����,�̗�</summary>
    public int CON;
    /// <summary>�}�W�b�N�p���[,����</summary>
    public int MAG;
    /// <summary>�����I�ȗ�</summary>
    public int STR;
    /// <summary>Vitality,�����I�Ȋ拭���A��Ԉُ�ւ̒�R��</summary>
    public int VIT;
    /// <summary>Intelligence,�m��</summary>
    public int INT;
    /// <summary>Evasion,���</summary>
    public int EVA;
    /// <summary>Luck , �^</summary>
    public int LUK;

    public void LevelSet(int level)
    {
        LV = level;
        NEXT_EXP = ExpTable.instance.GetNextExp(_charaId, level);
        StatusSet();
        if(LV == _firstLv) { HP = HPMax; MP = MPMax; }
        MonsterPanelManger.Instance.PanalInfoSet(this);
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
    /// <summary>���̃��x���ւ̌o���l�̑���</summary>
    public int NEXT_EXP;

    /// <summary>�^����ꂽ���</summary>
    internal TacticsList _tactics = default;

    /// <summary>�^����ꂽ���</summary>
    internal List<SKILL> _skillList = new List<SKILL>();

    // Start is called before the first frame update
    void Start()
    {
        NAME = SetStatus.Instance.GetName(_charaId);
        ATTRIBUTE = SetStatus.Instance.GetAttribute(_charaId);
        if(LV == 1) 
        {
            LevelSet(_firstLv);
        }
        SkillSet();
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

    public void GetExp(int exp) 
    {
        EXP += exp;
        if(EXP >= NEXT_EXP) { LevelSet(LV + 1); Debug.Log("���x���A�b�v!!"); }
    }
}
