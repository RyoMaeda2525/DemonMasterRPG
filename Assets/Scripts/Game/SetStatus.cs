using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Reflection;
using System;

public class SetStatus : MonoBehaviour
{
    [SerializeField]
    MonsterStatusSheet _ms;

    StatusSheet _ss;

    Type _msType;

    public string GetName(int charaId)
    {
        FieldInfo[] field = _ms.GetType().GetFields();
        List<StatusSheet> ss = (List<StatusSheet>)field[charaId].GetValue(_ms);
        return ss[1].NAME;
    }

    public int GetAttribute(int charaId)
    {
        FieldInfo[] field = _ms.GetType().GetFields();
        List<StatusSheet> ss = (List<StatusSheet>)field[charaId].GetValue(_ms);
        return ss[1].ATTRIBUTE;
    }

    public int[] GetStatus(int charaId , int level)
    {
        int[] setStatus = new int[7];

        FieldInfo[] field = _ms.GetType().GetFields();
        List<StatusSheet> ss = (List<StatusSheet>)field[charaId].GetValue(_ms);

        setStatus[0] = ss[level].CON;

        setStatus[1] = ss[level].MAG;

        setStatus[2] = ss[level].STR;

        setStatus[3] = ss[level].VIT;

        setStatus[4] = ss[level].INT;

        setStatus[5] = ss[level].EVA;

        setStatus[6] = ss[level].CRI;

        return setStatus;
    }

    void Awake()
    {
        // 他のゲームオブジェクトにアタッチされているか調べる
        // アタッチされている場合は破棄する。
        CheckInstance();
        _msType = typeof(MonsterStatusSheet);
    }

    public static SetStatus instance;

    public static SetStatus Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(SetStatus);

                instance = (SetStatus)FindObjectOfType(t);
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
