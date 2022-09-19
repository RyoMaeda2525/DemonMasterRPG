using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tactics : MonoBehaviour
{
    public void ActionSet(TacticsList tactics , List<SKILL> skillList) 
    {
        switch (tactics.tactics_id) 
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        // 他のゲームオブジェクトにアタッチされているか調べる
        // アタッチされている場合は破棄する。
        CheckInstance();
    }

    public static Tactics instance;

    public static Tactics Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(Tactics);

                instance = (Tactics)FindObjectOfType(t);
                if (instance == null)
                {
                    Debug.LogWarning($"{t}をアタッチしているオブジェクトがありません");
                }
            }

            return instance;
        }
    }

    protected bool CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
            return true;
        }
        else if (Instance == this)
        {
            return true;
        }
        Destroy(gameObject);
        return false;
    }
}
