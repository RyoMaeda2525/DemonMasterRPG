using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

namespace MonsterTree
{
    /// <summary>
    /// �ڂ̑O�Ƀ����X�^�[�����邩�ǂ���
    /// </summary>
    [Serializable]
    public class Discover : IBehavior
    {
        MonsterCamera _camera = null;

        public Result Action(Environment env)
        {
            if (env.Visit(this))
            {
                if (_camera == null) { _camera = env.mySelf.GetComponent<MonsterCamera>(); }
            }

            if (env.target != null) 
            {
                Debug.Log("���ɓG������");
                return Result.Success;
            }

            GameObject monster = _camera.CameraMonsterFind(env.viewingDistance);

            if (monster != null)
            {
                Debug.Log("�ړG");
                env.target = monster;
                return Result.Success;
            }

            Debug.Log("�G�Ȃ�");
            return Result.Failure;
        }
    }
}
