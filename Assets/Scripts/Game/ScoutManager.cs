using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoutManager : MonoBehaviour
{
    [SerializeField]
    GameObject _scoutPanel = null;

    [SerializeField , Tooltip("スカウトが成功したかを表示する")]
    Image[] _image;

    public void Scout(EnemyMonsterMove emm)
    {
        EnemyMonsterStatus ems = emm.gameObject.GetComponent<EnemyMonsterStatus>();

        float scoutProbability = ScoutProbability(ems.HP / ems.HPMax);

        Debug.Log(scoutProbability);

        if (scoutProbability > Random.Range(0f, 100f))
        {
            emm.ScoutSuccess();
            StartCoroutine(ScoutSuccess(3f));
        }
        else { StartCoroutine(ScoutFaild(3f)); }  
    }

    private float ScoutProbability(float hpRatio) 
    {
        float scoutProbability = 0;

        if (hpRatio > 0.8)
        {
            scoutProbability = Random.Range(0.1f, 5f);
        }
        else if (hpRatio > 0.5)
        {
            scoutProbability = Random.Range(8f, 15f);
        }
        else if (hpRatio > 0.3)
        {
            scoutProbability = Random.Range(20f, 40f);
        }
        else if (hpRatio > 0.1)
        {
            scoutProbability = Random.Range(40f, 60f);
        }
        return scoutProbability;
    }

    private IEnumerator ScoutSuccess(float waitTime)
    {
        Debug.Log("ScoutScsees");
        Time.timeScale = 0;
        yield return new WaitForSeconds(waitTime);
        //_image[0].gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private IEnumerator ScoutFaild(float waitTime) 
    {
        Debug.Log("ScoutFaild");
        Time.timeScale = 0;
        yield return new WaitForSeconds(waitTime);
        //_image[1].gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
