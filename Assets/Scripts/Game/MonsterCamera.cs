using MonsterTree;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterCamera : MonoBehaviour
{
    [SerializeField, Tooltip("�f���Ă��邩���肷��J�����ւ̎Q��")]
    Camera _targetCamera;

    ///<summary>��ʓ������肷�邽�߂�Rect</summary>
    private Rect _rect = new Rect(0, 0, 1, 1);

    public GameObject CameraMonsterFind(float viewingDistance)
    {
        if(this.CompareTag("EnemyMonster")) 
        {
            foreach (var monster in Player.Instance.MonsterStatus)
            {
                if ((monster.transform.position - transform.position).magnitude < viewingDistance)
                {
                    if (CameraCheck(monster.gameObject)) 
                    {
                        _targetCamera.enabled = false;
                        return monster.gameObject; 
                    }
                }
            }
        }
        else
        {
            foreach (var monster in Player.Instance._enemyList)
            {
                if ((monster.transform.position - transform.position).magnitude < viewingDistance)
                {
                    if (CameraCheck(monster.gameObject)) 
                    {
                        _targetCamera.enabled = false;
                        return monster.gameObject; 
                    }
                }
            }
        }

        return null;
    }

    private bool CameraCheck(GameObject monster) 
    {
        if (!_targetCamera.enabled) { _targetCamera.enabled = true; }

        var viewportPos = _targetCamera.WorldToViewportPoint(monster.transform.position);

        if (_rect.Contains(viewportPos) && viewportPos.z > 0)
        {
            return true;
        }
        return false;
    }
}
