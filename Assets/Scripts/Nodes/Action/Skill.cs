using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MonsterTree
{
    [Serializable]  
    public class Skill : IBehavior
    {
        public Result Action(Environment env)
        {
            if(env.Visit(this))
            {
                   
            }

            return Result.Success;
        }
    }

    [Serializable]
    public class SkillDecide : IBehavior 
    {
        List<SKILL> _skillList = null;

        public Result Action(Environment env)
        { 
            if (env.Visit(this)) 
            {
                if (_skillList == null) 
                {
                    //_skillList = GameManager.Instance.SkillManager.Skill;
                }
            }

            return Result.Success;
        }
    }
}
