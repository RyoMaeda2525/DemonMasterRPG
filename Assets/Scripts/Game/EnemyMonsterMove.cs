using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyMonsterMove : MonoBehaviour
{
    [SerializeField, Tooltip("�v���C���[�����E�ɓ����Ă��邩���肷��J����")]
    EnemyCamera _eCmera = default;

    [SerializeField, Tooltip("�G�̃X�e�[�^�X")]
    EnemyMonsterStatus _ems;

    [Tooltip("�s���͈͂̒��S�_"), SerializeField]
    public Vector3 startPosition = default;
    [Tooltip("�s���͈͂̔��a"), SerializeField]
    public float Actionradius = 3;
    [Tooltip("�s���̐�������"), SerializeField]
    public float ActionDistance = 20f;
    [SerializeField, Tooltip("�����_���Ɉړ�����܂ł̎���")]
    private float _randomTime = 10;
    [SerializeField, Tooltip("�s������܂ł̎���")]
    private float _actionTime = 5;

    /// <summary>���Ɏg���X�L��</summary>
    public SKILL _nextSkill;

    /// <summary>�_���Ă���v���C���[�L����</summary>
    public GameObject _target = null;

    private float _actionTimer = 0;

    /// <summary>�����_���Ɉړ����邩�̎��Ԃ��v��^�C�}�[</summary>
    private float _randomTimer = 0;

    private NavMeshAgent _nav;

    private Animator _ani;

    // Start is called before the first frame update
    void Start()
    {
        _nav = GetComponent<NavMeshAgent>();
        _ani = GetComponent<Animator>();
        startPosition = this.transform.position; //�z�u���ꂽ�ꏊ���L��
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_target != null && _nav.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            _nav.SetDestination(_target.transform.position);
            transform.LookAt(_target.transform.position);

            if (_nextSkill.skill_name == null) { TacticsOnAction(); }

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

            //�s�������͈͊O�ɂł���߂�
            if (Vector3.Distance(this.transform.position, startPosition) > ActionDistance)
            {
                _target = null;
                _randomTimer = 9999;
            }
        }
        else 
        {
            _actionTimer = 0;

            //�������ꍇ���Ԋu�Ń����_���Ɉړ�����
            if (Actionradius > 0)
            {
                _randomTimer += Time.deltaTime;

                if (_randomTimer > _randomTime)
                {
                    NavMeshHit navMeshHit;

                    Vector3 randomPos = new Vector3(Random.Range(startPosition.x - Actionradius, startPosition.x + Actionradius), 0,
                                                                    Random.Range(startPosition.z - Actionradius, startPosition.z + Actionradius));
                    NavMesh.SamplePosition(randomPos, out navMeshHit, 10, 1);
                    _nav.SetDestination(navMeshHit.position);

                    _randomTimer = 0;
                }
            }
            else //Boss�L�����ȂǕ������Ȃ��̂�Actionradius��0�ɂ��邱�Ƃœ����ꏊ�ɂ���
            {
                _nav.SetDestination(startPosition);
            }
        }

        float navSpeed = _nav.velocity.magnitude;
        _ani.SetFloat("NavSpeed", navSpeed);
    }

    public void TacticsOnAction()
    {
        _nextSkill = Tactics.instance.ActionSet(null, this, TacticsManager.instance._tactics[1], _ems._skillList); ;
        if (_nextSkill.skill_type[0].effect_type > 0)
        {
            //_ani.Play("MagicChant");
        }
    }

    public void OnDetectObject(GameObject other)
    {
        if (_target == null && other.GetComponent<Player>())
        {
            if (_eCmera.CameraPlayerFind()) 
            {
                PlayerMonsterStatus[] _monsters = other.GetComponent<Player>()._pms.ToArray();
                _target = _monsters[Random.Range(0, _monsters.Length)].gameObject;
            }          
        }
    }

    public void ExitDetectObject() 
    {
        _eCmera.TimerRiset();
    }
    public void OnActionEnd()
    {
        _actionTimer = 0;
        _nextSkill = new SKILL();
    }
}
