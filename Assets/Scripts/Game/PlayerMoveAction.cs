using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveAction : MonoBehaviour
{
    [SerializeField, Tooltip("前進速度")]
    private float forwardSpeed = 7.0f;

    [SerializeField, Tooltip("旋回速度")]
    private float rotateSpeed = 2.0f;

    [SerializeField]
    private PlayerInput _input;

    Vector3 _stopVelo;

    private Rigidbody _rb;

    private Animator _anim;// キャラにアタッチされるアニメーターへの参照

    private PauseManager _pauseManager;

    bool _pause;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        if (!_pause)
        {
            var inputMove = _input.actions["Move"].ReadValue<Vector2>();

            Vector3 dir = Vector3.forward * inputMove.y + Vector3.right * inputMove.x;

            if (dir == Vector3.zero)
            {
                _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f);// 方向の入力がニュートラルの時は、y 軸方向の速度を保持する
            }
            else
            {
                //anim.SetBool("run", true);
                // カメラを基準に入力が上下=奥/手前, 左右=左右にキャラクターを向ける
                dir = Camera.main.transform.TransformDirection(dir);    // メインカメラを基準に入力方向のベクトルを変換する
                dir.y = 0;  // y 軸方向はゼロにして水平方向のベクトルにする
                            // 入力方向に滑らかに回転させる
                Quaternion targetRotation = Quaternion.LookRotation(dir);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);

                Vector3 velo = dir.normalized * forwardSpeed; // 入力した方向に移動する
                _rb.velocity = velo;   // 計算した速度ベクトルをセットする
            }
            _anim.SetFloat("Speed", Math.Abs(_rb.velocity.y) + Math.Abs(_rb.velocity.x));
        }
    }

    private void Awake() // この処理は Start やると遅いので Awake でやっている
    {
        _pauseManager = UiManager.Instance.PauseManager;
    }

    private void OnEnable() //ゲームに入ると加わる
    {
        _pauseManager.onCommandMenu += PauseCommand;
        //_pauseMenu.offCommandMenu += ResumCommand;
    }

    private void OnDisable() //消えると抜ける
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

    void Pause() //停止処理
    {
        _stopVelo = _rb.velocity;
        _anim.speed = 0;
    }

    void Resum() //再開
    {
        _rb.velocity = _stopVelo;
        _anim.speed = 1f;
    }
}
