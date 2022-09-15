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

    int _maxHp = 200; //体力の最大値

    int _hp = 100; //体力の現在地

    int _atk = 5;

    List<int> _skillFlags = new List<int>(); //スキルを習得しているかのフラグ

    string _strategy = "ガンガン"; //作戦の内容

    //float _turnTime = 0; //ターンの間隔

    private void Start()
    {
        for (int i = 0; i < _skill._skill.Count; i++)
        {
            _skillFlags.Add(0); //スキルの数だけ習得がないフラグリストを作る
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
            //    StrategyAction(); //行動開始
            //    _turnTime = 0;
            //}
        }
        else Debug.Log("GameOver");
    }

    public void StrategyAction() //作戦それぞれの動きをする。
    {
        int id = -1;

        switch (_strategy)
        {        //体力が1割を下回るまで最大火力をぶっ放す作戦
            case "ガンガン": 
                if (_hp < _maxHp / 10) //自分ひとりの体力が1割のとき
                {
                    for (int i = 0; i < _skillFlags.Count; i++) //味方単体を回復するスキルのみを探索
                    {
                        if (_skill._skill[i].skill_attribute == "回復" && _skill._skill[i].skill_type[0].target_type == 2 && _skillFlags[i] == 1)
                        {
                            id = i; //idが高いほど相対的にスキルが強くなるので0からあげれば無問題...?
                        }
                    }
                    if (id >= 0) { SkillAction(id); break; }

                    for (int i = 0; i < _skillFlags.Count; i++)//単体スキルがなければ全体
                    {
                        if (_skill._skill[i].skill_attribute == "回復" && _skill._skill[i].skill_type[0].target_type == 3 && _skillFlags[i] == 1)
                        {
                            id = i; //idが高いほど相対的にスキルが強くなるので0からあげれば無問題...?
                        }
                    }
                    if (id >= 0) { SkillAction(id); break; }
                }
                for (int i = 0; i < _skillFlags.Count; i++)//体力が1割以上もしくは回復スキルがないときは最大ダメージで攻撃
                {
                    if (_skill._skill[i].skill_attribute != "回復" && _skill._skill[i].skill_type[0].target_type == 0 && _skillFlags[i] == 1)
                    {
                        id = i; //idが高いほど相対的にスキルが強くなるので0からあげれば無問題...?
                    }
                }
                if (id >= 0) { SkillAction(id); break; }
                Debug.Log("スキルなし");
                break;

                 //体力が5割を下回るまでMPを消費しないスキルで攻撃
            case "いのちだいじに":
                if (_hp < _maxHp / 2) //自分ひとりの体力が5割のとき
                {
                    for (int i = 0; i < _skillFlags.Count; i++) //味方単体を回復するスキルのみを探索
                    {
                        if (_skill._skill[i].skill_attribute == "回復" && _skill._skill[i].skill_type[0].target_type == 2 && _skillFlags[i] == 1)
                        {
                            id = i; //idが高いほど相対的にスキルが強くなるので0からあげれば無問題...?
                        }
                    }
                    if (id >= 0) { SkillAction(id); break; }

                    for (int i = 0; i < _skillFlags.Count; i++)//単体スキルがなければ全体
                    {
                        if (_skill._skill[i].skill_attribute == "回復" && _skill._skill[i].skill_type[0].target_type == 3 && _skillFlags[i] == 1)
                        {
                            id = i; //idが高いほど相対的にスキルが強くなるので0からあげれば無問題...?
                        }
                    }
                    if (id >= 0) { SkillAction(id); break; }
                }
                    for (int i = 0; i < _skillFlags.Count; i++)//体力が5割以上もしくは回復スキルがないときはMPを消費しないスキルで攻撃
                    {
                        if (_skill._skill[i].skill_attribute != "回復" && _skill._skill[i].skill_type[0].target_type == 0 && _skill._skill[i].skill_type[0].effect_cost == 0 && _skillFlags[i] == 1)
                        {
                            id = i; //idが高いほど相対的にスキルが強くなるので0からあげれば無問題...?
                    }
                    }
                    if (id >= 0) { SkillAction(id); break; }
                Debug.Log("スキルなし");
                break;
        }
    }

    /// <summary>
    /// スキルの詳細を見るための関数
    /// </summary>
    /// <param name="id"></param>
    public void SkillInfo(int id)
    {
        _skillFlags[id] = 1;

        SKILL sk = new SKILL();

        sk = _skill._skill[id];

        Debug.Log($"{sk.skill_name} 属性 {sk.skill_attribute} {sk.skill_info} 消費MP {sk.skill_type[0].effect_cost}");
    }

    /// <summary>
    /// スキルを使用する関数
    /// </summary>
    /// <param name="id"></param>
    public void SkillAction(int id)
    {
        if (_skillFlags[id] == 1)
        {
            SKILL sk = new SKILL();

            sk = _skill._skill[id];

            switch (sk.skill_type[0].effect_type)//スキルの種類で処理を判断
            {
                case 1: //物理攻撃のスキル
                    //通常攻撃の場合
                    if (sk.skill_id == 0) { Debug.Log($"プレイヤーの攻撃！　自身に{sk.skill_type[0].effect_value + _atk}のダメージ！"); }
                    //1体に対しての攻撃
                    else if (sk.skill_type[0].target_type == 0) { Debug.Log($"プレイヤーは自身に{sk.skill_name}を放った！自身に{sk.skill_type[0].effect_value + _atk}のダメージ！"); }
                    //全員に対しての攻撃
                    else if (sk.skill_type[0].target_type == 1) { Debug.Log($"プレイヤーは味方全員に{sk.skill_name}を放った！プレイヤーに{sk.skill_type[0].effect_value + _atk}のダメージ！"); }
                    _hp -= sk.skill_type[0].effect_value + _atk;
                    break;

                case 0://回復スキル
                    //単体に対しての回復
                    if (sk.skill_type[0].target_type == 2) { Debug.Log($"プレイヤーは自身に{sk.skill_name}を唱えた。プレイヤーの体力を{sk.skill_type[0].effect_value}回復"); }
                    //全体に対しての回復
                    if (sk.skill_type[0].target_type == 3) { Debug.Log($"プレイヤーは味方全員に{sk.skill_name}を唱えた。プレイヤーの体力を{sk.skill_type[0].effect_value}回復"); }
                    _hp += sk.skill_type[0].effect_value;
                    break;

                case 2://魔法を使ってダメージを与えるスキル
                    //単体に対して
                    if (sk.skill_type[0].target_type == 0) { Debug.Log($"プレイヤーは自身に{sk.skill_name}を唱えた！自身に{sk.skill_type[0].effect_value}のダメージ！"); }
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
