using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoutManager : MonoBehaviour
{
    [SerializeField , Tooltip("スカウトが成功したかを表示する")]
    Image[] _image;

    public bool Scout(CharacterRank rank)
    {
        float scoutProbability = ScoutProbability(rank);

        if (scoutProbability > Random.Range(0f, 100f))
        {
            return true;
        }
        else 
        {
            return false;
        }  
    }

    private float ScoutProbability(CharacterRank rank) 
    {
        Debug.Log(rank);

        float probability = 0f;

        switch (rank) 
        {
            case CharacterRank.C:
                probability = 100f;
                break;

            case CharacterRank.B:
                probability = 30f;
                break;

            case CharacterRank.A:
                probability = 10f;
                break;
        }

        
        return probability;
    }
}
