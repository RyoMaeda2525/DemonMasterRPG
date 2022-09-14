using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Tooltip("使う作戦を得るために取得する")]
    TacticsManager _tM = default;

    /// <summary>ヒットポイント</summary>
    public int HP;
    /// <summary>物理的な攻撃力へのバフ</summary>
    public int ATK_Buff;
    /// <summary>物理的な頑強へのバフ</summary>
    public int DEF_Buff;
    /// <summary>魔法に対しての抵抗力へのバフ</summary>
    public int MDEF_Buff;
    /// <summary>回避率へのバフ</summary>
    public int EVA_Buff;
    /// <summary>クリティカルの発生率へのバフ</summary>
    public int CRI_Buff;
    /// <summary>持っている経験値の総量</summary>
    public int EXP;
    /// <summary>次のレベルへの経験値の総量</summary>
    public int NEXT_EXP;

    Tactics[] _tacticsArray = default;

    void SetTactics(Tactics[] tactics)
    {
        _tacticsArray = tactics;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
