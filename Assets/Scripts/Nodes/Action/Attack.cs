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

        float _actionInterval = 4;

        public Result Action(Environment env)
        {
            if (env.Visit(this)) { _timer = 0; }

            _timer += Time.deltaTime;

            if (_timer > _actionInterval) 
            {
                float hoge = UnityEngine.Random.Range(0f , 100f);

                bool cri =  env.status.Cri > hoge? true : false;

                env.target.GetComponent<MonsterStatus>().
                    AttackDamage(env.status.Atk , cri , env.target);

                return Result.Success;
            }

            return Result.Running;
        }
    }
}
