using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

/// <summary>
/// スキルの効果を格納する構造体
/// </summary>
public struct Skill_Type
{
    public int effect_type; //攻撃スキルや回復スキルなどの種類
    public int effect_value; //スキルで与えられる効果量
    public int target_type; //スキルの対象の種類(味方ひとり,味方全体 ,敵単体)
    public int effect_cost; //スキルで消費するコスト
}
/// <summary>
/// スキルの情報を格納する構造体
/// </summary>
public struct SKILL
{
    //public int skill_flag;　//習得しているかの判定用
    public int skill_id;　　//スキルのID
    public string skill_name; //スキルの名前
    public string skill_info; //スキルの説明
    public string skill_attribute; //属性の種類(無属性,火属性)
    public List<Skill_Type> skill_type; //Skill_Typeの効果を格納するList
}

public class Skill : MonoBehaviour
{
    public List<SKILL> skill = default;

    void Awake()
    {
        skill = SKILL_read_csv("Skill");
    }

    public List<SKILL> SKILL_read_csv(string name)
    {
        //一時入力用で毎回初期化する構造体とリスト
        SKILL sk = new SKILL();
        List<SKILL> sk_list = new List<SKILL>();

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
            sk.skill_id = int.Parse(csvDatas[i][0]);
            sk.skill_name = csvDatas[i][1];
            sk.skill_info = csvDatas[i][2];
            sk.skill_attribute = csvDatas[i][3];
            sk.skill_type = new List<Skill_Type>();

            Skill_Type tmp = new Skill_Type();
            tmp.effect_type = int.Parse(csvDatas[i][4]);
            tmp.effect_value = int.Parse(csvDatas[i][5]);
            tmp.target_type = int.Parse(csvDatas[i][6]);
            tmp.effect_cost = int.Parse(csvDatas[i][7]);
            sk.skill_type.Add(tmp);

            //戻り値のリストに加える
            sk_list.Add(sk);
        }
        return sk_list;
    }
}
