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
            [SerializeField , Tooltip("ノードの優先順位")]
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
                    //アクションノードが実行中なら何もせずに戻る
                    if (ret == Result.Running) return Result.Running;
                    //実行が終わっていたら初期化して返す
                    _current = null;
                    return ret;
                }

                //Priority(優先順位)の値の順に並べる
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
