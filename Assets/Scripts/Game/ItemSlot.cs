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
    [SerializeField, Tooltip("�����O�X���b�g�̔��a")]
    float radius = 100;

    [SerializeField, Tooltip("�����O�X���b�g���񂷗�")]
    public float offsetAngle;

    [SerializeField, Tooltip("�A�C�e���X���b�g��Text")]
    private List<Text> _itemTextArray;

    [SerializeField, Tooltip("�X�N���[�������Ƃ��̕ω���")]
    private float _scrollValue = 90;

    [SerializeField, Tooltip("��]���I���܂ł̎���")]
    private float _wheelChangeInterval = 1.5f;

    [SerializeField, Tooltip("�F�ύX���I���܂ł̎���")]
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

        for (int i = 0; i < _itemTextArray.Count; i++)
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
        int[] _itemCount = new int[4];

        for (int i = 0; i < itemArray.Count; i++)
        {
            if (_itemTextArray[i] != null)
            {
                _itemTextArray[i].text = itemArray[i].name;
            }
            else { _itemTextArray[i].text = null; }

            _itemTextArray[i].gameObject.SetActive(false);
        }
    }

    public static ItemSlot instance;

    public static ItemSlot Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(ItemSlot);

                instance = (ItemSlot)FindObjectOfType(t);
                if (instance == null)
                {
                    Debug.LogWarning($"{t}���A�^�b�`���Ă���I�u�W�F�N�g������܂���");
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