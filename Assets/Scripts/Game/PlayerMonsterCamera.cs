using MonsterTree;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMonsterCamera : MonoBehaviour
{
    [SerializeField, Tooltip("‰f‚Á‚Ä‚¢‚é‚©”»’è‚·‚éƒJƒƒ‰‚Ö‚ÌQÆ")]
    Camera _targetCamera;

    private List<PlayerMonsterStatus> _playerMonsterList = null;

    private List<GameObject> _monsterList = null;

    ///<summary>‰æ–Ê“à‚©”»’è‚·‚é‚½‚ß‚ÌRect</summary>
    private Rect _rect = new Rect(0, 0, 1, 1);

    public GameObject CameraMonsterFind(float viewingDistance)
    {
        if(gameObject.tag == "EnemyMonster") 
        {
            foreach (var monster in Player.Instance._pms)
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
            foreach (var monster in Player.Instance._emmList)
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
