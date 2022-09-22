using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCollisionDetector : MonoBehaviour
{
    [SerializeField]
    EnemyMonsterMove _emm = default;

    private void OnTriggerStay(Collider other)
    {
        _emm.OnDetectObject(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        _emm.ExitDetectObject();
    }
}