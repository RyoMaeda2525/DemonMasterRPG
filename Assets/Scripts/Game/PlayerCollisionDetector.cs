using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player.Instance.OnDetectObject(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<EnemyMonsterMove>())
        {
            if (!Player.Instance._emmList.Contains(other.GetComponent<EnemyMonsterMove>()))
            {
                Player.Instance.OnDetectObject(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player.Instance.ExitDetectObject(other.gameObject);
    }
}
