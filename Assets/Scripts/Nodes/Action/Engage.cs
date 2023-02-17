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
        MonsterCamera _camera = null;

        public Result Action(Environment env)
        {
            if (env.Visit(this))
            {
                if (_camera == null) { _camera = env.mySelf.GetComponent<MonsterCamera>(); }
            }

            if (env.target != null) 
            {
                Debug.Log("既に敵がいる");
                return Result.Success;
            }

            GameObject monster = _camera.CameraMonsterFind(env.viewingDistance);

            if (monster != null)
            {
                Debug.Log("接敵");
                env.target = monster.GetComponent<MonsterStatus>();
                return Result.Success;
            }

            Debug.Log("敵なし");
            return Result.Failure;
        }
    }
}
