using MonsterTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonsterCamera))]
[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(MoveTree))]
public partial class MonsterStatus : MonsterBase
{
    int _charaId = 0;

    [SerializeField , Header("ヒエラルキーに置く場合はキャラクターシートが必要")]
    CharacterSheet _characterSheet;

    [SerializeField, Header("ヒエラルキーに置く場合は初期レベルの設定が必要")] 
    int _firestLevel = 1;

    AnimationController _controller;

    MoveTree _moveTree;

    Environment _env;

    /// <summary>プレイヤー視点でのindex</summary>
    int monsterIndex;

    //直接ではなくMVPパターンにしたい
    MonsterPanelManger PanelManger => UiManager.Instance.MonsterPanel;

    protected override void Setup(CharacterSheet sheet, int lv)
    {
        statusSheet = sheet.Sheet;
        _charaId = sheet.Id;
        base.Setup(sheet , lv);
    }

    static public MonsterStatus Create(CharacterSheet sheet, int level) 
    {
        GameObject chara = Instantiate(sheet.Prefab);
        var script = chara.AddComponent<MonsterStatus>();
        script.Setup(sheet , level);
        return script;
    }

    private void Start()
    {
        if (statusSheet == null) { Setup(_characterSheet , _firestLevel); }

        _controller = GetComponent<AnimationController>();
        _moveTree = GetComponent<MoveTree>();
        _env = _moveTree.Env;
        if (this.CompareTag("PlayerMonster"))
        {
            Player.Instance.MonstersStatus.Add(this);
            monsterIndex = Player.Instance.MonstersStatus.IndexOf(this);
            PanelManger.MonsterPanalSet(monsterIndex, statusSheet.image);
            if (LV > 1)
            {
                EXP = status[LV - 2].NEXT_EXP;
            }
            else { EXP = 0; }
        }
        else { base.ExpSet(); }
    }

    void NextLevel()
    {
        if (EXP >= NEXT_EXP)
        {
            LevelSet(LV + 1);
            PanelManger.MonsterPanalSet(monsterIndex, statusSheet.image);
            NextLevel();
        }
    }

    /// <summary>
    /// 攻撃する処理
    /// アニメーションから呼ぶ
    /// </summary>
    public void OnAttack()
    {
        if (_env.target != null)
        {
            float hoge = Random.Range(0f, 100f);
            bool cri = CRI > hoge ? true : false;
            _env.target.AttackDamage(ATK, cri, this);
        }
    }

    /// <summary>味方モンスターならUIのアクションを表示</summary>
    /// <param name="skillName"></param>
    public void ActionDecision(string skillName)
    {
        if (CompareTag("PlayerMonster"))
            PanelManger.ActionTextSet(monsterIndex, skillName);
    }

    /// <summary>味方モンスターならUIのアクションを消す</summary>
    public void ActionEnd()
    {
        if (CompareTag("PlayerMonster"))
            PanelManger.ActionTextDelete(monsterIndex);
    }

    /// <summary>
    /// 作戦を受け取る
    /// </summary>
    /// <param name="tactics"></param>
    public void TacticsSet(TacticsClass tactics)
    {
        _tactics = tactics;
        _moveTree.ChangeTactics(_tactics.tactics_id);
    }

    /// <summary>
    /// スキルのコストを払う
    /// </summary>
    public void UseSkillCost(int cost)
    {
        MP -= cost;
        if (CompareTag("PlayerMonster"))
            PanelManger.MpSet(this);
    }

    /// <summary>
    /// 攻撃を受けたときに呼ぶ
    /// </summary>
    public void AttackDamage(int atk, bool cri, MonsterStatus attaker)
    {
        int damage;
        if (!cri)
        {
            damage = atk / 2 - Def / 4;
            if (damage < 0) { damage = 0; }
        }
        else { damage = atk / 2; }

        HP -= damage;

        if (HP <= 0) { HP = 0; _controller.DethAnimation(); }

        _moveTree.UnderAttack(attaker);

        if (CompareTag("PlayerMonster"))
            PanelManger.HpSet(this);
    }

    /// <summary>
    /// 体力の回復を受けたときに呼ぶ
    /// </summary>
    public void Heal(int healValue)
    {
        HP += healValue;
        if (HP > HPMax) { HP = HPMax; }
        if (CompareTag("PlayerMonster"))
            PanelManger.HpSet(this);
    }

    /// <summary>
    /// モンスターの死亡処理
    /// アニメーションから呼ぶ
    /// </summary>
    public void Deth()
    {
        if (CompareTag("PlayerMonster"))
        {
            PanelManger.MonsterDeth(monsterIndex);

            gameObject.SetActive(false);

            foreach (var pms in Player.Instance.MonstersStatus)
            {
                if (pms.gameObject.activeSelf) { return; }
            }
            GameManager.Instance.GameOver();
        }
        else
        {
            GameManager.Instance.GainExp(EXP);
            Player.Instance.ExitDetectObject(this.gameObject);
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 経験値を取得
    /// </summary>
    /// <param name="exp"></param>
    public void GetExp(int exp)
    {
        EXP += exp;
        NextLevel();
    }
}
