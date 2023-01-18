using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField, Header("ステージ内のモンスターを入れる")]
    private GameObject[] _enemyMonster = null;

    /// <summary>ステージ内のモンスター</summary>
    public GameObject[] EnemyMonster => _enemyMonster;
}
