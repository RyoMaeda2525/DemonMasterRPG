using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveAction : MonoBehaviour
{
    [SerializeField, Tooltip("�O�i���x")]
    private float forwardSpeed = 7.0f;

    [SerializeField, Tooltip("���񑬓x")]
    private float rotateSpeed = 2.0f;

    [SerializeField]
    private PlayerInput _input;

    Vector3 _stopVelo;

    private Rigidbody _rb;

    private Animator _anim;// �L�����ɃA�^�b�`�����A�j���[�^�[�ւ̎Q��

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
            _anim.SetFloat("Speed", Math.Abs(_rb.velocity.y) + Math.Abs(_rb.velocity.x));
        }
    }

    private void Awake() // ���̏����� Start ���ƒx���̂� Awake �ł���Ă���
    {
        _pauseManager = UiManager.Instance.PauseManager;
    }

    private void OnEnable() //�Q�[���ɓ���Ɖ����
    {
        _pauseManager.onCommandMenu += PauseCommand;
        //_pauseMenu.offCommandMenu += ResumCommand;
    }

    private void OnDisable() //������Ɣ�����
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

    void Pause() //��~����
    {
        _stopVelo = _rb.velocity;
        _anim.speed = 0;
    }

    void Resum() //�ĊJ
    {
        _rb.velocity = _stopVelo;
        _anim.speed = 1f;
    }
}
