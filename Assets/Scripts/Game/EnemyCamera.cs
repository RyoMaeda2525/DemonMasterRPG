using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyCamera : MonoBehaviour
{
    [SerializeField , Tooltip("�f���Ă��邩���肷��J�����ւ̎Q��")]
    Camera _targetCamera; 

    //[SerializeField , Tooltip("�f���Ă��邩���肷��Ώۂւ̎Q�ƁBinspector�Ŏw�肷��")]
    //Transform _targetObj; 

    [SerializeField, Tooltip("���E�ɓ����Ă���ǂ�������܂ł̎���")]
    private float _findTime = 2;

    ///<summary>��ʓ������肷�邽�߂�Rect</summary>
    private Rect _rect = new Rect(0, 0, 1, 1);

    private float _timer = 0;

    public bool CameraPlayerFind(GameObject player)
    {
        var viewportPos = _targetCamera.WorldToViewportPoint(player.transform.position);

        if (_rect.Contains(viewportPos) && viewportPos.z > 0)
        {
            _timer += Time.deltaTime;
        }

        if (_timer > _findTime) { _timer = 0; return true; }

        return false;
    }

    public void TimerRiset() { _timer = 0; }
}
