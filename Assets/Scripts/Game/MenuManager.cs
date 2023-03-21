using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField , Header("メニュー画面のテキスト")]
     Text _menuText;

     List<GameObject> _menuPanels = new List<GameObject>();

    void Awake()
    {
        foreach (Transform panel in transform)
        {
            if (!panel.CompareTag("Text"))
                _menuPanels.Add(panel.gameObject);
        }
    }

    void OnEnable()
    {
        _menuPanels.First().SetActive(true);
    }

    void OnDisable()
    {
        foreach (var panel in _menuPanels)
        {
            panel.SetActive(false);
        }
    }

    /// <summary>選んだ作戦の説明文を表示する</summary>
    /// <param name="text"></param>
    public void TextSet(string text)
    {
        _menuText.text = text;
    }
}
