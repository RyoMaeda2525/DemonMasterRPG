using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMonsterStatus : MonoBehaviour
{
    [SerializeField, Tooltip("Statusなどを取得する際に使用する共通番号")]
    int _charaId = 0;

    //純粋なステータス

    /// <summary>レベル</summary>
    public int LV;
    /// <summary>名前</summary>
    public string NAME;
    /// <summary>属性</summary>
    public int ATTRIBUTE;
    /// <summary>コンスティチューション,体力</summary>
    public int CON;
    /// <summary>マジックパワー,魔力</summary>
    public int MAG;
    /// <summary>物理的な力</summary>
    public int STR;
    /// <summary>Vitality,物理的な頑強さ、状態異常への抵抗力</summary>
    public int VIT;
    /// <summary>Resist,魔法に対しての抵抗力</summary>
    public int RES;
    /// <summary>Intelligence,知力</summary>
    public int INT;
    /// <summary>Evasion,回避率</summary>
    public int EVA;
    /// <summary>Critical,クリティカルの発生率</summary>
    public int CRI;

    void LevelSet(int level)
    {
        LV = level;
    }

    //------------計算後のステータスなど---------------

    /// <summary>ヒットポイント</summary>
    public int HP;
    /// <summary>マジックポイント</summary>
    public int MP;
    /// <summary>物理的な攻撃力へのバフ</summary>
    public int ATK_Buff;
    /// <summary>物理的な頑強へのバフ</summary>
    public int DEF_Buff;
    /// <summary>魔法に対しての抵抗力へのバフ</summary>
    public int MDEF_Buff;
    /// <summary>知力へのバフ</summary>
    public int INT_Buff;
    /// <summary>回避率へのバフ</summary>
    public int EVA_Buff;
    /// <summary>クリティカルの発生率へのバフ</summary>
    public int CRI_Buff;
    /// <summary>持っている経験値の総量</summary>
    public int EXP;
    /// <summary>次のレベルへの経験値の総量</summary>
    public int NEXT_EXP;

    Tactics _tactics = default;

    // Start is called before the first frame update
    void Start()
    {
        NAME = SetStatus.Instance.GetName(_charaId);
        ATTRIBUTE = SetStatus.Instance.GetAttribute(_charaId);
    }

    public void TacticsSet(Tactics tactics) 
    {
        _tactics = tactics;
    }

    void NextLevelUP()
    {
        
    }
}
