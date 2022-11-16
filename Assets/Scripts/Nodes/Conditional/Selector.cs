using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MonsterTree
{
    [Serializable]
    public class PrioritySelector : IBehavior
    {
        [Serializable]
        public class SelectorChildPriority 
        {
            [SerializeField , Tooltip("�m�[�h�̗D�揇��")]
            public int Priority;

            [SerializeField , SerializeReference ,SubclassSelector] public IBehavior Node;
        }

        [SerializeField] List<SelectorChildPriority> _childNodes;
        IBehavior _current = null;

        public Result Action(Environment env) 
        {
            if (env.Visit(this)) 
            {
                _current = null;
            }

            do
            {
                if (_current != null)
                {
                    Result ret = _current.Action(env);
                    //�A�N�V�����m�[�h�����s���Ȃ牽�������ɖ߂�
                    if (ret == Result.Running) return Result.Running;
                    //���s���I����Ă����珉�������ĕԂ�
                    _current = null;
                    return ret;
                }

                //Priority(�D�揇��)�̒l�̏��ɕ��ׂ�
                var nodes = _childNodes.OrderByDescending(n => n.Priority);
                foreach (var node in nodes)
                { 
                    _current = node.Node;
                    break;
                }
            } while (_current != null);

            env.Leave(this);
            return Result.Success;
        }
    }
}
