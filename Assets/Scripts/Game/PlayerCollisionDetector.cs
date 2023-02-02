using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        MonsterStatus monster = other.GetComponent<MonsterStatus>();

        if (monster != null && monster.CompareTag("EnemyMonster"))
        {
            //‰æ–Ê“à‚©”»’è‚·‚é‚½‚ß‚ÌRect
            Rect _rect = new Rect(0, 0, 1, 1);

            var viewportPos = Camera.main.WorldToViewportPoint(other.transform.position);

            if (_rect.Contains(viewportPos) && viewportPos.z > 0)
            {
                if (!Player.Instance._enemyList.Contains(monster))
                {
                    Player.Instance._enemyList.Add(monster);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player.Instance.ExitDetectObject(other.gameObject);
    }
}
