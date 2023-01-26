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
        // ���̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă��邩���ׂ�
        // �A�^�b�`����Ă���ꍇ�͔j������B
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
                    Debug.LogWarning($"{t}���A�^�b�`���Ă���I�u�W�F�N�g������܂���");
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
