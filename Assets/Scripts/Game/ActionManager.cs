using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    [SerializeField]
    PlayerMonsterMove _pmm;

    [SerializeField]
    EnemyMonsterMove _emm;

    public void OnAttack()
    {
        if (_pmm != null)
        {
            PlayerMonsterStatus pms = GetComponent<PlayerMonsterStatus>();

            bool cri = pms.CRI > Random.Range(0, 100) ? true : false;

            if (cri) { GameManager.Instance.CriticalHit(); }

            _pmm._target.GetComponent<ChangeStatus>().AttackDamege(_pmm._nextSkill, pms.ATK, cri);

            _pmm.OnActionEnd();
        }
        else if (_emm != null)
        {
            EnemyMonsterStatus ems = GetComponent<EnemyMonsterStatus>();

            bool cri = ems.CRI > Random.Range(0, 100) ? true : false;

            if (cri) { GameManager.Instance.CriticalHit(); }

            _emm._target.GetComponent<ChangeStatus>().AttackDamege(_emm._nextSkill, ems.ATK, cri);
            _emm.OnActionEnd();
        }
    }
}
