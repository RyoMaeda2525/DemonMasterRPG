using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Status
{
    /// <summary>レベル</summary>
    public int LV;
    /// <summary>コンスティチューション,体力</summary>
    public int CON;
    /// <summary>マジックパワー,魔力</summary>
    public int MAG;
    /// <summary>物理的な力</summary>
    public int STR;
    /// <summary>Vitality,物理的な頑強さ、状態異常への抵抗力</summary>
    public int VIT;
    /// <summary>Intelligence,知力</summary>
    public int INT;
    /// <summary>Evasion,回避率</summary>
    public int EVA;
    /// <summary>Critical,クリティカルの発生率</summary>
    public int LUK;
    /// <summary>次のレベルまでに必要な経験値</summary>
    public int NEXT_EXP;
    /// <summary>倒した時に貰える経験値</summary>
    public int ENEMY_EXP;
}

[System.Serializable]
public class MonsterSkill 
{
    //使用できるスキル
    public SkillAssets Skill;

    //使用できるレベル
    public int LearnLv;
}

[CreateAssetMenu]
public class StatusSheet : ScriptableObject
{
    [Header("属性")]
    public Attribute Attribute;

    [Header("画像")]
    public Sprite image;

    /// <summary>攻撃射程</summary>
    public float attackDistance;
    /// <summary>視覚距離</summary>
    public float viewingDistance;
    /// <summary>移動スピード</summary>
    public float walkSpeed
;
    public MonsterSkill[] skills;

    public Status[] status;

    public StatusSheet(StatusSheet statusSheet) 
    {
        this.name = statusSheet.name;
        this.Attribute = statusSheet.Attribute;
        this.image = statusSheet.image;
        this.attackDistance = statusSheet.attackDistance;
        this.viewingDistance = statusSheet.viewingDistance;
        this.skills = statusSheet.skills;
        this.status = statusSheet.status;
    }
}