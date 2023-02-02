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
        [Tooltip("行動範囲の半径"), SerializeField]
        public float _actionRadius = 3;
        [Tooltip("行動の制限距離"), SerializeField]
        public float _actionDistance = 20f;

        /// <summary>行動範囲の中心点</summary>
        private Vector3 _startPosition = Vector3.zero;

        Vector3 _randomPos = Vector3.zero;

        NavMeshAgent _nav;

        public Result Action(Environment env)
        {
            if (env.Visit(this))
            {
                if (_startPosition == Vector3.zero)
                {
                    _startPosition = env.mySelf.transform.position;
                    _nav = env.mySelf.GetComponent<NavMeshAgent>();
                }

                _randomPos = new Vector3(UnityEngine.Random.Range(_startPosition.x - _actionRadius, _startPosition.x + _actionRadius), 0,
                                                                UnityEngine.Random.Range(_startPosition.z - _actionRadius, _startPosition.z + _actionRadius));

                NavMesh.SamplePosition(_randomPos, out NavMeshHit navMeshHit, 10, 1);

                _nav.destination = navMeshHit.position;
            }

            if ((_randomPos - env.mySelf.transform.position).magnitude > _nav.stoppingDistance) { return Result.Running; }

            return Result.Success;
        }
    }

    [Serializable]
    public class TargetMove : IBehavior
    {
        [SerializeField, SerializeReference, SubclassSelector]
        IBehavior Next;

        /// <summary>アニメーション用のパラメータ</summary>
        float navSpeed;

        GameObject _target;

        NavMeshAgent _nav = null;

        public Result Action(Environment env)
        {
            if (env.Visit(this))
            {
                if (_nav == null) 
                {
                    _nav = env.mySelf.GetComponent<NavMeshAgent>();
                }

                _target = env.target;
            }

            if (_target != env.target) { Debug.Log("TargetMove Failure");  return Result.Failure; }
            env.mySelf.transform.LookAt(_target.transform.position);
            _nav.destination = _target.transform.position;
            navSpeed = _nav.velocity.magnitude;
            env.aniController.SetFloat("NavSpeed", navSpeed);

            if ((_target.transform.position - env.mySelf.transform.position).magnitude < _nav.stoppingDistance)
            {
                env.aniController.SetFloat("NavSpeed", 0);
                env.Leave(this);
                Debug.Log("TargetMove Success");
                return Next.Action(env);           
            }
            else 
            {
                Debug.Log("TargetMove Running");
                return Result.Running;
            }
        }
    }
}




