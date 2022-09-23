using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public struct EXPTable
{
    public int chara_id;
    public int enemy_exp;
    public List<int> nextlebel_exps;
}

public class ExpTable : MonoBehaviour
{
    public List<EXPTable> _expTable;

    void Awake()
    {
        // 他のゲームオブジェクトにアタッチされているか調べる
        // アタッチされている場合は破棄する。
        CheckInstance();
        _expTable = EXPTable_read_csv("ExpTable");
    }

    public List<EXPTable> EXPTable_read_csv(string name)
    {
        //一時入力用で毎回初期化する構造体とリスト
        EXPTable et = new EXPTable();
        List<EXPTable> et_list = new List<EXPTable>();

        //CSVの読み込みに必要
        TextAsset csvFile;  // CSVファイル
        List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト
        int height = 0; // CSVの行数
        int i = 0;//debugループカウンタ

        /* Resouces/CSV下のCSV読み込み */
        csvFile = Resources.Load("CSV" + name) as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(',')); // リストに入れる
            height++; // 行数加算
        }
        for (i = 0; i < height; i++)
        {
            Debug.Log("モンスターが保持する経験値を読み込んだ");
            et.chara_id = int.Parse(csvDatas[i][0]);
            et.enemy_exp = int.Parse(csvDatas[i][1]); ;
            et.nextlebel_exps = new List<int>();

            for (int j = 0; j < csvDatas[i].Length; j++)
            {
                    et.nextlebel_exps.Add(int.Parse(csvDatas[i][j]));
            }

            //戻り値のリストに加える
            et_list.Add(et);
        }
        return et_list;
    }

    public int GetNextExp(int charaid , int level) 
    {
        return _expTable[charaid].nextlebel_exps[level];
    }

    public static ExpTable instance;

    public static ExpTable Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(ExpTable);

                instance = (ExpTable)FindObjectOfType(t);
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
