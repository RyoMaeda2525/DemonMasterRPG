using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MonsterTree
{
    /// <summary>
    /// 目の前にモンスターがいるかどうか
    /// </summary>
    [Serializable]
    public class Discover : IBehavior
    { 
        [SerializeField]
        PlayerMonsterCamera _monsterCamera = null;

        [SerializeField]
        PlayerMonsterMove _monsterMove = null;

        [SerializeField, SerializeReference, SubclassSelector] IBehavior Next;

        Player _player = null;

        public Result Action(Environment env)
        {
            if (_player == null) 
            {
                _player = Player.Instance;
            }

            if (_player._emmList.Count > 0)
            {
                foreach (var monster in _player._emmList)
                {
                    if (_monsterCamera.CameraEnemyFind(monster.gameObject)) 
                    {
                        _monsterMove.ContactEnemy(monster.gameObject);
                        return Next.Action(env);
                    }
                }
            }
            else {  }
            return Result.Failure;
        }
    }
}
