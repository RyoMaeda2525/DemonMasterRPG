using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {


        if (other.CompareTag("EnemyMonster"))
        {
            //��ʓ������肷�邽�߂�Rect
            Rect _rect = new Rect(0, 0, 1, 1);

            var viewportPos = Camera.main.WorldToViewportPoint(other.transform.position);

            if (_rect.Contains(viewportPos) && viewportPos.z > 0)
            {
                Player.Instance.EnemyDiscover(other.GetComponent<MonsterStatus>());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player.Instance.ExitDetectObject(other.gameObject);
    }
}
