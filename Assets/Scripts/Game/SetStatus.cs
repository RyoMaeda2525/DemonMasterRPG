using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class SetStatus : SingletonMonoBehaviour<SetStatus>
{
    [SerializeField , Tooltip("Monsterのステータスを格納しているXLSファイル")]
    StatusSheet SS;

    string GetName(int charaId)
    {
        return SS.sheets[charaId].list[0].NAME;
    }

    int GetAttribute(int charaId)
    {
        return SS.sheets[charaId].list[1].ATTRIBUTE;
    }

    List<int> GetStatus(int charaId , int level)
    {
        List<int> setStatus = new List<int>();

        setStatus[0] = SS.sheets[charaId].list[level].CON;

        setStatus[1] = SS.sheets[charaId].list[level].MAG;

        setStatus[2] = SS.sheets[charaId].list[level].STR;

        setStatus[3] = SS.sheets[charaId].list[level].VIT;

        setStatus[4] = SS.sheets[charaId].list[level].RES;

        setStatus[5] = SS.sheets[charaId].list[level].INT;

        setStatus[6] = SS.sheets[charaId].list[level].EVA;

        setStatus[7] = SS.sheets[charaId].list[level].CRI;



        return setStatus;
    }
}
