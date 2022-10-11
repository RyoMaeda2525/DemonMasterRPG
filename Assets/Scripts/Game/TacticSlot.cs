using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class TacticSlot : UIBehaviour, ILayoutGroup
{
    [SerializeField , Tooltip("リングスロットの半径")]
    float radius = 100;
    
    [SerializeField , Tooltip("リングスロットを回す量")]
    public float offsetAngle;

    [SerializeField, Tooltip("作戦スロットのText")]
    private Text[] _tacticsTextArray;

    [SerializeField , Tooltip("スクロールしたときの変化量")]
    private float _scrollValue = 90;

    [SerializeField, Tooltip("回転が終わるまでの時間")]
    private float _wheelChangeInterval = 1.5f;

    [SerializeField, Tooltip("色変更が終わるまでの時間")]
    private float _colorChangeInterval = 1.5f;

    public int _selectIndex = 0;

    private int _beforeSelectIndex = 0;

    private float _nextoffsetAngle = 0;



    protected override void OnValidate()
    {
        base.OnValidate();
        Wheel();
        Arrange();
    }

    public void SetLayoutHorizontal() { }
    public void SetLayoutVertical()
    {
        Arrange();
    }

    void Arrange()
    {
        float splitAngle = 360 / transform.childCount;
        //var rect = transform as RectTransform;

        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i) as RectTransform;
            float currentAngle = splitAngle * i + offsetAngle;
            child.anchoredPosition = new Vector2(
                Mathf.Cos(currentAngle * Mathf.Deg2Rad),
                Mathf.Sin(currentAngle * Mathf.Deg2Rad)) * radius;
        }
    }



    public void WheelUp()
    {
        _nextoffsetAngle += _scrollValue;
        _beforeSelectIndex = _selectIndex;
        _selectIndex++;
        if (_selectIndex > 3) { _selectIndex = 0; }
        Wheel();
    }

    public void WheelDown()
    {
        _nextoffsetAngle -= _scrollValue;
        _beforeSelectIndex = _selectIndex;
        _selectIndex--;
        if (_selectIndex < 0) { _selectIndex = 3; }
        Wheel();
    }

    private void Wheel() 
    {
        DOTween.To(() => offsetAngle,
        x => offsetAngle = x, _nextoffsetAngle,
        _wheelChangeInterval).OnUpdate(() => Arrange());

        for (int i = 0; i < _tacticsTextArray.Length; i++)
        {
            int j = i;

            if (_selectIndex != j)
            {
                Color col = _tacticsTextArray[j].color;

                DOTween.To(() => col, x => col = x, Color.white, _colorChangeInterval)
                    .OnUpdate(() => _tacticsTextArray[j].color = col);
            }
            else
            {
                Color col = _tacticsTextArray[j].color;

                DOTween.To(() => col, x => col = x, Color.red, _colorChangeInterval)
                        .OnUpdate(() => _tacticsTextArray[j].color = col);
            }
        }

    } 

    public void TextSlotSet(TacticsList[] tacticsArray) 
    {
        for (int i = 0; i < tacticsArray.Length; i++) 
        {
            _tacticsTextArray[i].text = tacticsArray[i].tactics_name;
        }
    }

    public static TacticSlot instance;

    public static TacticSlot Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(TacticSlot);

                instance = (TacticSlot)FindObjectOfType(t);
                if (instance == null)
                {
                    Debug.LogWarning($"{t}をアタッチしているオブジェクトがありません");
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
