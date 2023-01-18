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
        public Result Action(Environment env)
        {
            if (env.Visit(this)) { }

            

            return Result.Success;
        }
    }
}
