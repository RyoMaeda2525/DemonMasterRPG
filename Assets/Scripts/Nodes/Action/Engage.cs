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
        [SerializeField]
        PlayerMonsterCamera _monsterCamera = null;

        [SerializeField, SerializeReference, SubclassSelector] IBehavior Next;

        public Result Action(Environment env)
        {
            if (env.Visit(this))
            {

            }

            GameObject monster = _monsterCamera.CameraMonsterFind(env.viewingDistance);

            if (monster != null)
            {
                env.target = monster;
                return Next.Action(env);
            }


            return Result.Failure;
        }
    }
}
