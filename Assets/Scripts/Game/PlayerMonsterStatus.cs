using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMonsterStatus : MonoBehaviour
{
    [SerializeField, Tooltip("Statusなどを取得する際に使用する共通番号")]
    int _charaId = 0;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
