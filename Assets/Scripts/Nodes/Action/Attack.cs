using MonsterTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterTree
{
    [Serializable]
    public class Attack : IBehavior
    {
        float _timer = 0;

        MonsterStatus _target;

        float _actionInterval = 4;

        public Result Action(Environment env)
        {
            if (env.Visit(this)) 
            { 
                _timer = 0;
                _target = env.target;
            }

            if (_target != env.target) 
            {
                env.Leave(this);
                return Result.Failure; 
            }

            env.mySelf.transform.LookAt(_target.transform);

            _timer += Time.deltaTime;

            if (_timer > _actionInterval)
            {
                //float hoge = UnityEngine.Random.Range(0f, 100f);

                //bool cri = env.status.Cri > hoge ? true : false;

                //_targetStatus.AttackDamage(env.status.Atk, cri, env.target);
                
                env.aniController.Action("Attack" , 1);

                _timer = 0;
                env.Leave(this);
                Debug.Log("Attack Success");
                return Result.Success;
            }
            else 
            {
                Debug.Log("Attack Running");
                return Result.Running;
            }
        }
    }
}
