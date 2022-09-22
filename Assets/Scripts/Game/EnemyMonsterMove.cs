using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyMonsterMove : MonoBehaviour
{
    [SerializeField, Tooltip("プレイヤーが視界に入っているか判定するカメラ")]
    EnemyCamera _eCmera = default;

    [Tooltip("行動範囲の中心点"), SerializeField]
    public Vector3 startPosition = default;
    [Tooltip("行動範囲の半径"), SerializeField]
    public float Actionradius = 3;
    [Tooltip("行動の制限距離"), SerializeField]
    public float ActionDistance = 20f;
    [SerializeField, Tooltip("ランダムに移動するまでの時間")]
    private float _randomTime = 10;

    /// <summary>ランダムに移動するかの時間を計るタイマー</summary>
    private float _randomTimer = 0;

    /// <summary>狙っているプレイヤーキャラ</summary>
    private GameObject _target = null;

    private NavMeshAgent _nav;

    private Animator _ani;

    // Start is called before the first frame update
    void Start()
    {
        _nav = GetComponent<NavMeshAgent>();
        _ani = GetComponent<Animator>();
        startPosition = this.transform.position; //配置された場所を記憶
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null && _nav.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            _nav.SetDestination(_target.transform.position);
            
            //行動制限範囲外にでたら戻る
            if (Vector3.Distance(this.transform.position, startPosition) > ActionDistance)
            {
                _target = null;
                _randomTimer = 9999;
            }
        }
        else 
        {
            //歩き回る場合一定間隔でランダムに移動する
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
            else //Bossキャラなど歩き回らないのはActionradiusを0にすることで同じ場所にいる
            {
                _nav.SetDestination(startPosition);
            }
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
