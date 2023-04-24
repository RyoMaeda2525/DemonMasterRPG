using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TriggerCondition 
{
    /// <summary>�R���f�B�V����</summary>
    public Condition condition;
    /// <summary>�ȏォ�ȉ���</summary>
    public TriggerUpDown upDown;
    /// <summary>�����̒l</summary>
    public float value;
    /// <summary>�g�p����X�L���̎��</summary>
    public Effect_Type effect_Type;
    /// <summary>�X�L���̃^�[�Q�b�g</summary>
    public Effect_Target effect_Target;
    /// <summary>0�ŉ�����A1�ŏ�̃X�L����D��I�Ɏg�p</summary>
    public int skillGrade = 0;
}

[CreateAssetMenu]
public class SkillTrigger : ScriptableObject
{
    /// <summary>��햼</summary>
    public new string name;
    /// <summary>��������</summary>
    public TriggerCondition[] triggers;

    public SkillTrigger(SkillTrigger skillTrigger)
    {
        this.name = skillTrigger.name;
        this.triggers = skillTrigger.triggers;
    }
}

/// <summary>�X�L���̔�������</summary>
public enum Condition
{
    Default,
    MyHp,
    MemberHp,
    TeamHp,
    TargetDistance,
}

/// <summary>�����̒l���ォ����</summary>
public enum TriggerUpDown 
{
    Up , Down
}