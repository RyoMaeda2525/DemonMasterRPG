using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterTree
{
    public class MoveTree : MonoBehaviour
    {
        [SerializeField , Header("���F����")]
        private float viewingDistance = 10f;

        [SerializeField]
        PlayerMonsterCamera _monsterCamera = null;

        [SerializeField, SerializeReference, SubclassSelector] IBehavior RootNode;

        [SerializeField , Header("���s��")]
        TacticsTree _tree;

        [SerializeField , Header("���݂̍��")]
        int _treeIndex = 0;

        Environment _env = new Environment();

        void Start()
        {
            _env.mySelf = this.gameObject;
            _env.viewingDistance = viewingDistance;
            _env.camera = _monsterCamera;
        }

        void Update()
        {
            RootNode = _tree._tactics[_treeIndex].RootNode;
            RootNode.Action(_env);
        }
    }
}
