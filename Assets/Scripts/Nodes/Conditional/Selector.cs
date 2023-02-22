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
            public bool Visit;
            [Header("�m�[�h�̗D�揇��")]
            public int Priority;
            [Header("�s������")]
            public string information;
            [SerializeField , SerializeReference ,SubclassSelector] public IBehavior Node;
        }

        [SerializeField] List<SelectorChildPriority> ChildNodes;
        IBehavior _current = null;

        public Result Action(Environment env) 
        {
            if (env.Visit(this)) 
            {
                ChildNodes.ForEach(n => n.Visit = false);
                _current = null;
            }

            do
            {
                if (_current != null)
                {
                    Result ret = _current.Action(env);
                    //�A�N�V�����m�[�h�����s���Ȃ牽�������ɖ߂�
                    if (ret == Result.Running) return Result.Running;
                    else if (ret == Result.Success) 
                    {
                        env.Leave(this);
                        return Result.Success;
                    }
                    //���s���I����Ă����珉�������ĕԂ�
                    _current = null;
                    return ret;
                }

                //Priority(�D�揇��)�̒l�̏��ɕ��ׂ�
                var nodes = ChildNodes.OrderBy(n => n.Priority);
                foreach (var node in nodes)
                {
                    if (node.Visit) continue;
                    _current = node.Node;
                    node.Visit = true;
                    break;
                }
            } while (_current != null);

            env.Leave(this);
            return Result.Success;
        }
    }
}
