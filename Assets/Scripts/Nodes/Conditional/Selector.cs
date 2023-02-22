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
            [Header("ノードの優先順位")]
            public int Priority;
            [Header("行動説明")]
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
                    //アクションノードが実行中なら何もせずに戻る
                    if (ret == Result.Running) return Result.Running;
                    else if (ret == Result.Success) 
                    {
                        env.Leave(this);
                        return Result.Success;
                    }
                    //実行が終わっていたら初期化して返す
                    _current = null;
                    return ret;
                }

                //Priority(優先順位)の値の順に並べる
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
