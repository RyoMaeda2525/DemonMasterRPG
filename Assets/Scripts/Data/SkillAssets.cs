using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[Serializable]
/// <summary>
/// �X�L���̌��ʂ��i�[����\����
/// </summary>
public class Skill_Type
{
    /// <summary>�U���X�L����񕜃X�L���Ȃǂ̎��</summary>
    public Effect_Type effect_type;
    /// <summary>�X�L���ŗ^��������ʗ�</summary>
    public int effect_value;
    /// <summary>�X�L���̑Ώۂ̎��</summary>
    public Effect_Target target_type;
    /// <summary>�X�L���ŏ����R�X�g</summary>
    public int effect_cost;
}

[Serializable]
/// <summary>
/// �X�L���̏����i�[����\����
/// </summary>
public class SKILL
{
    //public int skill_flag;�@//�K�����Ă��邩�̔���p
    public int skill_id;�@�@//�X�L����ID
    public string skill_name; //�X�L���̖��O
    public string skill_info; //�X�L���̐���
    public Attribute skill_attribute; //�����̎��(������,�Α���)
    public List<Skill_Type> skill_type; //Skill_Type�̌��ʂ��i�[����List
}

[CreateAssetMenu]
public class SkillAssets : ScriptableObject
{
    public SKILL _skill = default;

    public SkillAssets(SkillAssets skillManager) 
    {
        this._skill = skillManager._skill;
    }
}

public enum Attribute 
{
    Fire,
    Wood,
    Holy
}

public enum Effect_Type 
{
    Damage,
    Heal,
    AtkBuff,
    DefBuff
}

public enum Effect_Target
{
    Mine,
    One,
    Team,
    TeamMember
}
