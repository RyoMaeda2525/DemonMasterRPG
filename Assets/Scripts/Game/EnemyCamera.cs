using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyCamera : MonoBehaviour
{
    [SerializeField , Tooltip("映っているか判定するカメラへの参照")]
    Camera _targetCamera; 

    //[SerializeField , Tooltip("映っているか判定する対象への参照。inspectorで指定する")]
    //Transform _targetObj; 

    [SerializeField, Tooltip("視界に入ってから追いかけるまでの時間")]
    private float _findTime = 2;

    ///<summary>画面内か判定するためのRect</summary>
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
