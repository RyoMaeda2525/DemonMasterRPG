using UnityEngine;
using System;
using MonsterTree;


    public enum TacticsType
    {
        escape,
        offense,
        diffence,
        special
    }

    [Serializable]
    public class TacticsClass
    {
        public int tactics_id;
        public string tactics_name;
        public string tactics_info;
        public TacticsType tactics_type;
        [SerializeField, SerializeReference, SubclassSelector] IBehavior rootNode;

        public IBehavior RootNode => rootNode;
    }
