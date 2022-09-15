using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTest : MonoBehaviour
{
    [SerializeField]
    Skill _skill = null;

    [SerializeField]
    InputField _inputField = null;

    int _maxHp = 200; //�̗͂̍ő�l

    int _hp = 100; //�̗͂̌��ݒn

    int _atk = 5;

    List<int> _skillFlags = new List<int>(); //�X�L�����K�����Ă��邩�̃t���O

    string _strategy = "�K���K��"; //���̓��e

    //float _turnTime = 0; //�^�[���̊Ԋu

    private void Start()
    {
        for (int i = 0; i < _skill._skill.Count; i++)
        {
            _skillFlags.Add(0); //�X�L���̐������K�����Ȃ��t���O���X�g�����
        }
        _skillFlags[0] = 1;
    }

    private void Update()
    {
        if (_hp > 0)
        {
            //_turnTime += Time.deltaTime;
            //if (_turnTime > 3) 
            //{
            //    StrategyAction(); //�s���J�n
            //    _turnTime = 0;
            //}
        }
        else Debug.Log("GameOver");
    }

    public void StrategyAction() //��킻�ꂼ��̓���������B
    {
        int id = -1;

        switch (_strategy)
        {        //�̗͂�1���������܂ōő�Η͂��Ԃ��������
            case "�K���K��": 
                if (_hp < _maxHp / 10) //�����ЂƂ�̗̑͂�1���̂Ƃ�
                {
                    for (int i = 0; i < _skillFlags.Count; i++) //�����P�̂��񕜂���X�L���݂̂�T��
                    {
                        if (_skill._skill[i].skill_attribute == "��" && _skill._skill[i].skill_type[0].target_type == 2 && _skillFlags[i] == 1)
                        {
                            id = i; //id�������قǑ��ΓI�ɃX�L���������Ȃ�̂�0���炠����Ζ����...?
                        }
                    }
                    if (id >= 0) { SkillAction(id); break; }

                    for (int i = 0; i < _skillFlags.Count; i++)//�P�̃X�L�����Ȃ���ΑS��
                    {
                        if (_skill._skill[i].skill_attribute == "��" && _skill._skill[i].skill_type[0].target_type == 3 && _skillFlags[i] == 1)
                        {
                            id = i; //id�������قǑ��ΓI�ɃX�L���������Ȃ�̂�0���炠����Ζ����...?
                        }
                    }
                    if (id >= 0) { SkillAction(id); break; }
                }
                for (int i = 0; i < _skillFlags.Count; i++)//�̗͂�1���ȏ�������͉񕜃X�L�����Ȃ��Ƃ��͍ő�_���[�W�ōU��
                {
                    if (_skill._skill[i].skill_attribute != "��" && _skill._skill[i].skill_type[0].target_type == 0 && _skillFlags[i] == 1)
                    {
                        id = i; //id�������قǑ��ΓI�ɃX�L���������Ȃ�̂�0���炠����Ζ����...?
                    }
                }
                if (id >= 0) { SkillAction(id); break; }
                Debug.Log("�X�L���Ȃ�");
                break;

                 //�̗͂�5���������܂�MP������Ȃ��X�L���ōU��
            case "���̂���������":
                if (_hp < _maxHp / 2) //�����ЂƂ�̗̑͂�5���̂Ƃ�
                {
                    for (int i = 0; i < _skillFlags.Count; i++) //�����P�̂��񕜂���X�L���݂̂�T��
                    {
                        if (_skill._skill[i].skill_attribute == "��" && _skill._skill[i].skill_type[0].target_type == 2 && _skillFlags[i] == 1)
                        {
                            id = i; //id�������قǑ��ΓI�ɃX�L���������Ȃ�̂�0���炠����Ζ����...?
                        }
                    }
                    if (id >= 0) { SkillAction(id); break; }

                    for (int i = 0; i < _skillFlags.Count; i++)//�P�̃X�L�����Ȃ���ΑS��
                    {
                        if (_skill._skill[i].skill_attribute == "��" && _skill._skill[i].skill_type[0].target_type == 3 && _skillFlags[i] == 1)
                        {
                            id = i; //id�������قǑ��ΓI�ɃX�L���������Ȃ�̂�0���炠����Ζ����...?
                        }
                    }
                    if (id >= 0) { SkillAction(id); break; }
                }
                    for (int i = 0; i < _skillFlags.Count; i++)//�̗͂�5���ȏ�������͉񕜃X�L�����Ȃ��Ƃ���MP������Ȃ��X�L���ōU��
                    {
                        if (_skill._skill[i].skill_attribute != "��" && _skill._skill[i].skill_type[0].target_type == 0 && _skill._skill[i].skill_type[0].effect_cost == 0 && _skillFlags[i] == 1)
                        {
                            id = i; //id�������قǑ��ΓI�ɃX�L���������Ȃ�̂�0���炠����Ζ����...?
                    }
                    }
                    if (id >= 0) { SkillAction(id); break; }
                Debug.Log("�X�L���Ȃ�");
                break;
        }
    }

    /// <summary>
    /// �X�L���̏ڍׂ����邽�߂̊֐�
    /// </summary>
    /// <param name="id"></param>
    public void SkillInfo(int id)
    {
        _skillFlags[id] = 1;

        SKILL sk = new SKILL();

        sk = _skill._skill[id];

        Debug.Log($"{sk.skill_name} ���� {sk.skill_attribute} {sk.skill_info} ����MP {sk.skill_type[0].effect_cost}");
    }

    /// <summary>
    /// �X�L�����g�p����֐�
    /// </summary>
    /// <param name="id"></param>
    public void SkillAction(int id)
    {
        if (_skillFlags[id] == 1)
        {
            SKILL sk = new SKILL();

            sk = _skill._skill[id];

            switch (sk.skill_type[0].effect_type)//�X�L���̎�ނŏ����𔻒f
            {
                case 1: //�����U���̃X�L��
                    //�ʏ�U���̏ꍇ
                    if (sk.skill_id == 0) { Debug.Log($"�v���C���[�̍U���I�@���g��{sk.skill_type[0].effect_value + _atk}�̃_���[�W�I"); }
                    //1�̂ɑ΂��Ă̍U��
                    else if (sk.skill_type[0].target_type == 0) { Debug.Log($"�v���C���[�͎��g��{sk.skill_name}��������I���g��{sk.skill_type[0].effect_value + _atk}�̃_���[�W�I"); }
                    //�S���ɑ΂��Ă̍U��
                    else if (sk.skill_type[0].target_type == 1) { Debug.Log($"�v���C���[�͖����S����{sk.skill_name}��������I�v���C���[��{sk.skill_type[0].effect_value + _atk}�̃_���[�W�I"); }
                    _hp -= sk.skill_type[0].effect_value + _atk;
                    break;

                case 0://�񕜃X�L��
                    //�P�̂ɑ΂��Ẳ�
                    if (sk.skill_type[0].target_type == 2) { Debug.Log($"�v���C���[�͎��g��{sk.skill_name}���������B�v���C���[�̗̑͂�{sk.skill_type[0].effect_value}��"); }
                    //�S�̂ɑ΂��Ẳ�
                    if (sk.skill_type[0].target_type == 3) { Debug.Log($"�v���C���[�͖����S����{sk.skill_name}���������B�v���C���[�̗̑͂�{sk.skill_type[0].effect_value}��"); }
                    _hp += sk.skill_type[0].effect_value;
                    break;

                case 2://���@���g���ă_���[�W��^����X�L��
                    //�P�̂ɑ΂���
                    if (sk.skill_type[0].target_type == 0) { Debug.Log($"�v���C���[�͎��g��{sk.skill_name}���������I���g��{sk.skill_type[0].effect_value}�̃_���[�W�I"); }
                    _hp -= sk.skill_type[0].effect_value;
                    break;
            }
        }
        if (_hp > _maxHp) _hp = _maxHp;
        Debug.Log(_hp);
    }

    public void StrategySet() 
    {
        _strategy = _inputField.text;
        Debug.Log(_strategy);
    }
}
