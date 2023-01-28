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
                if (_camera == null) { env.mySelf.GetComponent<MonsterCamera>(); }
            }

            GameObject monster = _camera.CameraMonsterFind(env.viewingDistance);

            if (monster != null)
            {
                env.target = monster;
                return Next.Action(env);
            }


            return Result.Failure;
        }
    }
}
