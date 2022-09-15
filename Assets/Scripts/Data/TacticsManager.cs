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
        //�ꎞ���͗p�Ŗ��񏉊�������\���̂ƃ��X�g
        Tactics ts = new Tactics();
        List<Tactics> ts_list = new List<Tactics>();

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
            Debug.Log("�X�L����ǂݍ���");
            Debug.Log($"id {csvDatas[i][0]} name {csvDatas[i][1]} info {csvDatas[i][2]} attribute {csvDatas[i][3]}");
            ts.tactics_id = int.Parse(csvDatas[i][0]);
            ts.tactics_name = csvDatas[i][1];
            ts.tactics_info = csvDatas[i][2];
            ts.tactics_type = int.Parse(csvDatas[i][3]);

            //�߂�l�̃��X�g�ɉ�����
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
