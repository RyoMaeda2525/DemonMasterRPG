using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    Player _player = null;

    /// <summary>作戦指示中の停止用</summary>
    private bool _actionBool = false;

    /// <summary> アイテムスロットを表示しているか判定する</summary>
    bool _itemSlotBool = false;

    void FixedUpdate()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetButtonDown("Fire3"))
        {
            CameraChange.Instance.LockOnOff();
        }
        else if (CameraChange.Instance._isLockOn)
        {
            if (Input.GetKeyDown(KeyCode.Q)) { CameraChange.Instance.LockOnChangeLeft(); }
            else if (Input.GetKeyDown(KeyCode.E)) { CameraChange.Instance.LockOnChangeRight(); }
        }

        if (Input.GetButton("Jump"))
        {
            SlotChange(true);
        }
        else
        {
            SlotChange(false);
        }

        if (!_itemSlotBool)
        {
            if (!_actionBool && Input.GetKeyDown(KeyCode.F))
            {
                ChangeTactics();
            }
            else if (scrollWheel > 0) { TacticSlot.Instance.WheelUp(); }
            else if (scrollWheel < 0) { TacticSlot.Instance.WheelDown(); }
        }
        else
        {
            if (!_actionBool && Input.GetKeyDown(KeyCode.F))
            {
                UseItems();
            }
            else if (scrollWheel > 0) { ItemSlot.Instance.WheelUp(); }
            else if (scrollWheel < 0) { ItemSlot.Instance.WheelDown(); }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            ItemInventoryManager.Instance.OpenOrCloseInventory();
        }
    }


    /// <summary>表示するスロットを切り替える</summary>
    private void SlotChange(bool itemSlotactive)
    {
        if (_itemSlotBool != itemSlotactive)
        {
            TacticSlot.Instance.TacticsSlotActiveChange();
            ItemSlot.Instance.ItemSlotActiveChange();
            _itemSlotBool = itemSlotactive;
        }
    }

    /// <summary>作戦を指示する</summary>
    void ChangeTactics()
    {
        int index = TacticSlot.Instance._selectIndex;

        StartCoroutine(ActionStop(3.0f));

        _player.ConductTactics(index);
    }

    /// <summary>手持ちのアイテムを使用する </summary>
    private void UseItems()
    {

        int index = ItemSlot.Instance._selectIndex;

        StartCoroutine(ActionStop(3.0f));

        _player.UseItems(index);
    }

    private IEnumerator ActionStop(float waitTime)
    {
        _actionBool = true;
        yield return new WaitForSeconds(waitTime);
        _actionBool = false;
    }
}
