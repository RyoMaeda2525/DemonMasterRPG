using MonsterTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonsterCamera))]
[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(MoveTree))]
public partial class MonsterStatus : MonsterBase
{
    [SerializeField , Header("�q�G�����L�[�ɒu���ꍇ�̓L�����N�^�[�V�[�g���K�v")]
    CharacterSheet _characterSheet;

    [SerializeField, Header("�q�G�����L�[�ɒu���ꍇ�͏������x���̐ݒ肪�K�v")] 
    int _firestLevel = 1;

    AnimationController _controller;

    MoveTree _moveTree;

    Environment _env;

    CharacterType _characterType;

    bool _isScout;

    /// <summary>�v���C���[���_�ł�index</summary>
    int monsterIndex;

    //���ڂł͂Ȃ�MVP�p�^�[���ɂ�����
    MonsterPanelManger PanelManger => UiManager.Instance.MonsterPanel;

    public CharacterType CharacterType => _characterType;

    protected override void Setup(CharacterSheet sheet, int lv)
    {
        _characterSheet = sheet;
        statusSheet = sheet.Sheet;
        _characterType = sheet.Type;
        base.Setup(sheet , lv);
    }

    static public MonsterStatus Create(CharacterSheet sheet, int level) 
    {
        GameObject chara = Instantiate(sheet.Prefab);
        var script = chara.AddComponent<MonsterStatus>();
        script.Setup(sheet , level);
        return script;
    }

    private void Start()
    {
        if (statusSheet == null) { Setup(_characterSheet , _firestLevel); }

        _controller = GetComponent<AnimationController>();
        _moveTree = GetComponent<MoveTree>();
        _env = _moveTree.Env;
        if (_characterType == CharacterType.Player)
        {
            tag = "PlayerMonster";
            Player.Instance.MonstersStatus.Add(this);
            monsterIndex = Player.Instance.MonstersStatus.IndexOf(this);
            PanelManger.MonsterPanalSet(monsterIndex, _characterSheet.Image);
            if (_lv > 1)
            {
                EXP = status[_lv - 2].NEXT_EXP;
            }
            else { EXP = 0; }
        }
        else 
        {
            tag = "EnemyMonster";
            base.ExpSet(); 
        }
    }

    void NextLevel()
    {
        if (EXP >= NEXT_EXP)
        {
            LevelSet(_lv + 1);
            PanelManger.MonsterPanalSet(monsterIndex, _characterSheet.Image);
            NextLevel();
        }
    }

    /// <summary>
    /// �U�����鏈��
    /// �A�j���[�V��������Ă�
    /// </summary>
    public void OnAttack()
    {
        if (_env.target != null)
        {
            float hoge = Random.Range(0f, 100f);
            bool cri = CRI > hoge ? true : false;
            _env.target.AttackDamage(ATK, cri, this);
        }
    }

    /// <summary>���������X�^�[�Ȃ�UI�̃A�N�V������\��</summary>
    /// <param name="skillName"></param>
    public void ActionDecision(string skillName)
    {
        if (CompareTag("PlayerMonster"))
            PanelManger.ActionTextSet(monsterIndex, skillName);
    }

    /// <summary>���������X�^�[�Ȃ�UI�̃A�N�V����������</summary>
    public void ActionEnd()
    {
        if (CompareTag("PlayerMonster"))
            PanelManger.ActionTextDelete(monsterIndex);
    }

    /// <summary>
    /// �����󂯎��
    /// </summary>
    /// <param name="tactics"></param>
    public void TacticsSet(TacticsClass tactics)
    {
        _tactics = tactics;
        _moveTree.ChangeTactics(_tactics.tactics_id);
    }

    /// <summary>
    /// �X�L���̃R�X�g�𕥂�
    /// </summary>
    public void UseSkillCost(int cost)
    {
        MP -= cost;
        if (CompareTag("PlayerMonster"))
            PanelManger.MpSet(this);
    }

    /// <summary>
    /// �U�����󂯂��Ƃ��ɌĂ�
    /// </summary>
    public void AttackDamage(int atk, bool cri, MonsterStatus attaker)
    {
        int damage;
        if (!cri)
        {
            damage = atk / 2 - Def / 4;
            if (damage < 0) { damage = 0; }
        }
        else { damage = atk / 2; }

        HP -= damage;

        if (HP <= 0) { HP = 0; _controller.DethAnimation(); }

        _moveTree.UnderAttack(attaker);

        if (CompareTag("PlayerMonster"))
            PanelManger.HpSet(this);
    }

    /// <summary>�̗͂̉񕜂��󂯂��Ƃ��ɌĂ�</summary>
    public void Heal(int healValue)
    {
        HP += healValue;
        if (HP > HPMax) { HP = HPMax; }
        if (CompareTag("PlayerMonster"))
            PanelManger.HpSet(this);
    }

    /// <summary>�����X�^�[�̎��S����, �A�j���[�V��������Ă� </summary>
    public void Deth()
    {
        if (_characterType == CharacterType.Player)
        {
            PanelManger.MonsterDeth(monsterIndex);

            gameObject.SetActive(false);

            foreach (var pms in Player.Instance.MonstersStatus)
            {
                if (pms.gameObject.activeSelf) { return; }
            }
            GameManager.Instance.GameOver();
        }
        else
        {
            if (_isScout && GameManager.Instance.ScoutManager.Scout(_rank))
            {
                var monster = Create(_characterSheet.PlayerSheet, _lv);
                monster.transform.position = gameObject.transform.position;
            }
            else
            {
               GameManager.Instance.GainExp(EXP);
            }
            Player.Instance.ExitDetectObject(this.gameObject);
            gameObject.SetActive(false);
        }
    }

    public void Scout()
    {
        _isScout = true;
    }

    /// <summary>
    /// �o���l���擾
    /// </summary>
    /// <param name="exp"></param>
    public void GetExp(int exp)
    {
        EXP += exp;
        NextLevel();
    }
}
