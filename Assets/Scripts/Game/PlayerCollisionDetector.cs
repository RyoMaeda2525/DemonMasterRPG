using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<EnemyMonsterMove>())
        {
            //‰æ–Ê“à‚©”»’è‚·‚é‚½‚ß‚ÌRect
            Rect _rect = new Rect(0, 0, 1, 1);

            var viewportPos = Camera.main.WorldToViewportPoint(other.transform.position);

            if (_rect.Contains(viewportPos) && viewportPos.z > 0)
            {
                if (!Player.Instance._emmList.Contains(other.GetComponent<EnemyMonsterMove>()))
                {
                    Player.Instance._emmList.Add(other.GetComponent<EnemyMonsterMove>());
                    Player.Instance.OnDetectObject(other.gameObject);
                }
                else 
                {
                    Player.Instance._emmList.Remove(other.GetComponent<EnemyMonsterMove>())
;                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player.Instance.ExitDetectObject(other.gameObject);
    }
}
