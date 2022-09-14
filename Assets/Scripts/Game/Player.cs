using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Tooltip("�g�����𓾂邽�߂Ɏ擾����")]
    TacticsManager _tM = default;

    /// <summary>�q�b�g�|�C���g</summary>
    public int HP;
    /// <summary>�����I�ȍU���͂ւ̃o�t</summary>
    public int ATK_Buff;
    /// <summary>�����I�Ȋ拭�ւ̃o�t</summary>
    public int DEF_Buff;
    /// <summary>���@�ɑ΂��Ă̒�R�͂ւ̃o�t</summary>
    public int MDEF_Buff;
    /// <summary>��𗦂ւ̃o�t</summary>
    public int EVA_Buff;
    /// <summary>�N���e�B�J���̔������ւ̃o�t</summary>
    public int CRI_Buff;
    /// <summary>�����Ă���o���l�̑���</summary>
    public int EXP;
    /// <summary>���̃��x���ւ̌o���l�̑���</summary>
    public int NEXT_EXP;

    Tactics[] _tacticsArray = default;

    void SetTactics(Tactics[] tactics)
    {
        _tacticsArray = tactics;
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
