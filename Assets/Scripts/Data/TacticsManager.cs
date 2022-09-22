using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public struct TacticsList
{
    public int tactics_id;
    public string tactics_name;
    public string tactics_info;
    public int tactics_type;
}

public class TacticsManager : MonoBehaviour
{
    public List<TacticsList> _tactics = default;

    void Awake()
    {
        // 他のゲームオブジェクトにアタッチされているか調べる
        // アタッチされている場合は破棄する。
        CheckInstance();
        _tactics = Tactics_read_csv("Tactics");
    }

    public List<TacticsList> Tactics_read_csv(string name)
    {
        //一時入力用で毎回初期化する構造体とリスト
        TacticsList ts = new TacticsList();
        List<TacticsList> ts_list = new List<TacticsList>();

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
            //Debug.Log("作戦を読み込んだ");
            //Debug.Log($"id {csvDatas[i][0]} name {csvDatas[i][1]} info {csvDatas[i][2]} type {csvDatas[i][3]}");
            ts.tactics_id = int.Parse(csvDatas[i][0]);
            ts.tactics_name = csvDatas[i][1];
            ts.tactics_info = csvDatas[i][2];
            ts.tactics_type = int.Parse(csvDatas[i][3]);

            //戻り値のリストに加える
            ts_list.Add(ts);
        }
        return ts_list;
    }


    public TacticsList[] TacticsSet(int[] tacticsNumber)
    {
        TacticsList[] tactics = new TacticsList[4];

        for (int i = 0; i < tacticsNumber.Length; i++)
        {
            tactics[i].tactics_id = tacticsNumber[i];
            tactics[i].tactics_name = _tactics[i].tactics_name.ToString();
            tactics[i].tactics_info = _tactics[i].tactics_info.ToString();
            tactics[i].tactics_type = _tactics[i].tactics_type;
        }

        return tactics;
    }

    public static TacticsManager instance;

    public static TacticsManager Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(TacticsManager);

                instance = (TacticsManager)FindObjectOfType(t);
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
