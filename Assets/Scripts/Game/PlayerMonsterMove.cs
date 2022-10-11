using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerMonsterMove : MonoBehaviour
{
    [SerializeField, Tooltip("モンスターのステータスを格納しているスクリプト")]
    PlayerMonsterStatus _pms = default;

    [SerializeField, Tooltip("行動するまでの時間")]
    private int _actionTime = 5;

    /// <summary>現在狙っている敵</summary>
    public GameObject _target;

    /// <summary>次に使うスキル</summary>
    public SKILL _nextSkill;

    ///<summary>行動までの時間を図る</summary>
    private float _actionTimer = 0;

    /// <summary>戦闘中かどうか</summary>
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
        //戦闘中ではなければプレイヤーを追っかける
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

        //次の行動を取得
        if (_nextSkill.skill_name == null) 
        {
            TacticsOnAction();

            //やることがなければ戦闘終了 
            if (_target == null)/*メモ:防御の場合自分を指定すればいい*/
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

    /// <summary>作戦に合わせた行動を取得,実行</summary>
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

    /// <summary>"行動が終わった時にリセットするためのもの"</summary>
    public void OnActionEnd()
    {
        _actionTimer = 0;
        _nextSkill = new SKILL();
    }
}