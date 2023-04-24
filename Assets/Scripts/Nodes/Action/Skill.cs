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

        MonsterStatus _target;

        float _actionInterval = 4;

        SkillAssets _skill;

        Effect_Target _skillTarget;

        public Result Action(Environment env)
        {
            if (env.Visit(this))
            {
                _timer = 0;
                _target = env.target;
                SkillDecide skillDecide = new SkillDecide();
                _skill = skillDecide.Decide(env);
                if (_skill == null)
                {
                    env.Leave(this);
                    return Result.Failure;
                }
                else if (_skillTarget == Effect_Target.One && _skillTarget == Effect_Target.TeamMember)
                {
                    _target = env.target;
                }
                env.status.ActionDecision(_skill.name);
            }

            if (_target != null && _target != env.target)
            {
                _skill = null;
                env.Leave(this);
                env.status.ActionEnd();
                return Result.Failure;
            }

            _timer += Time.deltaTime;

            if (_timer > _actionInterval)
            {
                UseSkill(_skill.skill, env);
                env.status.UseSkillCost(_skill.Skill_Type.effect_cost);

                _timer = 0;
                env.Leave(this);
                return Result.Success;
            }
            else
            {
                return Result.Running;
            }
        }

        void UseSkill(SKILL skill, Environment env)
        {
            List<Skill_Type> skills = skill.skill_type;

            foreach (var type in skills)
            {
                switch (type.effect_type)
                {
                    case Effect_Type.Heal:
                        Heal(type, env);
                        break;
                }
            }
        }

        void Heal(Skill_Type type, Environment env)
        {
            switch (type.target_type)
            {
                case Effect_Target.Mine:
                    env.status.Heal(type.effect_value);
                    break;
            }
        }
    }
     
    /// <summary>‰½‚ÌƒXƒLƒ‹‚ðŽg‚¤‚©”»’è‚·‚é</summary>
    public class SkillDecide
    {
        public SkillAssets Decide(Environment env)
        {
            SkillAssets skill = null;

            foreach (var skillTrigger in env.skillTrigger)
            {

                List<SkillAssets> skillAssets = env.status.SkillList;

                if (skillTrigger.skillGrade > 0)
                {
                    skillAssets.Sort((a, b) => b.Skill_Type.effect_value
                                                    - a.Skill_Type.effect_value);
                }
                else
                {
                    skillAssets.Sort((a, b) => a.Skill_Type.effect_value
                                                    - b.Skill_Type.effect_value);
                }

                foreach (var skillAsset in skillAssets)
                {
                    if (skillAsset.Skill_Type.effect_type == skillTrigger.effect_Type)
                    {
                        if (skillAsset.Skill_Type.effect_cost <= env.status.Mp)
                            skill = skillAsset;
                    }
                }

                if (skill != null)
                {

                    switch (skillTrigger.condition)
                    {
                        case Condition.MyHp:
                            if (MyHp(env, skillTrigger)) { return skill; }
                            break;

                        case Condition.MemberHp:
                            if (MemberHp(env, skillTrigger)) { return skill; }
                            break;

                        case Condition.Default:
                            return skill;
                    }
                }
            }
            return null;
        }

        bool MyHp(Environment env, TriggerCondition trigger)
        {
            float hp = env.status.Hp;
            float maxHp = env.status.HpMax;

            if (trigger.upDown == TriggerUpDown.Up)
                return hp / maxHp >= trigger.value ? true : false;
            else
                return hp / maxHp <= trigger.value ? true : false;
        }

        bool MemberHp(Environment env, TriggerCondition trigger)
        {
            List<MonsterStatus> monsters = new List<MonsterStatus>();

            if (env.mySelf.CompareTag("PlayerMonster"))
            {
                monsters = Player.Instance.MonstersStatus;
            }
            else { monsters = Player.Instance.EnemyList; }


            foreach (var monster in monsters)
            {
                int hp = monster.Hp;
                int maxHp = monster.HpMax;

                if (trigger.upDown == TriggerUpDown.Up && hp / maxHp >= trigger.value
                     || trigger.upDown == TriggerUpDown.Down && hp / maxHp <= trigger.value)
                {
                    return true;
                }
            }
            return false;
        }
    }

}