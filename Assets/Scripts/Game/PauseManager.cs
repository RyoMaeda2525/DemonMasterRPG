using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    /// <summary>このデリケートに登録するとゲームが一時停止した際に呼ばれる</summary>
    public event Action<bool> onCommandMenu;

　　/// <summary>一時停止のON・OFFをする</summary>
    public void OnCommandMenu(bool trigger) 
    {
        onCommandMenu(trigger);      
    }
}
