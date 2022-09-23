using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMonsterStatus : MonoBehaviour
{
    [SerializeField, Tooltip("Statusなどを取得する際に使用する共通番号")]
    int _charaId = 0;

    [SerializeField]
    int _firstLv = 1;

    ///// <summary>使えるスキル</summary>
    [SerializeField, Tooltip("使えるスキル")]
    internal List<SKILL> _skillList;

    /// <summary>与えられた作戦</summary>
    internal TacticsList _tactics = default;

    //純粋なステータス

    /// <summary>レベル</summary>
    public int LV;
    /// <summary>名前</summary>
    public string NAME;
    /// <summary>属性</summary>
    public int ATTRIBUTE;
    /// <summary>コンスティチューション,体力</summary>
    private int CON;
    /// <summary>マジックパワー,魔力</summary>
    private int MAG;
    /// <summary>物理的な力</summary>
    private int STR;
    /// <summary>Vitality,物理的な頑強さ、状態異常への抵抗力</summary>
    private int VIT;
    /// <summary>Intelligence,知力</summary>
    private int INT;
    /// <summary>Evasion,回避率</summary>
    private int EVA;
    /// <summary>Luck , 運</summary>
    private int LUK;

    void LevelSet(int level)
    {
        LV = level;
        StatusSet();
    }
    //------------ステータスへのバフ・デバフ倍率---------------
    /// <summary>ヒットポイント</summary>
    public float HP_Buff = 1.0f;
    /// <summary>マジックポイント</summary>
    public float MP_Buff = 1.0f;
    /// <summary>物理的な攻撃力へ</summary>
    public float ATK_Buff = 1.0f;
    /// <summary>物理的な頑強へ</summary>
    public float DEF_Buff = 1.0f;
    /// <summary>知力</summary>
    public float MAT_Buff = 1.0f;
    /// <summary>回避率</summary>
    public float AVD_Buff = 1.0f;
    /// <summary>Critical,クリティカルの発生率</summary>
    public float CRI_Buff = 1.0f;

    //------------計算後のステータスなど---------------

    /// <summary>ヒットポイント</summary>
    public int HP;
    /// <summary>ヒットポイントの最大値</summary>
    public int HPMax;
    /// <summary>マジックポイント</summary>
    public int MP;
    /// <summary>マジックポイントの最大値</summary>
    public int MPMax;
    /// <summary>物理的な攻撃力へ</summary>
    public int ATK;
    /// <summary>物理的な頑強へ</summary>
    public int DEF;
    /// <summary>知力</summary>
    public int MAT;
    /// <summary>回避率</summary>
    public int AVD;
    /// <summary>Critical,クリティカルの発生率</summary>
    public int CRI;
    /// <summary>持っている経験値の総量</summary>
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
