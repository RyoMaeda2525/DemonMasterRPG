using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

namespace MonsterTree
{
    /// <summary>
    /// 目の前にモンスターがいるかどうか
    /// </summary>
    [Serializable]
    public class Discover : IBehavior
    {
        [SerializeField, SerializeReference, SubclassSelector] IBehavior Next;

        MonsterCamera _camera = null;

        public Result Action(Environment env)
        {
            if (env.Visit(this))
            {
                if (_camera == null) { _camera = env.mySelf.GetComponent<MonsterCamera>(); }
            }

            if (env.target != null) 
            {
                return Next.Action(env);
            }

            GameObject monster = _camera.CameraMonsterFind(env.status.ViewingDistance);

            if (monster != null)
            {
                env.target = monster.GetComponent<MonsterStatus>();
                return Result.Success;
            }

            return Result.Failure;
        }
    }

    public class PlayerTarget : IBehavior
    {
        [SerializeField, SerializeReference, SubclassSelector] IBehavior Next;

        public Result Action(Environment env) 
        {
            MonsterStatus playerTarget = Player.Instance._target;

            if (env.target != null)
            {
                if (env.target.CompareTag("EnemyMonster") && playerTarget == env.target
                    || env.target.CompareTag("EnemyMonster") && playerTarget == null)
                {
                    return Next.Action(env);
                }
            }
            else if(playerTarget != null) 
            {
                env.target = playerTarget;
                return Next.Action(env);
            }


            return Result.Failure;
        }
    }
}
