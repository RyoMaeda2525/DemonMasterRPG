using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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

    /// <summar>所持しているモンスターを保持するためのリスト</summar>
    List<PlayerMonsterStatus> _pms = default;

    Tactics[] _tacticsArray = default;

    void SetTactics(int[] tacticsNumber)
    {
        _tacticsArray = TacticsManager.Instance.TacticsSet(tacticsNumber);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("HorizontalKey");              // 矢印キーの水平軸をhで定義
        float v = Input.GetAxis("VerticalKey");                // 矢印キーの垂直軸をvで定義

        if (h != 0 || v != 0)
        {
            ChangeTactics(h, v);
        }
    }

    void ChangeTactics(float h , float v)
    {
        int i = -1;
        if (h > 0) { i = 0; }
        else if (v > 0) { i = 1; }
        else if (h < 0) { i = 2; }
        else if (v < 0) { i = 3; }

        if(i == -1) { Debug.Log("Error"); return; }

        foreach (var monster in _pms) 
        {
            monster.TacticsSet(_tacticsArray[i]);
        }
    }
}
