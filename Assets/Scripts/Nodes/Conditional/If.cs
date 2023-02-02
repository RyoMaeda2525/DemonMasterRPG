using MonsterTree;
using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MonsterTree
{
    [Serializable]
    public class TooFar : IBehavior
    {
        [SerializeField] float length;
        [SerializeField, SerializeReference, SubclassSelector] IBehavior Next;

        public Result Action(Environment env)
        {
            //if(Func.Judge(env))
            if ((env.target.transform.position - env.mySelf.transform.position).sqrMagnitude > length)
            {
                return Next.Action(env);
            }
            else
            {
                return Result.Failure;
            }
        }
    }
}