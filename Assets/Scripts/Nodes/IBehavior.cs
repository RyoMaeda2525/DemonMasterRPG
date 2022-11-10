using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MonsterTree
{
    public enum Result
    {
        Inactive,
        Running,
        Success,
        Failure,
        Completed
    }

    public interface IBehavior
    {
        Result Action(Environment env);
    }
}
