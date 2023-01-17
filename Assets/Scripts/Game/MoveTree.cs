using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterTree
{
    public class MoveTree : MonoBehaviour
    {
        [SerializeField , Header("Ž‹”F‹——£")]
        private float viewingDistance = 10f;

        [SerializeField, SerializeReference, SubclassSelector] IBehavior RootNode;

        Environment _env = new Environment();

        void Start()
        {
            _env.mySelf = this.gameObject;
            _env.viewingDistance = viewingDistance;
        }

        void Update()
        {
            RootNode.Action(_env);
        }
    }
}
