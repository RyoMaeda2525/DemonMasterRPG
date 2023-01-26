using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract class MonsterBase : MonoBehaviour
{
    [SerializeField, Tooltip("Statusなどを取得する際に使用する共通番号")]
    protected int _charaId = 0;

    [SerializeField, Tooltip("Statusなどを取得する際に使用する共通番号")]
    protected int _firstLv = 0;

    #region 元のステータス
    protected string NAME;
    protected int LV;
    protected int ATTRIBUTE;
    protected int CON;
    protected int MAG;
    protected int STR;
    protected int VIT;
    protected int INT;
    protected int EVA;
    protected int LUK;
    #endregion

    #region 元のステータスのプロパティ
    /// <summary>名前</summary>
    public virtual string Name => NAME;
    /// <summary>レベル</summary>
    public virtual int Level => LV;
    /// <summary>属性</summary>
    public virtual int Attribute => ATTRIBUTE;
    /// <summary>コンスティチューション,体力</summary>
    public virtual int Con => CON;
    /// <summary>マジックパワー,魔力</summary>
    public virtual int Mag => MAG;
    /// <summary>物理的な力</summary>
    public virtual int Str => STR;
    /// <summary>Vitality,物理的な頑強さ、状態異常への抵抗力</summary>
    public virtual int Vit => VIT;
    /// <summary>Intelligence,知力</summary>
    public virtual int Int => INT;
    /// <summary>Evasion,回避率</summary>
    public virtual int Eva => Eva;
    /// <summary>Luck , 運</summary>
    public virtual int Luk => LUK;
    #endregion

    #region ステータスへのバフ・デバフ倍率
    /// <summary>ヒットポイント</summary>
    protected float HP_Buff = 1.0f;
    /// <summary>マジックポイント</summary>
    protected float MP_Buff = 1.0f;
    /// <summary>物理的な攻撃力へ</summary>
    protected float ATK_Buff = 1.0f;
    /// <summary>物理的な頑強へ</summary>
    protected float DEF_Buff = 1.0f;
    /// <summary>知力</summary>
    protected float MAT_Buff = 1.0f;
    /// <summary>回避率</summary>
    protected float AVD_Buff = 1.0f;
    /// <summary>Critical,クリティカルの発生率</summary>
    protected float CRI_Buff = 1.0f;
    #endregion

    #region 実際のステータス
    protected int HP;
    protected int HPMax;
    protected int MP;
    protected int MPMax;
    protected int ATK;
    protected int DEF;
    protected int MAT;
    protected int AVD;
    protected int CRI;
    protected int EXP;
    protected int NEXT_EXP;
    #endregion

    #region ステータスのプロパティ
    /// <summary>ヒットポイント</summary>
    public int Hp => HP;
    /// <summary>ヒットポイントの最大値</summary>
    public int HpMax => HPMax;
    /// <summary>マジックポイント</summary>
    public int Mp => MP;
    /// <summary>マジックポイントの最大値</summary>
    public int MpMax => MPMax;
    /// <summary>物理的な攻撃力へ</summary>
    public int Atk => Atk;
    /// <summary>物理的な頑強へ</summary>
    public int Def => DEF;
    /// <summary>知力</summary>
    public int Mat => MAT;
    /// <summary>回避率</summary>
    public int Avd => AVD;
    /// <summary>Critical,クリティカルの発生率</summary>
    public int Cri => CRI;
    /// <summary>持っている経験値の総量</summary>
    public int Exp => Exp;
    /// <summary>次のレベルへの経験値の総量</summary>
    public int NextExp => NEXT_EXP;
    #endregion

    /// <summary>与えられた作戦</summary>
    protected TacticsList _tactics = default;

    /// <summary>与えられた作戦</summary>
    protected List<SKILL> _skillList = new List<SKILL>();


    // Start is called before the first frame update
    protected virtual void  Start()
    {
        NAME = SetStatus.Instance.GetName(_charaId);
        ATTRIBUTE = SetStatus.Instance.GetAttribute(_charaId);
        if (LV == 0)
        {
            LevelSet(_firstLv);
        }
        SkillSet();
    }

    protected void LevelSet(int level)
    {
        LV = level;
        NEXT_EXP = ExpTable.instance.GetNextExp(_charaId, level);
        StatusSet();
        if (LV == _firstLv) { HP = HPMax; MP = MPMax; }
    }

    protected void SkillSet()
    {
        _skillList = MonsterSkill.instance.SkillSet(_charaId, LV);
    }

    protected void TacticsSet(TacticsList tactics)
    {
        _tactics = tactics;
    }

    protected void StatusSet()
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
        if (EXP >= NEXT_EXP) { LevelSet(LV + 1); Debug.Log("レベルアップ!!"); }
    }
}
