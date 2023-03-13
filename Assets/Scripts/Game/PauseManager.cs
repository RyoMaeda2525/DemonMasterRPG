using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public event Action<bool> onCommandMenu;

    public void OnCommandMenu(bool trigger) 
    {
        onCommandMenu(trigger);      
    }
}
