using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

namespace MonsterTree
{
    /// <summary>
    /// –Ú‚Ì‘O‚Éƒ‚ƒ“ƒXƒ^[‚ª‚¢‚é‚©‚Ç‚¤‚©
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
                Debug.Log("Šù‚É“G‚ª‚¢‚é");
                return Result.Success;
            }

            GameObject monster = _camera.CameraMonsterFind(env.viewingDistance);

            if (monster != null)
            {
                Debug.Log("Ú“G");
                env.target = monster;
                return Result.Success;
            }

            Debug.Log("“G‚È‚µ");
            return Result.Failure;
        }
    }
}
