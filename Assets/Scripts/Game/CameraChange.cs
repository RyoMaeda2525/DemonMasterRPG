using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class CameraChange : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera _tpsCamera;
    [SerializeField]
    CinemachineVirtualCamera _targetCamera;
    [SerializeField]
    GameObject _lockOn;

    private MonsterStatus _target;

    public bool _isLockOn = false;

    public int _targetIndex = 0;

    private void Start()
    {
        _lockOn.SetActive(false);
    }

    void FixedUpdate()
    {
        if (_isLockOn && !_target.gameObject.activeSelf)
        {
            LookOff();
            CameraCange();
        }
        else if (_isLockOn) { _lockOn.transform.position = _target.gameObject.transform.position; }
    }

    public void LockOnOff()
    {
        if (!_isLockOn)
        {
            LookOn();
            Player.Instance._target = _target;
        }
        else
        {
            LookOff();
        }
        CameraCange();
    }

    void CameraCange()
    {
        //ロックオン中
        if (_isLockOn)
        {
            _tpsCamera.Priority = 10;
            _targetCamera.Priority = 20;
            _lockOn.transform.position = _target.gameObject.transform.position;
            _lockOn.SetActive(true);
        }
        //非ロックオン時
        else
        {
            _targetCamera.Priority = 10;
            _tpsCamera.Priority = 20;
            _lockOn.SetActive(false);
        }
    }

    void LookOn()
    {
        if (Player.Instance.EnemyList.Count > 0)
        {
            _isLockOn = true;
            _target = Player.Instance.EnemyList[0];

            Transform _lookPoint = _target.transform.Find("LookPoint");
            _targetCamera.LookAt = _lookPoint.transform;
        }
    }

    void LookOff()
    {
        _target = null;
        Player.Instance._target = null;
        _targetCamera.LookAt = null;
        _isLockOn = false;
        _targetIndex = 0;
    }

    public void LockOnChangeLeft()
    {
        List<MonsterStatus> enemyList = Player.Instance.EnemyList;

        if (enemyList.Count > 0)
        {
            if (_targetIndex == 0) { _targetIndex = enemyList.Count - 1; }
            else { _targetIndex -= 1; }

            _target = enemyList[_targetIndex];
            _lockOn.transform.position = _target.gameObject.transform.position;

            Player.Instance._target = _target;

            Transform _lookPoint = _target.transform.Find("LookPoint");
            _targetCamera.LookAt = _lookPoint.transform;
        }
    }
    public void LockOnChangeRight()
    {
        List<MonsterStatus> enemyList = Player.Instance.EnemyList;

        if (enemyList.Count > 0)
        {
            if (_targetIndex == enemyList.Count - 1) { _targetIndex = 0; }
            else { _targetIndex += 1; }

            _target = enemyList[_targetIndex];
            _lockOn.transform.position = _target.gameObject.transform.position;

            Player.Instance._target = _target;

            Transform _lookPoint = _target.transform.Find("LookPoint");
            _targetCamera.LookAt = _lookPoint.transform;
        }
    }
}
