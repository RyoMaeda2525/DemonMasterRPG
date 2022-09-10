using System;
using UnityEngine;
using System.Collections;

namespace UnityChan
{
    // �K�v�ȃR���|�[�l���g�̗�L
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]

    public class UnityChanControlScriptWithRgidBody : MonoBehaviour
    {

        public float animSpeed = 1.5f;              // �A�j���[�V�����Đ����x�ݒ�
        public float lookSmoother = 3.0f;           // a smoothing setting for camera motion
        public bool useCurves = true;               // Mecanim�ŃJ�[�u�������g�����ݒ肷��
                                                    // ���̃X�C�b�`�������Ă��Ȃ��ƃJ�[�u�͎g���Ȃ�
        public float useCurvesHeight = 0.5f;        // �J�[�u�␳�̗L�������i�n�ʂ����蔲���₷�����ɂ͑傫������j

        // �ȉ��L�����N�^�[�R���g���[���p�p�����^
        // �O�i���x
        public float forwardSpeed = 7.0f;
        // ��ޑ��x
        public float backwardSpeed = 2.0f;
        // ���񑬓x
        public float rotateSpeed = 2.0f;
        // �W�����v�З�
        public float jumpPower = 3.0f;
        // �L�����N�^�[�R���g���[���i�J�v�Z���R���C�_�j�̎Q��
        private CapsuleCollider col;
        private Rigidbody _rb;
        // �L�����N�^�[�R���g���[���i�J�v�Z���R���C�_�j�̈ړ���
        private Vector3 velocity;

        private Animator _anim;                          // �L�����ɃA�^�b�`�����A�j���[�^�[�ւ̎Q��
        private AnimatorStateInfo currentBaseState;         // base layer�Ŏg����A�A�j���[�^�[�̌��݂̏�Ԃ̎Q��

        private Vector3 walkSpeed = default;
        GameObject cameraObject;

        static int locoState = Animator.StringToHash("Base Layer.Locomotion");

        // ������
        void Start()
        {
            // Animator�R���|�[�l���g���擾����
            _anim = GetComponent<Animator>();
            // CapsuleCollider�R���|�[�l���g���擾����i�J�v�Z���^�R���W�����j
            col = GetComponent<CapsuleCollider>();
            _rb = GetComponent<Rigidbody>();
            //���C���J�������擾����
            cameraObject = GameObject.FindWithTag("MainCamera");
            // CapsuleCollider�R���|�[�l���g��Height�ACenter�̏����l��ۑ�����
        }


        // �ȉ��A���C������.���W�b�h�{�f�B�Ɨ��߂�̂ŁAFixedUpdate���ŏ������s��.
        void FixedUpdate()
        {
            float h = Input.GetAxis("Horizontal");              // ���̓f�o�C�X�̐�������h�Œ�`
            float v = Input.GetAxis("Vertical");                // ���̓f�o�C�X�̐�������v�Œ�`

                Vector3 dir = Vector3.forward * v + Vector3.right * h;

                if (dir == Vector3.zero)
                {
                    _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f);// �����̓��͂��j���[�g�����̎��́Ay �������̑��x��ێ�����
                }
                else
                {
                    //anim.SetBool("run", true);
                    // �J��������ɓ��͂��㉺=��/��O, ���E=���E�ɃL�����N�^�[��������
                    dir = Camera.main.transform.TransformDirection(dir);    // ���C���J��������ɓ��͕����̃x�N�g����ϊ�����
                    dir.y = 0;  // y �������̓[���ɂ��Đ��������̃x�N�g���ɂ���
                                // ���͕����Ɋ��炩�ɉ�]������
                    Quaternion targetRotation = Quaternion.LookRotation(dir);
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);

                    Vector3 velo = dir.normalized * forwardSpeed; // ���͂��������Ɉړ�����
                    _rb.velocity = velo;   // �v�Z�������x�x�N�g�����Z�b�g����
                }
                walkSpeed = _rb.velocity;
                walkSpeed.y = 0;
                _anim.SetFloat("Speed", Math.Abs(h) + Math.Abs(v));
  
            if (Input.GetButtonDown("Jump"))
            {   // �X�y�[�X�L�[����͂�����

                //�A�j���[�V�����̃X�e�[�g��Locomotion�̍Œ��̂݃W�����v�ł���
                if (currentBaseState.fullPathHash == locoState)
                {
                    //�X�e�[�g�J�ڒ��łȂ�������W�����v�ł���
                    if (!_anim.IsInTransition(0))
                    {
                        _rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
                        _anim.SetBool("Jump", true);     // Animator�ɃW�����v�ɐ؂�ւ���t���O�𑗂�
                    }
                }
            }
        }
    }
}