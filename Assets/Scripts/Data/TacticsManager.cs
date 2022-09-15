using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public struct Tactics
{
    public int tactics_id;
    public string tactics_name;
    public string tactics_info;
    public int tactics_type;
}

public class TacticsManager : SingletonMonoBehaviour<TacticsManager>
{
    public List<Tactics> _tactics = default;

    void Awake()
    {
        _tactics = Tactics_read_csv("Tactics");
    }

    public List<Tactics> Tactics_read_csv(string name)
    {
        //一時入力用で毎回初期化する構造体とリスト
        Tactics ts = new Tactics();
        List<Tactics> ts_list = new List<Tactics>();

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
            Debug.Log("スキルを読み込んだ");
            Debug.Log($"id {csvDatas[i][0]} name {csvDatas[i][1]} info {csvDatas[i][2]} attribute {csvDatas[i][3]}");
            ts.tactics_id = int.Parse(csvDatas[i][0]);
            ts.tactics_name = csvDatas[i][1];
            ts.tactics_info = csvDatas[i][2];
            ts.tactics_type = int.Parse(csvDatas[i][3]);

            //戻り値のリストに加える
            ts_list.Add(ts);
        }
        return ts_list;
    }


        public Tactics[] TacticsSet(int[] tacticsNumber) 
    {
        Tactics[] tactics = new Tactics[4];

        for (int i = 0; i < tacticsNumber.Length; i++) 
        {
            tactics[i].tactics_id = tacticsNumber[i];
            tactics[i].tactics_name = _tactics[i].tactics_name.ToString();
            tactics[i].tactics_info = _tactics[i].tactics_info.ToString();
            tactics[i].tactics_type = _tactics[i].tactics_type;
        }

        return tactics;
    }
    
}
