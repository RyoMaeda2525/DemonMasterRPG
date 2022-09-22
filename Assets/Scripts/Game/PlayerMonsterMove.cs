using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerMonsterMove : MonoBehaviour
{
    [SerializeField , Tooltip("モンスターのステータスを格納しているスクリプト")]
    PlayerMonsterStatus _pms = default;

    [SerializeField, Tooltip("行動するまでの時間")]
    private int _actionTime = 5;

    ///<summary>行動までの時間を図る</summary>
    private float _actionTimer = 0;

    /// <summary>戦闘中かどうか</summary>
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
        //戦闘中ではなければプレイヤーを追っかける
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