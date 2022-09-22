using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerMonsterMove : MonoBehaviour
{
    [SerializeField , Tooltip("�����X�^�[�̃X�e�[�^�X���i�[���Ă���X�N���v�g")]
    PlayerMonsterStatus _pms = default;

    [SerializeField, Tooltip("�s������܂ł̎���")]
    private int _actionTime = 5;

    ///<summary>�s���܂ł̎��Ԃ�}��</summary>
    private float _actionTimer = 0;

    /// <summary>�퓬�����ǂ���</summary>
    private bool _actionBool = false;

    private Player _player = null;

    private NavMeshAgent _nav = default;

    private Animator _ani = default;

    private GameObject _target;

    private float navSpeed;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _nav = GetComponent<NavMeshAgent>();
        _ani = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _player._pms.Add(GetComponent<PlayerMonsterStatus>());
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //�퓬���ł͂Ȃ���΃v���C���[��ǂ�������
        if (!_actionBool) 
        {
            if (_nav.pathStatus != NavMeshPathStatus.PathInvalid)
            {
                _nav.SetDestination(_player.gameObject.transform.position);
            }
            _actionTimer = 0;
            navSpeed = _nav.velocity.magnitude;
            _ani.SetFloat("NavSpeed", navSpeed);
            return; 
        }

        _actionTimer += Time.deltaTime;

        if (_actionTimer > _actionTime)
        {
            Tactics.instance.ActionSet(_pms._tactics, _pms._skillList);
        }
        if (_nav.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            _nav.SetDestination(_target.gameObject.transform.position);
        }
        navSpeed = _nav.velocity.magnitude;
        _ani.SetFloat("NavSpeed", navSpeed);
    }

    public void ContactEnemy(GameObject enemy) 
    {
        _target = enemy;
        _actionBool = true;
    }
}