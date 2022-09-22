using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyMonsterMove : MonoBehaviour
{
    [SerializeField, Tooltip("プレイヤーが視界に入っているか判定するカメラ")]
    EnemyCamera _eCmera = default;

    NavMeshAgent _nav;

    Animator _ani;

    public GameObject _target = null;

    // Start is called before the first frame update
    void Start()
    {
        _nav = GetComponent<NavMeshAgent>();
        _ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null && _nav.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            _nav.SetDestination(_target.transform.position);
        }
        float navSpeed = _nav.velocity.magnitude;
        _ani.SetFloat("NavSpeed", navSpeed);
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
}
