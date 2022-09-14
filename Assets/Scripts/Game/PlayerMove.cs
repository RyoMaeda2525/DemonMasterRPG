using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Tooltip("�O�i���x")]
    private float forwardSpeed = 7.0f;

    [SerializeField, Tooltip("���񑬓x")]
    private float rotateSpeed = 2.0f;

    private Vector3 walkSpeed = default;

    private Rigidbody _rb;

    private Animator _anim;                          // �L�����ɃA�^�b�`�����A�j���[�^�[�ւ̎Q��

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
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
    }
}