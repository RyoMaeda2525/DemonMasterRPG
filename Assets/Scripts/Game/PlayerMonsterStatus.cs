using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMonsterStatus : MonoBehaviour
{
    [SerializeField, Tooltip("Status�Ȃǂ��擾����ۂɎg�p���鋤�ʔԍ�")]
    int _charaId = 0;

    //�����ȃX�e�[�^�X

    /// <summary>���x��</summary>
    public int LV;
    /// <summary>���O</summary>
    public string NAME;
    /// <summary>����</summary>
    public int ATTRIBUTE;
    /// <summary>�R���X�e�B�`���[�V����,�̗�</summary>
    public int CON;
    /// <summary>�}�W�b�N�p���[,����</summary>
    public int MAG;
    /// <summary>�����I�ȗ�</summary>
    public int STR;
    /// <summary>Vitality,�����I�Ȋ拭���A��Ԉُ�ւ̒�R��</summary>
    public int VIT;
    /// <summary>Resist,���@�ɑ΂��Ă̒�R��</summary>
    public int RES;
    /// <summary>Intelligence,�m��</summary>
    public int INT;
    /// <summary>Evasion,���</summary>
    public int EVA;
    /// <summary>Critical,�N���e�B�J���̔�����</summary>
    public int CRI;

    void LevelSet(int level)
    {
        LV = level;
    }

    //------------�v�Z��̃X�e�[�^�X�Ȃ�---------------
    [SerializeField, Tooltip("�s������܂ł̎���")]
    private int _actionTime = 5;

    /// <summary>�q�b�g�|�C���g</summary>
    public int HP;
    /// <summary>�}�W�b�N�|�C���g</summary>
    public int MP;
    /// <summary>�����I�ȍU���͂ւ̃o�t</summary>
    public int ATK_Buff;
    /// <summary>�����I�Ȋ拭�ւ̃o�t</summary>
    public int DEF_Buff;
    /// <summary>���@�ɑ΂��Ă̒�R�͂ւ̃o�t</summary>
    public int MDEF_Buff;
    /// <summary>�m�͂ւ̃o�t</summary>
    public int INT_Buff;
    /// <summary>��𗦂ւ̃o�t</summary>
    public int EVA_Buff;
    /// <summary>�N���e�B�J���̔������ւ̃o�t</summary>
    public int CRI_Buff;
    /// <summary>�����Ă���o���l�̑���</summary>
    public int EXP;
    /// <summary>���̃��x���ւ̌o���l�̑���</summary>
    public int NEXT_EXP;

    /// <summary>�^����ꂽ���</summary>
    private TacticsList _tactics = default;

    /// <summary>�^����ꂽ���</summary>
    List<SKILL> _skillList = new List<SKILL>();

    ///<summary>�s���܂ł̎��Ԃ�}��</summary>
    private float _actionTimer = 0;

    /// <summary>�퓬�����ǂ���</summary>
    private bool _actionBool = false;

    // Start is called before the first frame update
    void Start()
    {
        NAME = SetStatus.Instance.GetName(_charaId);
        ATTRIBUTE = SetStatus.Instance.GetAttribute(_charaId);
    }

    private void SkillSet() 
    {
        _skillList = MonsterSkill.instance.SkillSet(_charaId, LV);
    }

    public void TacticsSet(TacticsList tactics) 
    {
        _tactics = tactics;
    }

    private void FixedUpdate()
    {
        if (!_actionBool) {return;}

        _actionTimer += Time.deltaTime;

        if (_actionTimer > _actionTime) 
        {
            Tactics.instance.ActionSet(_tactics, _skillList);
        }
    }

    void NextLevelUP()
    {
        
    }
}
