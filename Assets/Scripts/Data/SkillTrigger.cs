using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TriggerCondition 
{
    /// <summary>コンディション</summary>
    public Condition condition;

    /// <summary>以上か以下か</summary>
    public TriggerUpDown upDown;

    public float value;

    public Effect_Type effect_Type;

    public Effect_Target effect_Target;

    /// <summary>0で下から、1で上のスキルを優先的に使用</summary>
    public int skillGrade = 0;
}

[CreateAssetMenu]
public class SkillTrigger : ScriptableObject
{
    /// <summary>作戦名</summary>
    public new string name;
    /// <summary>発動条件</summary>
    public TriggerCondition[] triggers;

    public SkillTrigger(SkillTrigger skillTrigger)
    {
        this.name = skillTrigger.name;
        this.triggers = skillTrigger.triggers;
    }
}

public enum Condition
{
    Default,
    MyHp,
    MemberHp,
    TeamHp,
    TargetDistance,
}

public enum TriggerUpDown 
{
    Up , Down
}