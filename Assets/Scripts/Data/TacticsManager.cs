using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Tactics
{
    public int Tactics_id;
    public string Tactics_name;
    public string Tactics_info;
    public string Tactics_type;
}

public class TacticsManager : ScriptableObject
{
    public List<Tactics> _tactics = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
