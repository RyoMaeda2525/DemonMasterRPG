using MonsterTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(MonsterCamera))]
[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(MoveTree))]
public partial class MonsterStatus : MonsterBase
{
    AnimationController _controller;

    MoveTree _moveTree;

    Environment _env;

    private UiManager UiManager => UiManager.Instance;

    protected override void Awake()
    {
        base.Awake();
        _controller = GetComponent<AnimationController>();
        _moveTree = GetComponent<MoveTree>();
        _env = _moveTree.Environment;
        if (this.CompareTag("PlayerMonster"))
        {
            Player.Instance.MonsterStatus.Add(this);
            UiManager.MonsterPanel.MonsterPanalSet(this);
            EXP = 0;
        }
        else { base.ExpSet(); }
    }

    public void TacticsSet(TacticsClass tactics) 
    {
        _tactics = tactics;
        _moveTree.ChangeTactics(_tactics.tactics_id);
    }

    public void UseSkillCost(int cost) 
    {
        MP -= cost;
        if (CompareTag("PlayerMonster"))
            UiManager.MonsterPanel.MpSet(this);
    }

    public void AttackDamage(int atk, bool cri , MonsterStatus attaker)
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

        if(CompareTag("PlayerMonster"))
        UiManager.MonsterPanel.HpSet(this);
    }

    public void Heal(int healValue)
    {
        HP += healValue;
        if (HP > HPMax) { HP = HPMax; }
        if (CompareTag("PlayerMonster"))
            UiManager.MonsterPanel.HpSet(this);
    }

    public void Deth()
    {
        gameObject.SetActive(false);

        if (CompareTag("PlayerMonster"))
        {
            foreach (var pms in Player.Instance.MonsterStatus)
            {
                if (pms.gameObject.activeSelf) { return; }
            }
            GameManager.Instance.GameOver();
        }
        else 
        {
            GameManager.Instance.GainExp(EXP);
            Player.Instance.ExitDetectObject(this.gameObject);
        } 
    }

    public void OnAttack() 
    {
        float hoge = UnityEngine.Random.Range(0f, 100f);
        bool cri = CRI > hoge ? true : false;
        _env.target.AttackDamage(ATK, cri , this);
    }
}
