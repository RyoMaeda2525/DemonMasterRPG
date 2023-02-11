using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Status
{
    /// <summary>���x��</summary>
    public int LV;
    /// <summary>���O</summary>
    public string NAME;
    /// <summary>����</summary>
    public Attribute ATTRIBUTE;
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
    /// <summary>Critical,�N���e�B�J���̔�����</summary>
    public int LUK;
    /// <summary>���̃��x���܂łɕK�v�Ȍo���l</summary>
    public int NEXT_EXP;
    /// <summary>�|�������ɖႦ��o���l</summary>
    public int ENEMY_EXP;
}

[System.Serializable]
public class MonsterSkill 
{
    //�g�p�ł���X�L��
    public SkillAssets Skill;

    //�g�p�ł��郌�x��
    public int LearnLv;
}

[CreateAssetMenu]
public class StatusSheet : ScriptableObject
{
    public new string name;

    public MonsterSkill[] skills;

    public Status[] status;

    public StatusSheet(StatusSheet statusSheet) 
    {
        this.name = statusSheet.name;
        this.skills = statusSheet.skills;
        this.status = statusSheet.status;
    }
}