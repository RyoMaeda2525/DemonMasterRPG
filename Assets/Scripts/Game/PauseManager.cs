using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    /// <summary>���̃f���P�[�g�ɓo�^����ƃQ�[�����ꎞ��~�����ۂɌĂ΂��</summary>
    public event Action<bool> onCommandMenu;

�@�@/// <summary>�ꎞ��~��ON�EOFF������</summary>
    public void OnCommandMenu(bool trigger) 
    {
        onCommandMenu(trigger);      
    }
}
