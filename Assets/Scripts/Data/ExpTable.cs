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
        // ���̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă��邩���ׂ�
        // �A�^�b�`����Ă���ꍇ�͔j������B
        CheckInstance();
        _expTable = EXPTable_read_csv("ExpTable");
    }

    public List<EXPTable> EXPTable_read_csv(string name)
    {
        //�ꎞ���͗p�Ŗ��񏉊�������\���̂ƃ��X�g
        EXPTable et = new EXPTable();
        List<EXPTable> et_list = new List<EXPTable>();

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
            Debug.Log("�����X�^�[���ێ�����o���l��ǂݍ���");
            et.chara_id = int.Parse(csvDatas[i][0]);
            et.enemy_exp = int.Parse(csvDatas[i][1]); ;
            et.nextlebel_exps = new List<int>();

            for (int j = 0; j < csvDatas[i].Length; j++)
            {
                    et.nextlebel_exps.Add(int.Parse(csvDatas[i][j]));
            }

            //�߂�l�̃��X�g�ɉ�����
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
