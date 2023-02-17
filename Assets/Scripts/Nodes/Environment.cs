using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MonsterTree
{
    public class Environment
    {
        //共有変数の定義
        public GameObject mySelf;
        public MonsterStatus target;
        public float viewingDistance;
        public MonsterStatus status;
        public AnimationController aniController;
        public TriggerCondition[] skillTrigger;

        List<IBehavior> visit = new List<IBehavior>();
        public bool Visit(IBehavior node)
        {
            if (visit.Where(n => n == node).Count() == 0)
            {
                visit.Add(node);
                return true;
            }
            return false;
        }
        public void Leave(IBehavior node)
        {
            var n = visit.Where(n => n == node);
            if (n.Count() == 0) return;

            visit.Remove(n.Single());
        }
    }
}
