using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tactics : MonoBehaviour
{
    public void ActionSet(PlayerMonsterMove pmm , EnemyMonsterMove emm , TacticsList tactics , List<SKILL> skillList) 
    {
        switch (tactics.tactics_id) 
        {
            case 0:
                break;
            case 1:
                if (pmm != null) 
                {
                    
                }
                else if(emm != null)
                {
                    
                }
                break;
            case 2:
                if (pmm != null)
                {

                }
                else if (emm != null)
                {

                }
                break;
            case 3:
                if (pmm != null)
                {

                }
                else if (emm != null)
                {

                }
                break;
            case 4:
                if (pmm != null)
                {

                }
                else if (emm != null)
                {

                }
                break;
        }
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
