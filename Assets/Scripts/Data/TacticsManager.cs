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
        // ���̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă��邩���ׂ�
        // �A�^�b�`����Ă���ꍇ�͔j������B
        CheckInstance();
        _tactics = Tactics_read_csv("Tactics");
    }

    public List<TacticsList> Tactics_read_csv(string name)
    {
        //�ꎞ���͗p�Ŗ��񏉊�������\���̂ƃ��X�g
        TacticsList ts = new TacticsList();
        List<TacticsList> ts_list = new List<TacticsList>();

        //CSV�̓ǂݍ��݂ɕK�v
        TextAsset csvFile;  // CSV�t�@�C��
        List<string[]> csvDatas = new List<string[]>(); // CSV�̒��g�����郊�X�g
        int height = 0; // CSV�̍s��
        int i = 0;//debug���[�v�J�E���^

        /* Resouces/CSV����CSV�ǂݍ��� */
        csvFile = Resources.Load("CSV" + name) as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(',')); // ���X�g�ɓ����
            height++; // �s�����Z
        }
        for (i = 0; i < height; i++)
        {
            //Debug.Log("����ǂݍ���");
            //Debug.Log($"id {csvDatas[i][0]} name {csvDatas[i][1]} info {csvDatas[i][2]} type {csvDatas[i][3]}");
            ts.tactics_id = int.Parse(csvDatas[i][0]);
            ts.tactics_name = csvDatas[i][1];
            ts.tactics_info = csvDatas[i][2];
            ts.tactics_type = int.Parse(csvDatas[i][3]);

            //�߂�l�̃��X�g�ɉ�����
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
