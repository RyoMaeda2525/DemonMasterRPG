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
        [Tooltip("行動範囲の中心点"), SerializeField]
        public Vector3 startPosition = default;
        [Tooltip("行動範囲の半径"), SerializeField]
        public float Actionradius = 3;
        [Tooltip("行動の制限距離"), SerializeField]
        public float ActionDistance = 20f;

        NavMeshAgent _nav;

        public Result Action(Environment env)
        {
            NavMeshHit navMeshHit;

            Vector3 randomPos = new Vector3(UnityEngine.Random.Range(startPosition.x - Actionradius, startPosition.x + Actionradius), 0,
                                                            UnityEngine.Random.Range(startPosition.z - Actionradius, startPosition.z + Actionradius));
            NavMesh.SamplePosition(randomPos, out navMeshHit, 10, 1);
            _nav.SetDestination(navMeshHit.position);

            return Result.Success;
        }
    }

    [Serializable]
    public class TargetMove 
    {
        public Result Action(Environment evn) 
        {
            return Result.Success;
        }
    }
}




