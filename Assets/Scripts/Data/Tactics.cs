using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tactics : MonoBehaviour
{
    public SKILL ActionSet(PlayerMonsterMove pmm, EnemyMonsterMove emm, TacticsList tactics, List<SKILL> skillList)
    {
        switch (tactics.tactics_id)
        {
            case 1:
                pmm._target = null;
                return new SKILL();
            case 2: //�ڂ̑O�̓G���U��
                if (pmm != null) //�G���܂��_���Ă��Ȃ��Ƃ�
                {
                    if (pmm._target == null && Player.Instance._emmList.Count > 0)
                    {
                        pmm._target = Player.Instance._emmList[0].gameObject;
                        return skillList[0];
                    }
                    else if(pmm._target != null) { return skillList[0]; }
                    else return new SKILL();
                }
                else if (emm != null)
                {
                    return skillList[0];
                }
                break;
            case 3:
                if (pmm != null)
                {
                    if (CameraChange.Instance._target != null)
                    {
                        pmm._target = CameraChange.Instance._target.gameObject;
                        return skillList[0];
                    }
                    else return new SKILL();
                }
                else if (emm != null)
                {
                    return skillList[0];
                }
                break;
            case 4:
                if (pmm != null)
                {
                    return new SKILL();
                }
                else if (emm != null)
                {
                    return new SKILL();
                }
                break;
            case 5:
                if (pmm != null)
                {
                    return skillList[0];
                }
                else if (emm != null)
                {
                    return skillList[0];
                }
                break;
        }
        return skillList[0];
    }

    // Start is called before the first frame update
    void Awake()
    {
        // ���̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă��邩���ׂ�
        // �A�^�b�`����Ă���ꍇ�͔j������B
        CheckInstance();
    }

    public static Tactics instance;

    public static Tactics Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(Tactics);

                instance = (Tactics)FindObjectOfType(t);
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
