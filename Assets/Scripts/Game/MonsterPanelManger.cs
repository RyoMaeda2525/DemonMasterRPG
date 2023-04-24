using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MonsterPanelManger : MonoBehaviour
{
    #region 変数
    [SerializeField] 
    GameObject[] _panels;
    [SerializeField] 
    Image[] _images;
    [SerializeField] 
    Text[] _levelTexts;
    [SerializeField] 
    Text[] _nameTexts;
    [SerializeField] 
    Slider[] _hpSliders;
    [SerializeField] 
    Slider[] _mpSliders;
    [SerializeField] 
    Text[] _hpTexts;
    [SerializeField]
    Text[] _mpTexts;
    [SerializeField] 
    Text[] _actionTexts;
    [SerializeField] 
    float　_hpmpChangeInterval = 1.5f;
    int _tempHp = 0;
    int _tempMp = 0;
    #endregion

    Player Player => Player.Instance;

    /// <summary>
    /// 味方モンスターのパネルを表示する
    /// </summary>
    /// <param name="index"></param>
    /// <param name="image"></param>
    public void MonsterPanalSet(int index , Sprite image) 
    {
        if (Player.MonstersStatus.Count > index) 
        {
            MonsterStatus ms = Player.MonstersStatus[index];

            if (!_panels[index].activeSelf) { _panels[index].SetActive(true); }

            _levelTexts[index].text = "Lv  " + ms.Level;
            _nameTexts[index].text = ms.Name;
            _images[index].sprite = image;
            _hpSliders[index].maxValue = ms.HpMax;
            _mpSliders[index].maxValue = ms.MpMax;
            ChangeHpValue(index, ms.Hp);
            ChangeMpValue(index, ms.Mp);
        }
    }

    /// <summary>
    /// Hpに変更があれば呼ぶ
    /// </summary>
    /// <param name="ms"></param>
    public void HpSet(MonsterStatus ms) 
    {
        int monsterNumber = Player.MonstersStatus.IndexOf(ms);

        ChangeHpValue(monsterNumber, ms.Hp);
    }

    /// <summary>
    /// Mpに変更があれば呼ぶ
    /// </summary>
    /// <param name="ms"></param>
    public void MpSet(MonsterStatus ms)
    {
        int monsterNumber = Player.MonstersStatus.IndexOf(ms);

        ChangeMpValue(monsterNumber, ms.Mp);
    }

    /// <summary>
    /// Hpスライダーを動的に動かす
    /// </sumary>
    /// <param name="i"></param>
    /// <param name="hitPoint"></param>m
    public void ChangeHpValue(int i , int hitPoint)
    {
        DOTween.To(() => _tempHp,
        x => _tempHp = x,
        hitPoint,
        _hpmpChangeInterval)
            .OnUpdate(() => _hpTexts[i].text = _tempHp.ToString("000"));

        DOTween.To(() => _hpSliders[i].value,
        x => _hpSliders[i].value = x,
        hitPoint,
        _hpmpChangeInterval);
    }

    /// <summary>
    /// Mpスライダーを動的に動かす
    /// </summary>
    /// <param name="i"></param>
    /// <param name="magicPoint"></param>
    public void ChangeMpValue(int i, int magicPoint)
    {
        DOTween.To(() => _tempMp,
        x => _tempMp = x,
        magicPoint,_hpmpChangeInterval)
            .OnUpdate(() => _mpTexts[i].text = _tempMp.ToString("000"));

        DOTween.To(() => _mpSliders[i].value,
        x => _mpSliders[i].value = x,
        magicPoint,_hpmpChangeInterval);
    }

    /// <summary>
    /// モンスターが倒れたらパネルを消す
    /// </summary>
    /// <param name="index"></param>
    public void MonsterDeth(int index)
    {
        _panels[index].SetActive(false);
    }

    /// <summary>
    /// モンスターのアクションをテキストで表示
    /// </summary>
    public void ActionTextSet(int index , string actionName) 
    {
        if (!_actionTexts[index].enabled) 
        {
           _actionTexts[index].enabled = true;
        }
        _actionTexts[index].text = actionName;
    }

    /// <summary>
    /// モンスターのアクションがない場合テキストを消す
    /// </summary>
    public void ActionTextDelete(int index) 
    {
        _actionTexts[index].enabled = false;
    }
}
