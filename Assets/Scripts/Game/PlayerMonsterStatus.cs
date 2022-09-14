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

    Tactics _tactics = default;

    // Start is called before the first frame update
    void Start()
    {
        NAME = SetStatus.Instance.GetName(_charaId);
        ATTRIBUTE = SetStatus.Instance.GetAttribute(_charaId);
    }

    public void TacticsSet(Tactics tactics) 
    {
        _tactics = tactics;
    }

    void NextLevelUP()
    {
        
    }
}
