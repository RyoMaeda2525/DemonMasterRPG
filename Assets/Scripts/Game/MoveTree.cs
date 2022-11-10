using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterTree
{
    public class MoveTree : MonoBehaviour
    {
        [SerializeField, SerializeReference, SubclassSelector] IBehavior RootNode;

        Environment _env = new Environment();

        void Start()
        {
            _env.mySelf = this.gameObject;
        }

        void Update()
        {
            RootNode.Action(_env);
        }
    }
}
