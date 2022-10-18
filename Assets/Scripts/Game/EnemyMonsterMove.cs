using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyMonsterMove : MonoBehaviour
{
    [SerializeField, Tooltip("プレイヤーが視界に入っているか判定するカメラ")]
    EnemyCamera _eCmera = default;

    [SerializeField, Tooltip("敵自身のステータス")]
    EnemyMonsterStatus _ems;

    [Tooltip("行動範囲の中心点"), SerializeField]
    public Vector3 startPosition = default;
    [Tooltip("行動範囲の半径"), SerializeField]
    public float Actionradius = 3;
    [Tooltip("行動の制限距離"), SerializeField]
    public float ActionDistance = 20f;
    [SerializeField, Tooltip("ランダムに移動するまでの時間")]
    private float _randomTime = 10;
    [SerializeField, Tooltip("行動するまでの時間")]
    private float _actionTime = 5;

    /// <summary>次に使うスキル</summary>
    public SKILL _nextSkill;

    /// <summary>狙っているプレイヤーキャラ</summary>
    public GameObject _target = null;

    private float _actionTimer = 0;

    /// <summary>ランダムに移動するかの時間を計るタイマー</summary>
    private float _randomTimer = 0;

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
    void FixedUpdate()
    {
        if (_target != null &&  _nav.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            if (!_target.activeSelf)  //もしターゲットが倒れていたら
            {
                _target = null;
                _nextSkill = new SKILL();
            }
            else 
            {
                _nav.SetDestination(_target.transform.position);
                transform.LookAt(_target.transform.position);
            } 

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

            //行動制限範囲外にでたら戻る
            if (Vector3.Distance(this.transform.position, startPosition) > ActionDistance)
            {
                _target = null;
                _randomTimer = 9999;
            }
        }
        else 
        {
            _actionTimer = 0;

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
            if (_eCmera.CameraPlayerFind(other)) 
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

    public void ScoutSuccess() 
    {
        GameObject monster = (GameObject)Resources.Load($"{_ems.NAME}");

        Instantiate(monster);

        monster.GetComponent<PlayerMonsterStatus>().LevelSet(_ems.LV);

        Player.Instance.PartyAdd(monster.GetComponent<PlayerMonsterStatus>());

        _ani.Play("Deth");
    }
}
