using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMonsterCamera : MonoBehaviour
{
    [SerializeField]
    private PlayerMonsterMove _pmm = default;

    [SerializeField, Tooltip("映っているか判定するカメラへの参照")]
    Camera _targetCamera;

    ///<summary>画面内か判定するためのRect</summary>
    private Rect _rect = new Rect(0, 0, 1, 1);

    public void CameraEnemyFind(GameObject enemy)
    {
        var viewportPos = _targetCamera.WorldToViewportPoint(enemy.transform.position);

        if (_rect.Contains(viewportPos) && viewportPos.z > 0)
        {
            _pmm.ContactEnemy(enemy);
        }
    }
}
