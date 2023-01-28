using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(MonsterCamera))]
public partial class MonsterStatus : MonsterBase
{
    protected override void Start()
    {
        base.Start();
        if (this.CompareTag("PlayerMonster"))
        {
            UiManager.Instance.MonsterPanel.MonsterPanalSet(this);
            Player.Instance.MonsterStatus.Add(this);
        }
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
        if (HP < 0) { HP = 0; }
        UiManager.Instance.MonsterPanel.HpSet(this);
    }

    public void Heal(int healValue)
    {
        HP += healValue;
        if (HP > HPMax) { HP = HPMax; }
        UiManager.Instance.MonsterPanel.HpSet(this);
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
        else GameManager.Instance.GainExp(EXP);

        Player.Instance.ExitDetectObject(this.gameObject);
    }
}
