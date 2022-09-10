using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Tooltip("前進速度")]
    private float forwardSpeed = 7.0f;

    [SerializeField, Tooltip("旋回速度")]
    private float rotateSpeed = 2.0f;

    private Vector3 walkSpeed = default;

    private Rigidbody _rb;

    private Animator _anim;                          // キャラにアタッチされるアニメーターへの参照

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }


    // 以下、メイン処理.リジッドボディと絡めるので、FixedUpdate内で処理を行う.
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");              // 入力デバイスの水平軸をhで定義
        float v = Input.GetAxis("Vertical");                // 入力デバイスの垂直軸をvで定義

        Vector3 dir = Vector3.forward * v + Vector3.right * h;

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
        walkSpeed = _rb.velocity;
        walkSpeed.y = 0;
        _anim.SetFloat("Speed", Math.Abs(h) + Math.Abs(v));
    }
}
