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
    [SerializeField, Tooltip("skill�̐�")]
    private int _skillCount = 0;

    public List<MonsterSkillList> _monsterSkills = null;

    void Awake()
    {
        // ���̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă��邩���ׂ�
        // �A�^�b�`����Ă���ꍇ�͔j������B
        CheckInstance();
        _monsterSkills = MonsterSkill_read_csv("MonsterSkill");
    }

    public List<MonsterSkillList> MonsterSkill_read_csv(string name)
    {
        //�ꎞ���͗p�Ŗ��񏉊�������\���̂ƃ��X�g
        MonsterSkillList ms = new MonsterSkillList();
        List<MonsterSkillList> ms_list = new List<MonsterSkillList>();

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
            Debug.Log($"id {csvDatas[i][0]}");
            ms.chara_id = int.Parse(csvDatas[i][0]);
            ms.skill_id = new List<int>();

            for(int j = 0; j < _skillCount; j++) 
            {
                ms.skill_id.Add(int.Parse(csvDatas[i][j]));
            }

            //�߂�l�̃��X�g�ɉ�����
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

    
    //��������V���O���g���̏���
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
