using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStatus : MonoBehaviour
{
    [SerializeField]
    PlayerMonsterStatus _pms = null;

    [SerializeField]
    EnemyMonsterStatus _ems = null;

    public void AttackDamege(SKILL skill , int atk , bool cri , GameObject attacker)
    {
        if (_pms != null)
        {
            int damage;
            if (!cri)
            {
                damage = atk / 2 - _pms.DEF / 4;
                if(damage < 0) { damage = 0; }
            }
            else { damage = atk / 2; }

            _pms.HP -= damage;
            if (_pms.HP < 0) { _pms.HP = 0; }
            MonsterPanelManger.Instance.HpSet(_pms);

            if(_pms.HP == 0) { GetComponent<Animator>().Play("Deth"); }
        }
        else if (_ems != null)
        {
            int damage;
            if (!cri)
            {
                damage = atk / 2 - _ems.DEF / 4;
            }
            else { damage = atk / 2; }

            _ems.HP -= damage;
            if (GetComponent<EnemyMonsterMove>()._target == null) 
            {
                GetComponent<EnemyMonsterMove>()._target = attacker;
            }
            if (_ems.HP < 0) { GetComponent<Animator>().Play("Deth"); }
        }
    }

    public void SkillDamege()
    {
        if (_pms != null)
        {

        }
        else if (_ems != null)
        {

        }
    }

    public void Heal()
    {
        if (_pms != null)
        {

        }
        else if (_ems != null)
        {

        }
    }

    public void Buff()
    {
        if (_pms != null)
        {

        }
        else if (_ems != null)
        {

        }
    }

    void Debuff() 
    {
        if (_pms != null)
        {

        }
        else if (_ems != null)
        {

        }
    }

    /// <summary>HPÇ™0Ç…Ç»ÇËÉ_ÉEÉìÇµÇΩéûÇÃèàóù</summary>
    public void Deth()
    {
        gameObject.SetActive(false);

        if (_pms != null)
        {
            foreach (var pms in Player.Instance._pms)
            {
                if (pms.gameObject.activeSelf) { return; }
            }
            GameManager.Instance.GameOver();
        }
        else GameManager.Instance.GainExp(_ems.EXP);
    }
}
