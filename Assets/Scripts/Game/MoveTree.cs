using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterTree
{
    [RequireComponent(typeof(MonsterStatus))]
    public class MoveTree : MonoBehaviour
    {
        [SerializeField , Header("‹”F‹——£")]
        private float viewingDistance = 10f;

        [SerializeField, Header("UŒ‚Ë’ö")]
        private float attackDistance = 5f;

        [SerializeField, SerializeReference, SubclassSelector] IBehavior RootNode;

        [SerializeField , Header("ìís“®")]
        TacticsTree _tree;

        [SerializeField , Header("Œ»İ‚Ììí")]
        int _treeIndex = 0;

        Environment _env = new Environment();

        void Start()
        {
            _env.mySelf = this.gameObject;
            _env.viewingDistance = viewingDistance;
            _env.status = GetComponent<MonsterStatus>();
        }

        void Update()
        {
            RootNode = _tree._tactics[_treeIndex].RootNode;
            RootNode.Action(_env);
        }
    }
}
