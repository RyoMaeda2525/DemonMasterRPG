using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>
{
    [SerializeField, Tooltip("作戦を入れる仮配列")]
    private int[] _tacticsSetArray;

    /// <summary>ヒットポイント</summary>
    private int HP;
    /// <summary>物理的な攻撃力へのバフ</summary>
    private int ATK_Buff;
    /// <summary>物理的な頑強へのバフ</summary>
    private int DEF_Buff;
    /// <summary>魔法に対しての抵抗力へのバフ</summary>
    private int MDEF_Buff;
    /// <summary>回避率へのバフ</summary>
    private int EVA_Buff;
    /// <summary>クリティカルの発生率へのバフ</summary>
    private int CRI_Buff;
    /// <summary>持っている経験値の総量</summary>
    private int EXP;
    /// <summary>次のレベルへの経験値の総量</summary>
    private int NEXT_EXP;

    /// <summar>所持しているモンスターを保持するためのリスト</summar>
    public List<PlayerMonsterStatus> _pms = new List<PlayerMonsterStatus>();

    private bool _actionBool = false;

    private TacticsList[] _tacticsArray;

    void SetTactics(int[] tacticsNumber)
    {
        _tacticsArray = TacticsManager.Instance.TacticsSet(tacticsNumber);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetTactics(_tacticsSetArray);
        foreach (var tactics in _tacticsArray) { Debug.Log(tactics.tactics_name); }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("HorizontalKey");              // 矢印キーの水平軸をhで定義
        float v = Input.GetAxis("VerticalKey");                // 矢印キーの垂直軸をvで定義

        if (!_actionBool && h != 0 || !_actionBool && v != 0)
        {
            ChangeTactics(h, v);
        }
    }

    void ChangeTactics(float h, float v)
    {
        StartCoroutine(ActionStop(3.0f));

        int i = -1;
        if (h > 0) { i = 0; }
        else if (v > 0) { i = 1; }
        else if (h < 0) { i = 2; }
        else if (v < 0) { i = 3; }

        if (i == -1) { Debug.Log("Error01"); return; }

        if (_pms.Count == 0) { Debug.Log("Error02"); return; }

        Debug.Log($"{_tacticsArray[i].tactics_id} {_tacticsArray[i].tactics_name} {_tacticsArray[i].tactics_info} {_tacticsArray[i].tactics_type}");

        foreach (var monster in _pms)
        {
            monster.TacticsSet(_tacticsArray[i]);
        }
    }

    private IEnumerator ActionStop(float waitTime)
    {
        _actionBool = true;
        yield return new WaitForSeconds(waitTime);
        _actionBool = false;
    }
}
