using System;
using UnityEngine;
using System.Collections;

namespace UnityChan
{
    // 必要なコンポーネントの列記
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]

    public class UnityChanControlScriptWithRgidBody : MonoBehaviour
    {

        public float animSpeed = 1.5f;              // アニメーション再生速度設定
        public float lookSmoother = 3.0f;           // a smoothing setting for camera motion
        public bool useCurves = true;               // Mecanimでカーブ調整を使うか設定する
                                                    // このスイッチが入っていないとカーブは使われない
        public float useCurvesHeight = 0.5f;        // カーブ補正の有効高さ（地面をすり抜けやすい時には大きくする）

        // 以下キャラクターコントローラ用パラメタ
        // 前進速度
        public float forwardSpeed = 7.0f;
        // 後退速度
        public float backwardSpeed = 2.0f;
        // 旋回速度
        public float rotateSpeed = 2.0f;
        // ジャンプ威力
        public float jumpPower = 3.0f;
        // キャラクターコントローラ（カプセルコライダ）の参照
        private CapsuleCollider col;
        private Rigidbody _rb;
        // キャラクターコントローラ（カプセルコライダ）の移動量
        private Vector3 velocity;

        private Animator _anim;                          // キャラにアタッチされるアニメーターへの参照
        private AnimatorStateInfo currentBaseState;         // base layerで使われる、アニメーターの現在の状態の参照

        private Vector3 walkSpeed = default;
        GameObject cameraObject;

        static int locoState = Animator.StringToHash("Base Layer.Locomotion");

        // 初期化
        void Start()
        {
            // Animatorコンポーネントを取得する
            _anim = GetComponent<Animator>();
            // CapsuleColliderコンポーネントを取得する（カプセル型コリジョン）
            col = GetComponent<CapsuleCollider>();
            _rb = GetComponent<Rigidbody>();
            //メインカメラを取得する
            cameraObject = GameObject.FindWithTag("MainCamera");
            // CapsuleColliderコンポーネントのHeight、Centerの初期値を保存する
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
  
            if (Input.GetButtonDown("Jump"))
            {   // スペースキーを入力したら

                //アニメーションのステートがLocomotionの最中のみジャンプできる
                if (currentBaseState.fullPathHash == locoState)
                {
                    //ステート遷移中でなかったらジャンプできる
                    if (!_anim.IsInTransition(0))
                    {
                        _rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
                        _anim.SetBool("Jump", true);     // Animatorにジャンプに切り替えるフラグを送る
                    }
                }
            }
        }
    }
}