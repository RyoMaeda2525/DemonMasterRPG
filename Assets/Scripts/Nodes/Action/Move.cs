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
        public float Actionradius = 3;
        [Tooltip("行動の制限距離"), SerializeField]
        public float ActionDistance = 20f;

        /// <summary>行動範囲の中心点</summary>
        private Vector3 _startPosition = Vector3.zero;

        Vector3 randomPos = Vector3.zero;

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

                randomPos = new Vector3(UnityEngine.Random.Range(_startPosition.x - Actionradius, _startPosition.x + Actionradius), 0,
                                                                UnityEngine.Random.Range(_startPosition.z - Actionradius, _startPosition.z + Actionradius));

                NavMesh.SamplePosition(randomPos, out NavMeshHit navMeshHit, 10, 1);

                _nav.destination = navMeshHit.position;
            }

            if ((randomPos - env.mySelf.transform.position).magnitude > _nav.stoppingDistance) { return Result.Running; }

            return Result.Success;
        }
    }

    [Serializable]
    public class TargetMove : IBehavior
    {
        [SerializeField, Tooltip("追いかける対象")]
        public GameObject _target;

        [SerializeField, Tooltip("追いかけるのを止める距離")]
        public Vector3 _targetRange = new Vector3();

        public Result Action(Environment evn) 
        {


            return Result.Success;
        }
    }
}




