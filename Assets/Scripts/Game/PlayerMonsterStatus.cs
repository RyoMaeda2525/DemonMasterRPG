using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMonsterStatus : MonoBehaviour
{
    [SerializeField, Tooltip("Status�Ȃǂ��擾����ۂɎg�p���鋤�ʔԍ�")]
    int _charaId = 0;

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
    /// <summary>Resist,���@�ɑ΂��Ă̒�R��</summary>
    public int RES;
    /// <summary>Intelligence,�m��</summary>
    public int INT;
    /// <summary>Evasion,���</summary>
    public int EVA;
    /// <summary>Luck , �^</summary>
    public int LUK;

    void LevelSet(int level)
    {
        LV = level;
        StatusSet();
    }

    //------------�v�Z��̃X�e�[�^�X�Ȃ�---------------

    /// <summary>�q�b�g�|�C���g</summary>
    public int HP;
    /// <summary>�}�W�b�N�|�C���g</summary>
    public int MP;
    /// <summary>�����I�ȍU���͂�</summary>
    public int ATK;
    /// <summary>�����I�Ȋ拭��</summary>
    public int DEF;
    /// <summary>���@�ɑ΂��Ă̒�R��</summary>
    public int MDEF;
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
        SkillSet();
        LevelSet(1);
    }

    private void SkillSet() 
    {
        _skillList = MonsterSkill.instance.SkillSet(_charaId, LV);
        foreach(SKILL skill in _skillList) 
        {
            Debug.Log($"id {skill.skill_id} name {skill.skill_name} info {skill.skill_info} attribute {skill.skill_attribute}");
        }
    }

    public void TacticsSet(TacticsList tactics) 
    {
        _tactics = tactics;
    }

    void StatusSet()
    {
        int[] setStatus = SetStatus.instance.GetStatus(_charaId , LV);
        CON = setStatus[0];
        MAG = setStatus[1];
        STR = setStatus[2];
        VIT = setStatus[3];
        RES = setStatus[4];
        INT = setStatus[5];
        EVA = setStatus[6];
        CRI = setStatus[7];
    }
}
