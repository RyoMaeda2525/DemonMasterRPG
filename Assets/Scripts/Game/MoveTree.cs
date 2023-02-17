using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterTree
{
    [RequireComponent(typeof(MonsterStatus))]
    [RequireComponent(typeof(AnimationController))]
    public class MoveTree : MonoBehaviour
    {
        [SerializeField, Header("Ž‹”F‹——£")]
        private float viewingDistance = 10f;

        [SerializeField, SerializeReference, SubclassSelector] IBehavior RootNode;

        [SerializeField, Header("ìís“®")]
        TacticsTree _tree;

        [SerializeField, Header("Œ»Ý‚Ììí")]
        int _treeIndex = 0;

        Environment _env = new Environment();

        public Environment Environment => _env;

        void Start()
        {
            _env.mySelf = this.gameObject;
            _env.status = GetComponent<MonsterStatus>();
            _env.viewingDistance = viewingDistance;
            _env.aniController = GetComponent<AnimationController>();
            _tree = Instantiate((TacticsTree)Resources.Load($"Node/{_tree.name}"));
            RootNode = _tree._tactics[_treeIndex].RootNode;
            _env.skillTrigger = _tree._tactics[_treeIndex].skillTrigger.triggers;
        }

        void Update()
        {
            if (_env.target != null && !_env.target.gameObject.activeSelf) 
            {
                _env.target = null; 
            }

            if (_env.aniController.Actionstate == ActionState.Wait)
            {
                RootNode.Action(_env);
            }
        }

        public void ChangeTactics(int tacticsIndex) 
        {
            _treeIndex = tacticsIndex;
            RootNode = _tree._tactics[_treeIndex].RootNode;
            _env.skillTrigger = _tree._tactics[_treeIndex].skillTrigger.triggers;
        }
    }
}
