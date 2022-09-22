using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStatus : MonoBehaviour
{
    [SerializeField]
    PlayerMonsterStatus _pms = null;

    [SerializeField]
    EnemyMonsterStatus _ems = null;

    public void AttackDamege(SKILL skill , int atk , bool cri)
    {
        if (_pms != null)
        {
            int damage;
            if (!cri)
            {
                damage = atk / 2 - _pms.DEF / 4;
            }
            else { damage = atk / 2; }

            _pms.HP -= damage;
            if(_pms.HP < 0) { GetComponent<Animator>().Play("Deth"); }
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
    }
}
