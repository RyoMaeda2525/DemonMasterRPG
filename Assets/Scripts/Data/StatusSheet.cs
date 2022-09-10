using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusSheet : ScriptableObject
{
    public List<Sheet> sheets = new List<Sheet>();

    /// <summary>Monster�̃X�e�[�^�X���i�[���Ă���XLS�t�@�C��</summary>
    public class Sheet
    {
        public string name = string.Empty;
        public List<Status> list = new List<Status>();
    }

    public class Status
    {
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
    }
}