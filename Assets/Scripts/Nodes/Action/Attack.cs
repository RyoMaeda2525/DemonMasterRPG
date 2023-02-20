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

        const float _actionInterval = 4;

        public Result Action(Environment env)
        {
            if (env.Visit(this))
            {
                _timer = 0;
                _target = env.target;
            }

            if (_target != env.target || _target == null) 
            {
                env.Leave(this);
                return Result.Failure; 
            }

            env.mySelf.transform.LookAt(_target.transform);

            _timer += Time.deltaTime;

            if (_timer > _actionInterval)
            {
                env.aniController.Action("Attack" , 1);

                _timer = 0;
                env.Leave(this);
                return Result.Success;
            }
            else 
            {
                return Result.Running;
            }
        }
    }
}
