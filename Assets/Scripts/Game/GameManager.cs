using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField]
    private Text _gameOverText = default;

    [SerializeField]
    private GameObject _menuPanel = null; 

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
    }

    public void CriticalHit() { StartCoroutine(HitStop());  }

    public void MenuOpenOrClose() 
    {
        if (_menuPanel != null)
        {
            if (_menuPanel.activeSelf)
            {
                _menuPanel.SetActive(!_menuPanel.activeSelf);
            }
            else 
            {
                _menuPanel.SetActive(!_menuPanel.activeSelf);
                ItemInventoryManager.Instance.OpenOrCloseInventory();
            }
        }
        else { Debug.Log("Menu画面がありません"); }
    }

    private IEnumerator HitStop() 
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 1f;
    }

    /// <summary>敵モンスターを倒した際にそのモンスターが持っている経験値を獲得</summary>
    public void GainExp(int exp) 
    {
        foreach (var pms in Player.Instance._pms) 
        {
            pms.GetExp(exp);
        }
    }

    /// <summary>味方モンスターが全滅したら呼び出す</summary>
    public void GameOver() 
    {
        _gameOverText.gameObject.SetActive(true);
    }
}
