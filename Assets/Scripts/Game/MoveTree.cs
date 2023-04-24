using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

namespace MonsterTree
{
    [RequireComponent(typeof(AnimationController))]
    public class MoveTree : MonoBehaviour
    {
        [SerializeField, SerializeReference, SubclassSelector] IBehavior RootNode;

        [SerializeField, Header("現在の作戦")]
        int _treeIndex = 0;

        NavMeshAgent _nav;

        TacticsTree _tree;

        PauseManager _pauseManager;

        bool _pause = false;

        Vector3 _stopVelo; 

        readonly Environment env = new Environment();

        /// <summary>各ノード間で受け渡しが可能な変数を保持するためのデータアセット</summary>
        public Environment Env => env;

        void Start()
        {
            TreeSet();
            Env.mySelf = this.gameObject;
            Env.status = GetComponent<MonsterStatus>();
            Env.aniController = GetComponent<AnimationController>();
            RootNode = _tree._tactics[_treeIndex].RootNode;
            Env.skillTrigger = _tree._tactics[_treeIndex].skillTrigger.triggers;
            _nav = GetComponent<NavMeshAgent>();
            _nav.stoppingDistance = Env.status.AttackDistance;
        }

        void Update()
        {
            if (!_pause)
            {
                float navSpeed = _nav.velocity.magnitude;
                Env.aniController.SetFloat("NavSpeed", navSpeed);

                if (Env.target != null && !Env.target.gameObject.activeSelf || 
                    Env.target != null &&
                   (Env.target.transform.position - transform.position).magnitude > Env.status.ViewingDistance)
                {
                    env.status.ActionEnd();
                    Env.target = null;
                }

                if (Env.aniController.Actionstate == ActionState.Wait)
                {
                    RootNode.Action(Env);
                }
            }
        }

        private void TreeSet() 
        {
            foreach (var tree in GameManager.Instance.TacticsTrees) 
            {
                if (tree.name == this.tag) 
                {
                    _tree = Instantiate(tree);
                }
            }
        }

        private void Awake()
        {
            _pauseManager = GameManager.Instance.PauseManager;
        }

        private void OnEnable()
        {
            _pauseManager.onCommandMenu += PauseCommand;
        }

        private void OnDisable()
        {
            _pauseManager.onCommandMenu -= PauseCommand;
        }

        void PauseCommand(bool onPause)
        {
            if (onPause)
            {
                Pause();
                _pause = true;
            }
            else
            {
                Resum();
                _pause = false;
            }
        }

        void Pause() //停止処理
        {
            _stopVelo = _nav.velocity;
            _nav.velocity = Vector3.zero;
            _nav.isStopped = true;
            Env.aniController.AnimationStop();
        }

        void Resum() //再開
        {
            _nav.velocity = _stopVelo;
            _nav.isStopped = false;
            Env.aniController.AnimationResume();
        }

        /// <summary>作戦の変更</summary>
        /// <param name="tacticsIndex">どの作戦か判別するためのindex</param>
        public void ChangeTactics(int tacticsIndex)
        {
            _treeIndex = tacticsIndex;
            RootNode = _tree._tactics[_treeIndex].RootNode;
            Env.skillTrigger = _tree._tactics[_treeIndex].skillTrigger.triggers;
        }

        /// <summary>攻撃されたときターゲットがいなければ反撃する</summary>
        /// <param name="attaker"></param>
        public void UnderAttack(MonsterStatus attaker)
        {
            if (Env.target == null && Env.aniController.Actionstate == ActionState.Wait)
            {
                Env.target = attaker;
            }
        }
    }
}
