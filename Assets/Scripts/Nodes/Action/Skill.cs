using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MonsterTree
{
    [Serializable]
    public class Skill : IBehavior
    {
        float _timer = 0;

        GameObject _target;

        float _actionInterval = 4;

        SkillAssets _skill;

        Effect_Target _skillTarget;

        public Result Action(Environment env)
        {
            if (env.Visit(this))
            {
                _timer = 0;
                _target = env.target;
                _skill = SkillDecide(env.status, env.skillTrigger);
                if (_skill == null)
                {
                    env.Leave(this);
                    return Result.Failure;
                }
                else if (_skillTarget == Effect_Target.One && _skillTarget == Effect_Target.TeamMember) 
                {
                    _target = env.target;
                }
            }

            if (_target != null && _target != env.target)
            {
                env.Leave(this);
                return Result.Failure;
            }

            _timer += Time.deltaTime;

            if (_timer > _actionInterval)
            {


                _timer = 0;
                env.Leave(this);
                Debug.Log($"Magic Success {_skill._skill.skill_name}");
                return Result.Success;
            }
            else
            {
                Debug.Log("Magic Running");
                return Result.Running;
            }
        }


        private SkillAssets SkillDecide(MonsterStatus status, TriggerCondition[] skillTriggers)
        {
            foreach (var skillTrigger in skillTriggers)
            {
                bool trigger = false;

                switch (skillTrigger.condition)
                {
                    case Condition.MyHp:
                        int myHp = status.Hp;

                        if (skillTrigger.upDown == TriggerUpDown.Up && myHp > skillTrigger.value)
                        {
                            trigger = true;
                        }
                        break;

                    case Condition.Default:
                            trigger = true;
                        break;
                }

                if (trigger)
                {
                    List<SkillAssets> skillAssets = status.SkillList;

                    if (skillTrigger.skillGrade > 0)
                    {
                        skillAssets.Sort((a, b) => b._skill.skill_type[0].effect_value 
                                                        - a._skill.skill_type[0].effect_value);
                    }
                    else
                    {
                        skillAssets.Sort((a, b) => a._skill.skill_type[0].effect_value 
                                                        - b._skill.skill_type[0].effect_value);
                    }

                    foreach (var skillAsset in skillAssets)
                    {
                        if (skillAsset._skill.skill_type[0].effect_type == skillTrigger.effect_Type) 
                        {
                            return skillAsset;
                        }
                    }
                }
            }

            return null;
        }
    }
}
