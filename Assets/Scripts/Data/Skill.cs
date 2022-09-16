using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

/// <summary>
/// �X�L���̌��ʂ��i�[����\����
/// </summary>
public struct Skill_Type
{
    public int effect_type; //�U���X�L����񕜃X�L���Ȃǂ̎��
    public int effect_value; //�X�L���ŗ^��������ʗ�
    public int target_type; //�X�L���̑Ώۂ̎��(�����ЂƂ�,�����S�� ,�G�P��)
    public int effect_cost; //�X�L���ŏ����R�X�g
}
/// <summary>
/// �X�L���̏����i�[����\����
/// </summary>
public struct SKILL
{
    //public int skill_flag;�@//�K�����Ă��邩�̔���p
    public int skill_id;�@�@//�X�L����ID
    public string skill_name; //�X�L���̖��O
    public string skill_info; //�X�L���̐���
    public string skill_attribute; //�����̎��(������,�Α���)
    public List<Skill_Type> skill_type; //Skill_Type�̌��ʂ��i�[����List
}

public class Skill : MonoBehaviour
{
    public List<SKILL> _skill = default;

    void Awake()
    {
        // ���̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă��邩���ׂ�
        // �A�^�b�`����Ă���ꍇ�͔j������B
        CheckInstance();
        _skill = SKILL_read_csv("Skill");
    }

    public List<SKILL> SKILL_read_csv(string name)
    {
        //�ꎞ���͗p�Ŗ��񏉊�������\���̂ƃ��X�g
        SKILL sk = new SKILL();
        List<SKILL> sk_list = new List<SKILL>();

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

            //�߂�l�̃��X�g�ɉ�����
            sk_list.Add(sk);
        }
        return sk_list;
    }

    public static Skill instance;

    public static Skill Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(Skill);

                instance = (Skill)FindObjectOfType(t);
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
