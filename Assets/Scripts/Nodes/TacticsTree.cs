using MonsterTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TacticsTree", menuName = "TacticsTreeBase")]
public class TacticsTree : ScriptableObject
{
    [SerializeField] public List<TacticsClass> _tactics = default;
}

