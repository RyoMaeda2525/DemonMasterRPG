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
        DOTween.To(() => _tempHp, // �A���I�ɕω�������Ώۂ̒l
        x => _tempHp = x, // �ω��������l x ���ǂ��������邩������
        hitPoint, // x ���ǂ̒l�܂ŕω������邩�w������
        _hpmpChangeInterval)  // ���b�����ĕω������邩�w������
            .OnUpdate(() => _hpTexts[i].text = _tempHp.ToString("000"));   // ���l���ω�����x�Ɏ��s���鏈��������

        tweener = DOTween.To(() => _hpSliders[i].value, // �A���I�ɕω�������Ώۂ̒l
        x => _hpSliders[i].value = x, // �ω��������l x ���ǂ��������邩������
        hitPoint, // x ���ǂ̒l�܂ŕω������邩�w������
        _hpmpChangeInterval).OnComplete(() =>  DethorLife(i , hitPoint));// ���b�����ĕω������邩�w����*/
    }

    public void ChangeMpValue(int i, int magicPoint)
    {
        DOTween.To(() => _tempMp, // �A���I�ɕω�������Ώۂ̒l
        x => _tempMp = x, // �ω��������l x ���ǂ��������邩������
        magicPoint, // x ���ǂ̒l�܂ŕω������邩�w������
        _hpmpChangeInterval)  // ���b�����ĕω������邩�w������
            .OnUpdate(() => _mpTexts[i].text = _tempMp.ToString("000"));   // ���l���ω�����x�Ɏ��s���鏈��������

        tweener = DOTween.To(() => _mpSliders[i].value, // �A���I�ɕω�������Ώۂ̒l
        x => _mpSliders[i].value = x, // �ω��������l x ���ǂ��������邩������
        magicPoint, // x ���ǂ̒l�܂ŕω������邩�w������
        _hpmpChangeInterval)/*.OnComplete(() => Debug.Log("����"))*/;// ���b�����ĕω������邩�w����*/
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
