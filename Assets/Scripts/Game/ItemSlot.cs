using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ItemSlot : UIBehaviour, ILayoutGroup
{
    [SerializeField, Tooltip("リングスロットの半径")]
    float radius = 100;

    [SerializeField, Tooltip("リングスロットを回す量")]
    private float offsetAngle;

    [SerializeField, Tooltip("アイテムスロットのText")]
    private List<Text> _itemTextArray;

    [SerializeField, Tooltip("スクロールしたときの変化量")]
    private float _scrollValue = 90;

    [SerializeField, Tooltip("回転が終わるまでの時間")]
    private float _wheelChangeInterval = 1.5f;

    [SerializeField, Tooltip("色変更が終わるまでの時間")]
    private float _colorChangeInterval = 1.5f;

    /// <summary>activeになっているアイテムスロットの数</summary>
    public int _activeChildren = 0;

    private float _nextoffsetAngle = 0;

    public int _selectIndex = 0;

    protected override void OnValidate()
    {
        base.OnValidate();
    }

    public void SetLayoutHorizontal() { }
    public void SetLayoutVertical()
    {
        Arrange();
    }

    void Arrange()
    {
        for (int i = 0; i < _activeChildren; i++)
        {
            var child = transform.GetChild(i) as RectTransform;
            float currentAngle = _scrollValue * i + offsetAngle;
            child.anchoredPosition = new Vector2(
                Mathf.Cos(currentAngle * Mathf.Deg2Rad),
                Mathf.Sin(currentAngle * Mathf.Deg2Rad)) * radius;
        }
    }

    public void WheelUp()
    {
        if (_activeChildren > 0) 
        {
            _nextoffsetAngle += _scrollValue;
            _selectIndex++;
            if (_selectIndex > _activeChildren - 1) { _selectIndex = 0; }
            Wheel();
        }
    }

    public void WheelDown()
    {
        if (_activeChildren > 0)
        {
            _nextoffsetAngle -= _scrollValue;
            _selectIndex--;
            if (_selectIndex < 0) { _selectIndex = _activeChildren - 1; }
            Wheel();
        }
    }

    private void Wheel()
    {
        DOTween.To(() => offsetAngle,
        x => offsetAngle = x, _nextoffsetAngle,
        _wheelChangeInterval).OnUpdate(() => Arrange());

        for (int i = 0; i < _activeChildren; i++)
        {
            int j = i;

            if (_selectIndex != j)
            {
                Color col = _itemTextArray[j].color;

                DOTween.To(() => col, x => col = x, Color.white, _colorChangeInterval)
                    .OnUpdate(() => _itemTextArray[j].color = col);
            }
            else
            {
                Color col = _itemTextArray[j].color;

                DOTween.To(() => col, x => col = x, Color.red, _colorChangeInterval)
                        .OnUpdate(() => _itemTextArray[j].color = col);
            }
        }

    }

    public void ItemSlotSet(List<Item> itemArray)
    {
        int activeChildren = 0;

        for (int i = 0; i < _itemTextArray.Count; i++)
        {
            if (i < itemArray.Count)
            {
                _itemTextArray[i].text = itemArray[i].name;
                activeChildren++;
            }
            else { _itemTextArray[i].text = null; }
        }

        _activeChildren = activeChildren;

        if (_activeChildren > 0)
        {
            _scrollValue = 360 / activeChildren;
            Arrange();
        }
    }
    public void ItemSlotActiveChange()
    {
        if (_itemTextArray[0].text != null && !_itemTextArray[0].gameObject.activeSelf)
        {
            foreach (var item in _itemTextArray)
            {
                if (item.text != null)
                {
                    item.gameObject.SetActive(true);
                }
            }
        }
        else if (_itemTextArray[0].gameObject.activeSelf)
        {
            foreach (var item in _itemTextArray)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}