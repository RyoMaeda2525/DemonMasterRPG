using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField, Header("�X�e�[�W���̃����X�^�[������")]
    private GameObject[] _enemyMonster = null;

    /// <summary>�X�e�[�W���̃����X�^�[</summary>
    public GameObject[] EnemyMonster => _enemyMonster;
}
