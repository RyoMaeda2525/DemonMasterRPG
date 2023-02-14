using MonsterTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(MonsterCamera))]
[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(MoveTree))]
public partial class MonsterStatus : MonsterBase
{
    AnimationController _controller;

    MoveTree _moveTree;

    private UiManager UiManager => UiManager.Instance;

    protected override void Start()
    {
        base.Start();
        _moveTree = GetComponent<MoveTree>();
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

    public void AttackDamage(int atk, bool cri, GameObject attacker)
    {
        int damage;
        if (!cri)
        {
            damage = atk / 2 - Def / 4;
            if (damage < 0) { damage = 0; }
        }
        else { damage = atk / 2; }

        HP -= damage;

        Debug.Log(HP);

        if (HP < 0) { HP = 0; _controller.DethAnimation(); }

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
}
