using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerMonsterMove : MonoBehaviour
{
    [SerializeField, Tooltip("�����X�^�[�̃X�e�[�^�X���i�[���Ă���X�N���v�g")]
    PlayerMonsterStatus _pms = default;

    [SerializeField, Tooltip("�s������܂ł̎���")]
    private int _actionTime = 5;

    /// <summary>���ݑ_���Ă���G</summary>
    public GameObject _target;

    /// <summary>���Ɏg���X�L��</summary>
    public SKILL _nextSkill;

    ///<summary>�s���܂ł̎��Ԃ�}��</summary>
    private float _actionTimer = 0;

    /// <summary>�퓬�����ǂ���</summary>
    private bool _actionBool = false;

    private Player _player = null;

    private NavMeshAgent _nav = default;

    private Animator _ani = default;

    private float navSpeed;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _nav = GetComponent<NavMeshAgent>();
        _ani = GetComponent<Animator>();
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
            _target = null;
            _actionTimer = 0;
            navSpeed = _nav.velocity.magnitude;
            _ani.SetFloat("NavSpeed", navSpeed);
            return;
        }

        if (_target != null && !_target.activeSelf)
        {
            _target = null;
            _nextSkill = new SKILL();
            _actionTimer = 0;
            return;
        }

        //���̍s�����擾
        if (_nextSkill.skill_name == null) 
        {
            TacticsOnAction();

            //��邱�Ƃ��Ȃ���ΐ퓬�I�� 
            if (_target == null)/*����:�h��̏ꍇ�������w�肷��΂���*/
            {
                _actionBool = false;
                _nextSkill = new SKILL();
                _actionTimer = 0;
                return;
            }
        }

        _actionTimer += Time.deltaTime;

        if (_actionTimer > _actionTime)
        {
            if (_nextSkill.skill_type[0].effect_type > 0)
            {
                _ani.Play("MagicSkill");
            }
            else if (_nextSkill.skill_type[0].effect_type == 0)
            {
                _ani.Play("Attack");
            }
            _actionTimer = 0;
        }
        _nav.SetDestination(_target.gameObject.transform.position);
        transform.LookAt(_target.gameObject.transform.position);
        navSpeed = _nav.velocity.magnitude;
        _ani.SetFloat("NavSpeed", navSpeed);
    }

    /// <summary>���ɍ��킹���s�����擾,���s</summary>
    public void TacticsOnAction()
    {
        _nextSkill = Tactics.instance.ActionSet(this, null, _pms._tactics, _pms._skillList);
        if (_nextSkill.skill_info != null)
        {
            _actionBool = true;
        }  
    }

    public void ContactEnemy(GameObject enemy)
    {
        _target = enemy;
        _actionBool = true;
    }

    /// <summary>"�s�����I��������Ƀ��Z�b�g���邽�߂̂���"</summary>
    public void OnActionEnd()
    {
        _actionTimer = 0;
        _nextSkill = new SKILL();
    }
}