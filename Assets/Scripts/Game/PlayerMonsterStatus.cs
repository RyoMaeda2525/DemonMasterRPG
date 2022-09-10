using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMonsterStatus : MonoBehaviour
{
    [SerializeField, Tooltip("Status�Ȃǂ��擾����ۂɎg�p���鋤�ʔԍ�")]
    int _charaId = 0;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
