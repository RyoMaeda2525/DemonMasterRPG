using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterSheet : ScriptableObject
{
    public enum CharacterType 
    {
        Player = 0,
        Enemy = 1,
    }

    [SerializeField] int _id;
    [SerializeField] CharacterType _type;
    [SerializeField] string _name;
    [SerializeField] StatusSheet _baseParam;
    [SerializeField] GameObject _prefab;

    public int Id => _id;
    public CharacterType Type => _type;
    public string Name => _name;
    public StatusSheet Sheet => _baseParam;
    public GameObject Prefab => _prefab;
}
