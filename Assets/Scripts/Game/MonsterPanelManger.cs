using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MonsterPanelManger : SingletonMonoBehaviour<MonsterPanelManger>
{
    [SerializeField] GameObject[] _panels;
    [SerializeField] Image[] _images;
    [SerializeField] Text[] _levelTexts;
    [SerializeField] Text[] _nameTexts;
    [SerializeField] Slider[] _hpSliders;
    [SerializeField] Slider[] _mpSliders;
    [SerializeField] Text[] _hpTexts;
    [SerializeField] Text[] _mpTexts;
    [SerializeField] float _hpmpChangeInterval = 1.5f;

    int _tempHp = 0;
    int _tempMp = 0;
    Tweener tweener = default;

    public void PanalInfoSet(PlayerMonsterStatus pms) 
    {
        if (Player.Instance._pms.Contains(pms)) 
        {
            int monsterNumber = Player.Instance._pms.IndexOf(pms);

            if (!_panels[monsterNumber].activeSelf) { _panels[monsterNumber].SetActive(true); }

            _levelTexts[monsterNumber].text = "Lv  " + pms.LV.ToString();
            _nameTexts[monsterNumber].text = pms.NAME;
            _hpSliders[monsterNumber].maxValue = pms.HPMax;
            _mpSliders[monsterNumber].maxValue = pms.MPMax;
            ChangeHpValue(monsterNumber, pms.HP);
            ChangeMpValue(monsterNumber, pms.MP);
        }
    }

    public void HpSet(PlayerMonsterStatus pms) 
    {
        int monsterNumber = Player.Instance._pms.IndexOf(pms);

        ChangeHpValue(monsterNumber, pms.HP);
    }

    public void MpSet(PlayerMonsterStatus pms)
    {
        int monsterNumber = Player.Instance._pms.IndexOf(pms);

        ChangeMpValue(monsterNumber, pms.MP);
    }

    public void ChangeHpValue(int i , int hitPoint)
    {
        DOTween.To(() => _tempHp, // 連続的に変化させる対象の値
        x => _tempHp = x, // 変化させた値 x をどう処理するかを書く
        hitPoint, // x をどの値まで変化させるか指示する
        _hpmpChangeInterval)  // 何秒かけて変化させるか指示する
            .OnUpdate(() => _hpTexts[i].text = _tempHp.ToString("000"));   // 数値が変化する度に実行する処理を書く

        tweener = DOTween.To(() => _hpSliders[i].value, // 連続的に変化させる対象の値
        x => _hpSliders[i].value = x, // 変化させた値 x をどう処理するかを書く
        hitPoint, // x をどの値まで変化させるか指示する
        _hpmpChangeInterval).OnComplete(() =>  DethorLife(i , hitPoint));// 何秒かけて変化させるか指示す*/
    }

    public void ChangeMpValue(int i, int magicPoint)
    {
        DOTween.To(() => _tempMp, // 連続的に変化させる対象の値
        x => _tempMp = x, // 変化させた値 x をどう処理するかを書く
        magicPoint, // x をどの値まで変化させるか指示する
        _hpmpChangeInterval)  // 何秒かけて変化させるか指示する
            .OnUpdate(() => _mpTexts[i].text = _tempMp.ToString("000"));   // 数値が変化する度に実行する処理を書く

        tweener = DOTween.To(() => _mpSliders[i].value, // 連続的に変化させる対象の値
        x => _mpSliders[i].value = x, // 変化させた値 x をどう処理するかを書く
        magicPoint, // x をどの値まで変化させるか指示する
        _hpmpChangeInterval)/*.OnComplete(() => Debug.Log("完了"))*/;// 何秒かけて変化させるか指示す*/
    }

    private void DethorLife(int i , int hitpoint) 
    {
        if (hitpoint == 0) { Deth(Player.Instance._pms[i]); }
    }

    public void Deth(PlayerMonsterStatus pms)
    {
        int monsterNumber = Player.Instance._pms.IndexOf(pms);

        _panels[monsterNumber].SetActive(false);
    }
}
