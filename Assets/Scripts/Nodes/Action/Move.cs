using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MonsterTree
{
    [Serializable]
    public class RandomMove : IBehavior
    {
        [SerializeField, Tooltip("行動範囲の半径")]
        private float _actionRadius = 5;

        /// <summary>行動範囲の中心点</summary>
        private Vector3 _startPosition = Vector3.zero;

        Vector3 _randomPos = Vector3.zero;

        float _timer = 0;

        /// <summary>着いた後も留まる用</summary>
        const float _interval = 3f;

        NavMeshAgent _nav;

        public Result Action(Environment env)
        {
            if (env.Visit(this))
            {
                if (_startPosition == Vector3.zero)
                {
                    _startPosition = env.mySelf.transform.position;
                    _nav = env.mySelf.GetComponent<NavMeshAgent>();
                    _nav.stoppingDistance = env.status.AttackDistance;
                }
                if (_timer == 0 || _timer > _interval)
                {
                    _timer = 0;
                    _randomPos = new Vector3(UnityEngine.Random.Range(_startPosition.x - _actionRadius, _startPosition.x + _actionRadius), env.mySelf.transform.position.y,
                                                                    UnityEngine.Random.Range(_startPosition.z - _actionRadius, _startPosition.z + _actionRadius));
                    NavMesh.SamplePosition(_randomPos, out NavMeshHit navMeshHit, 10, 1);
                    _nav.destination = navMeshHit.position;
                }

            }

            _timer += Time.deltaTime;

            if (_timer > _interval)
            {
                env.Leave(this);
                return Result.Success;
            }
            else if ((_randomPos - env.mySelf.transform.position).magnitude > _nav.stoppingDistance)
            {
                return Result.Success;
            }

            return Result.Running;
        }
    }

    [Serializable]
    public class TargetMove : IBehavior
    {
        [SerializeField, SerializeReference, SubclassSelector] IBehavior Next;

        MonsterStatus _target;

        NavMeshAgent _nav = null;

        public Result Action(Environment env)
        {
            if (env.Visit(this))
            {
                if (_nav == null)
                {
                    _nav = env.mySelf.GetComponent<NavMeshAgent>();
                    _nav.stoppingDistance = env.status.AttackDistance;
                }

                _target = env.target;
            }

            if (_target != env.target || _target == null)
            {
                env.Leave(this);
                return Result.Failure;
            }

            env.mySelf.transform.LookAt(_target.transform.position);
            _nav.destination = _target.transform.position;

            if ((_target.transform.position - env.mySelf.transform.position).magnitude < _nav.stoppingDistance)
            {
                env.Leave(this);
                return Next.Action(env);
            }
            else
            {
                return Result.Running;
            }
        }
    }

    [Serializable]
    public class PlayerFollow : IBehavior
    {
        GameObject _player = null;

        NavMeshAgent _nav = null;

        public Result Action(Environment env)
        {
            if (env.Visit(this))
            {
                if (_nav == null)
                {
                    _player = Player.Instance.gameObject;
                    _nav = env.mySelf.GetComponent<NavMeshAgent>();
                    _nav.stoppingDistance = env.status.AttackDistance;
                }
            }

            _nav.SetDestination(_player.transform.position);

            if ((_nav.destination - env.mySelf.transform.position).magnitude <= _nav.stoppingDistance)
            {
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




