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
        [Tooltip("�s���͈͂̔��a"), SerializeField]
        public float Actionradius = 3;
        [Tooltip("�s���̐�������"), SerializeField]
        public float ActionDistance = 20f;

        /// <summary>�s���͈͂̒��S�_</summary>
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
        [SerializeField, Tooltip("�ǂ�������Ώ�")]
        public GameObject _target;

        [SerializeField, Tooltip("�ǂ�������̂��~�߂鋗��")]
        public Vector3 _targetRange = new Vector3();

        public Result Action(Environment evn) 
        {


            return Result.Success;
        }
    }
}




