using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace MonsterTree
{
    /// <summary>ランダムに移動するノード</summary>
    [Serializable]
    public class RandomMove : IBehavior
    {
        [SerializeField, Tooltip("行動範囲の半径")]
        float _actionRadius = 5;

        /// <summary>行動範囲の中心点</summary>
        Vector3 _startPosition = Vector3.zero;

        Vector3 _randomPos = Vector3.zero;

        float _timer = 0;

        /// <summary>着いた後も留まる用</summary>
        const float _interval = 5f;

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

    /// <summary>目標に移動するノード</summary>
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
                }
                _target = env.target;
            }

            if (_target != env.target || _target == null)
            {
                env.Leave(this);
                return Result.Failure;
            }

            if ((int)(_target.transform.position - env.mySelf.transform.position).magnitude <= _nav.stoppingDistance)
            {
                env.Leave(this);
                return Next.Action(env);
            }
            else
            {
                Vector3 targetTransform = new Vector3(_target.transform.position.x, env.mySelf.transform.position.y, _target.transform.position.z);
                env.mySelf.transform.LookAt(targetTransform);
                _nav.speed = env.status.WalkSpeed;
                _nav.stoppingDistance = env.status.AttackDistance;
                _nav.destination = _target.transform.position;

                return Result.Running;
            }
        }
    }

    /// <summary>プレイヤーを追従するノード</summary>
    [Serializable]
    public class PlayerFollow : IBehavior
    {
        GameObject _followTarget = null;

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

                if (env.followTarget != null)
                {
                    _followTarget = env.followTarget;
                }
                else 
                {
                    env.followTarget = Player.Instance.FollowGameObject;
                    _followTarget = env.followTarget;
                }
                
            }

            Debug.Log(_followTarget);

            Vector3 postion = new Vector3(_followTarget.transform.position.x, env.mySelf.transform.position.y, _followTarget.transform.position.z);
            _nav.speed = env.status.WalkSpeed;
            _nav.stoppingDistance = env.status.AttackDistance;
            _nav.SetDestination(postion);

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

    [Serializable]
    public class AttackMove : IBehavior
    {
        NavMeshAgent _nav = null;

        float _attackDistance;

        Vector3 _targetPosition;

        public Result Action(Environment env)
        {
            if (env.Visit(this))
            {
                if (_nav == null)
                {
                    _nav = env.mySelf.GetComponent<NavMeshAgent>();
                    _attackDistance = env.status.AttackDistance;
                }

                _targetPosition = destination(env);
            }

            //env.mySelf.transform.DOMove(_targetPosition, 1f);

            //if ((_targetPosition - env.target.transform.position).magnitude > _attackDistance)
            //{
            //    _targetPosition = destination(env);
            //}

            _nav.stoppingDistance = 2f;
            _nav.SetDestination(_targetPosition);
            return Result.Running;
        }


        private Vector3 destination(Environment env)
        {
            Vector3 targetPosition;

            Transform targetTransform = env.target.transform;

            var myTransform = env.mySelf.transform;

            //float _period;

            //if (UnityEngine.Random.Range(0, 1) >= 1)
            //{
            //    _period = UnityEngine.Random.Range(0, 20);
            //}
            //else { _period = UnityEngine.Random.Range(-20, 0); }

            //// 回転のクォータニオン作成
            //var angleAxis = Quaternion.AngleAxis(360f / _period * Time.deltaTime, Vector3.up);

            //float x, z;

            //float distance = env.status.AttackDistance - 1;

            //if (myTransform.position.x - targetTransform.position.x > 0)
            //{
            //    x = UnityEngine.Random.Range(myTransform.position.x, targetTransform.position.x);
            //}
            //else
            //{
            //    x = UnityEngine.Random.Range(myTransform.position.x, targetTransform.position.x);
            //}

            //if (myTransform.position.z - targetTransform.position.z > 0)
            //{
            //    z = UnityEngine.Random.Range(myTransform.position.z, targetTransform.position.z);
            //}
            //else
            //{
            //    z = UnityEngine.Random.Range(myTransform.position.z, targetTransform.position.z);
            //}

            //// 円運動の位置計算
            //var pos = new Vector3(x, myTransform.position.y, z);

            ////pos -= targetTransform.position;

            //pos = angleAxis * pos;

            ////pos += targetTransform.position;
            ///

            Vector3 centerPoint = Vector3.Lerp(targetTransform.position, myTransform.position, 0.5f);

            float distance = env.status.AttackDistance / 2;

            // 指定された半径の円内のランダム位置を取得
            var circlePos = distance * UnityEngine.Random.insideUnitCircle;

            // XZ平面で指定された半径、中心点の円内のランダム位置を計算
            var spawnPos = new Vector3(
                circlePos.x, 0, circlePos.y
            ) + centerPoint;

            targetPosition = new Vector3(spawnPos.x, myTransform.position.y, spawnPos.z);
            return targetPosition;
        }


    }
}




