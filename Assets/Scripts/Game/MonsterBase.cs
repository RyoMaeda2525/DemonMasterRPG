using MonsterTree;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract class MonsterBase : MonoBehaviour
{
    [SerializeField, Tooltip("Status�Ȃǂ��擾����ۂɎg�p���鋤�ʔԍ�")]
    protected int _charaId = 0;

    [SerializeField, Tooltip("Status�Ȃǂ��擾����ۂɎg�p���鋤�ʔԍ�")]
    protected int _firstLv = 0;

    protected Status[] status;

    protected StatusSheet statusSheet;

    #region ���̃X�e�[�^�X
    protected string NAME;
    protected int LV;
    protected Attribute ATTRIBUTE;
    protected int CON;
    protected int MAG;
    protected int STR;
    protected int VIT;
    protected int INT;
    protected int EVA;
    protected int LUK;
    #endregion

    #region ���̃X�e�[�^�X�̃v���p�e�B
    /// <summary>���O</summary>
    public virtual string Name => NAME;
    /// <summary>���x��</summary>
    public virtual int Level => LV;
    /// <summary>����</summary>
    public virtual Attribute Attribute => ATTRIBUTE;
    /// <summary>�R���X�e�B�`���[�V����,�̗�</summary>
    public virtual int Con => CON;
    /// <summary>�}�W�b�N�p���[,����</summary>
    public virtual int Mag => MAG;
    /// <summary>�����I�ȗ�</summary>
    public virtual int Str => STR;
    /// <summary>Vitality,�����I�Ȋ拭���A��Ԉُ�ւ̒�R��</summary>
    public virtual int Vit => VIT;
    /// <summary>Intelligence,�m��</summary>
    public virtual int Int => INT;
    /// <summary>Evasion,���</summary>
    public virtual int Eva => Eva;
    /// <summary>Luck , �^</summary>
    public virtual int Luk => LUK;
    #endregion

    #region �X�e�[�^�X�ւ̃o�t�E�f�o�t�{��
    /// <summary>�q�b�g�|�C���g</summary>
    protected float HP_Buff = 1.0f;
    /// <summary>�}�W�b�N�|�C���g</summary>
    protected float MP_Buff = 1.0f;
    /// <summary>�����I�ȍU���͂�</summary>
    protected float ATK_Buff = 1.0f;
    /// <summary>�����I�Ȋ拭��</summary>
    protected float DEF_Buff = 1.0f;
    /// <summary>�m��</summary>
    protected float MAT_Buff = 1.0f;
    /// <summary>���</summary>
    protected float AVD_Buff = 1.0f;
    /// <summary>Critical,�N���e�B�J���̔�����</summary>
    protected float CRI_Buff = 1.0f;
    #endregion

    #region ���ۂ̃X�e�[�^�X
    protected int HP;
    protected int HPMax;
    protected int MP;
    protected int MPMax;
    protected int ATK;
    protected int DEF;
    protected int MAT;
    protected int AVD;
    protected int CRI;
    protected int EXP;
    protected int NEXT_EXP;
    #endregion

    #region �X�e�[�^�X�̃v���p�e�B
    /// <summary>�q�b�g�|�C���g</summary>
    public int Hp => HP;
    /// <summary>�q�b�g�|�C���g�̍ő�l</summary>
    public int HpMax => HPMax;
    /// <summary>�}�W�b�N�|�C���g</summary>
    public int Mp => MP;
    /// <summary>�}�W�b�N�|�C���g�̍ő�l</summary>
    public int MpMax => MPMax;
    /// <summary>�����I�ȍU���͂�</summary>
    public int Atk => ATK;
    /// <summary>�����I�Ȋ拭��</summary>
    public int Def => DEF;
    /// <summary>�m��</summary>
    public int Mat => MAT;
    /// <summary>���</summary>
    public int Avd => AVD;
    /// <summary>Critical,�N���e�B�J���̔�����</summary>
    public int Cri => CRI;
    /// <summary>�����Ă���o���l�̑���</summary>
    public int Exp => Exp;
    /// <summary>���̃��x���ւ̌o���l�̑���</summary>
    public int NextExp => NEXT_EXP;
    #endregion

    /// <summary>�^����ꂽ���</summary>
    protected TacticsClass _tactics = default;

    /// <summary>�g�p�ł���X�L��</summary>
    protected List<SkillAssets> _skillList = new List<SkillAssets>();

    /// <summary>�g�p�ł���X�L��</summary>
    public List<SkillAssets> SkillList => _skillList;

    // Start is called before the first frame update
    protected virtual void  Start()
    {
        statusSheet = GameManager.Instance.StatusSheet[_charaId];
        status = statusSheet.status;
        NAME = status[0].NAME;
        ATTRIBUTE = status[0].ATTRIBUTE;
        if (LV == 0)
        {
            LevelSet(_firstLv);
        }
        SkillSet();
    }

    protected void LevelSet(int level)
    {
        LV = level;
        NEXT_EXP = status[level].NEXT_EXP;
        StatusSet();
        if (LV == _firstLv) { HP = HPMax; MP = MPMax; }
    }

    protected void SkillSet()
    {
        foreach (var skill in statusSheet.skills) 
        {
            if (LV > skill.LearnLv && !_skillList.Contains(skill.Skill)) 
            {
                _skillList.Add(skill.Skill);
            }
        }
    }

    protected void StatusSet()
    {
        CON = status[LV].CON;
        MAG = status[LV].MAG;
        STR = status[LV].STR;
        VIT = status[LV].VIT;
        INT = status[LV].INT;
        EVA = status[LV].EVA;
        LUK = status[LV].LUK;

        HPMax = (int)(CON * HP_Buff);
        MPMax = (int)(MAG * MP_Buff);
        ATK = (int)(STR * ATK_Buff);
        DEF = (int)(VIT * DEF_Buff);
        MAT = (int)(INT * MAT_Buff);
        AVD = (int)(EVA * AVD_Buff);
        CRI = (int)(LUK * CRI_Buff);
    }

    public void GetExp(int exp)
    {
        EXP += exp;
    }

    private void NextLevel()
    {
        if (EXP >= NEXT_EXP) 
        { 
            LevelSet(LV + 1); 
            Debug.Log("���x���A�b�v!!");
            NextLevel();
        }
    }

    /// <summary>�G�̏ꍇ�ɖႦ��o���l���Z�b�g����</summary>
    protected void ExpSet() 
    {
        EXP = status[Level].ENEMY_EXP;
    }
}
