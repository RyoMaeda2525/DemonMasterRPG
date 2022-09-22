using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public struct MonsterSkillList
{
    public int chara_id;
    public List<int> skill_id;
}

public class MonsterSkill : MonoBehaviour
{
    [SerializeField, Tooltip("skillの数")]
    private int _skillCount = 0;

    public List<MonsterSkillList> _monsterSkills = null;

    void Awake()
    {
        // 他のゲームオブジェクトにアタッチされているか調べる
        // アタッチされている場合は破棄する。
        CheckInstance();
        _monsterSkills = MonsterSkill_read_csv("MonsterSkill");
    }

    public List<MonsterSkillList> MonsterSkill_read_csv(string name)
    {
        //一時入力用で毎回初期化する構造体とリスト
        MonsterSkillList ms = new MonsterSkillList();
        List<MonsterSkillList> ms_list = new List<MonsterSkillList>();

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
            Debug.Log($"id {csvDatas[i][0]}");
            ms.chara_id = int.Parse(csvDatas[i][0]);
            ms.skill_id = new List<int>();

            for(int j = 0; j < _skillCount; j++) 
            {
                ms.skill_id.Add(int.Parse(csvDatas[i][j]));
            }

            //戻り値のリストに加える
            ms_list.Add(ms);
        }
        return ms_list;
    }

    public List<SKILL> SkillSet(int charaID , int charaLevel) 
    {
        List<SKILL> skillList = new List<SKILL>();

        for (int i = 0; i < _monsterSkills.Count; i++) 
        {
            if(_monsterSkills[charaID].skill_id[i] == 0 || _monsterSkills[charaID].skill_id[i] <= charaLevel)
            skillList.Add(Skill.instance._skill[_monsterSkills[charaID].skill_id[i]]);
        }

        return skillList;
    }

    
    //ここからシングルトンの処理
    public static MonsterSkill instance;

    public static MonsterSkill Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(TacticsManager);

                instance = (MonsterSkill)FindObjectOfType(t);
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
