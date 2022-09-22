using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
    }

    public void CriticalHit() { StartCoroutine(HitStop());  }

    private IEnumerator HitStop() 
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 1f;
    }
}
