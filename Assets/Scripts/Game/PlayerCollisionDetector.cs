using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        EnemyMonsterMove emm = other.GetComponent<EnemyMonsterMove>();

        if (emm != null)
        {
            //��ʓ������肷�邽�߂�Rect
            Rect _rect = new Rect(0, 0, 1, 1);

            var viewportPos = Camera.main.WorldToViewportPoint(other.transform.position);

            if (_rect.Contains(viewportPos) && viewportPos.z > 0)
            {
                if (!Player.Instance._emmList.Contains(emm))
                {
                    Player.Instance._emmList.Add(emm);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player.Instance.ExitDetectObject(other.gameObject);
    }
}
