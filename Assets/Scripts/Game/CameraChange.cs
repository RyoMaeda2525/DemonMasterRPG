using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraChange : SingletonMonoBehaviour<CameraChange>
{
    [SerializeField]
    CinemachineVirtualCamera _tpsCamera;
    [SerializeField]
    CinemachineVirtualCamera _targetCamera;

    private bool _isLockOn = false;

    public EnemyMonsterMove _target;

    public bool IsLockOn { get => _isLockOn; set => _isLockOn = value; }

    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            if (!_isLockOn)
            {
                LookOn();
            }
            else
            {
                LookOff();
            }
            CameraCange();
        }
    }

    void CameraCange()
    {
        //ロックオン中
        if (_isLockOn)
        {
            _tpsCamera.Priority = 10;
            _targetCamera.Priority = 20;
        }
        //非ロックオン時
        else
        {
            _targetCamera.Priority = 10;
            _tpsCamera.Priority = 20;
        }
    }

    void LookOn()
    {
        if (Player.Instance._emmList.Count > 0)
        {
            _isLockOn = true;
            _target = Player.Instance._emmList[0];

            Transform _lookPoint = _target.transform.Find("LookPoint");
            _targetCamera.LookAt = _lookPoint.transform;
        }
    }

    void LookOff()
    {
        _target = null;
        _targetCamera.LookAt = null;
        _isLockOn = false;
    }

}
