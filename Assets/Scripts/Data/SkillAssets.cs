using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[Serializable]
/// <summary>
/// スキルの効果を格納する構造体
/// </summary>
public class Skill_Type
{
    /// <summary>攻撃スキルや回復スキルなどの種類</summary>
    public Effect_Type effect_type;
    /// <summary>スキルで与えられる効果量</summary>
    public int effect_value;
    /// <summary>スキルの対象の種類</summary>
    public Effect_Target target_type;
    /// <summary>スキルで消費するコスト</summary>
    public int effect_cost;
}

[Serializable]
/// <summary>
/// スキルの情報を格納する構造体
/// </summary>
public class SKILL
{
    //public int skill_flag;　//習得しているかの判定用
    public int skill_id;　　//スキルのID
    public string skill_name; //スキルの名前
    public string skill_info; //スキルの説明
    public Attribute skill_attribute; //属性の種類(無属性,火属性)
    public List<Skill_Type> skill_type; //Skill_Typeの効果を格納するList
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
