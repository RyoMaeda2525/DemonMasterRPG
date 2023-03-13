using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

namespace MonsterTree
{
    [RequireComponent(typeof(MonsterStatus))]
    [RequireComponent(typeof(AnimationController))]
    public class MoveTree : MonoBehaviour
    {
        [SerializeField, SerializeReference, SubclassSelector] IBehavior RootNode;

        [SerializeField, Header("���݂̍��")]
        int _treeIndex = 0;

        NavMeshAgent _nav;

        TacticsTree _tree;

        PauseManager _pauseManager;

        bool _pause = false;

        Vector3 _stopVelo; 

        public readonly Environment env = new Environment();

        void Start()
        {
            TreeSet();
            env.mySelf = this.gameObject;
            env.status = GetComponent<MonsterStatus>();
            env.aniController = GetComponent<AnimationController>();
            RootNode = _tree._tactics[_treeIndex].RootNode;
            env.skillTrigger = _tree._tactics[_treeIndex].skillTrigger.triggers;
            _nav = GetComponent<NavMeshAgent>();
            _nav.stoppingDistance = env.status.AttackDistance;
        }

        void Update()
        {
            if (!_pause)
            {
                float navSpeed = _nav.velocity.magnitude;
                env.aniController.SetFloat("NavSpeed", navSpeed);

                if (env.target != null && !env.target.gameObject.activeSelf &&
                   (env.target.transform.position - transform.position).magnitude > env.status.ViewingDistance)
                {
                    env.target = null;
                }

                if (env.aniController.Actionstate == ActionState.Wait)
                {
                    RootNode.Action(env);
                }
            }
        }

        public void ChangeTactics(int tacticsIndex) 
        {
            _treeIndex = tacticsIndex;
            RootNode = _tree._tactics[_treeIndex].RootNode;
            env.skillTrigger = _tree._tactics[_treeIndex].skillTrigger.triggers;
        }

        public void UnderAttack(MonsterStatus attaker) 
        {
            if (env.target == null && env.aniController.Actionstate == ActionState.Wait) 
            {
                env.target = attaker;
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

        private void Awake()�@// ���̏����� Start ���ƒx���̂� Awake �ł���Ă���
        {
            _pauseManager = UiManager.Instance.PauseManager;
        }

        private void OnEnable()�@//�Q�[���ɓ���Ɖ����
        {
            _pauseManager.onCommandMenu += PauseCommand;
            //_pauseMenu.offCommandMenu += ResumCommand;
        }

        private void OnDisable() //������Ɣ�����
        {
            _pauseManager.onCommandMenu -= PauseCommand;
            //_pauseMenu.offCommandMenu -= ResumCommand;
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

        void Pause() //��~����
        {
            _stopVelo = _nav.velocity;
            _nav.velocity = Vector3.zero;
            _nav.isStopped = true;
            env.aniController.AnimationStop();
        }

        void Resum() //�ĊJ
        {
            _nav.velocity = _stopVelo;
            _nav.isStopped = false;
            env.aniController.AnimationResume();
        }
    }
}
