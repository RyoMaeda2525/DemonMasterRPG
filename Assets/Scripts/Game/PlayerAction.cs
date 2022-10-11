using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    Player _player = null;

    /// <summary>作戦指示中の停止用</summary>
    private bool _actionBool = false;

    void FixedUpdate()
    {

        float h = Input.GetAxis("HorizontalKey");              // 矢印キーの水平軸をhで定義
        float v = Input.GetAxis("VerticalKey");                // 矢印キーの垂直軸をvで定義
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        
        if (scrollWheel > 0) { TacticSlot.Instance.WheelUp(); }
        else if (scrollWheel < 0) { TacticSlot.Instance.WheelDown(); }


        if (Input.GetButton("Jump"))
        {
            if (!_actionBool && h != 0 || !_actionBool && v != 0)
            {
                UseItems(h, v);
            }
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeTactics();
        }
    }

    /// <summary>
    /// 作戦を指示する
    /// </summary>
    /// <param name="h">横</param>
    /// <param name="v">縦</param>
    void ChangeTactics()
    {
        int index = TacticSlot.Instance._selectIndex;

        StartCoroutine(ActionStop(3.0f));

        _player.ConductTactics(index);
    }

    private void UseItems(float h, float v)
    {

        int i = -1;
        if (h > 0) { i = 0; } //右
        else if (v > 0) { i = 1; }//下
        else if (h < 0) { i = 2; }//左
        else if (v < 0) { i = 3; }//上

        //入力がないとき
        if (i == -1) { Debug.Log("Error01"); return; }

        //手持ちのモンスターがいないとき
        if (_player._pms.Count == 0) { Debug.Log("Error02"); return; }

        StartCoroutine(ActionStop(3.0f));

        _player.UseItems(i);
    }

    private IEnumerator ActionStop(float waitTime)
    {
        _actionBool = true;
        yield return new WaitForSeconds(waitTime);
        _actionBool = false;
    }
}
