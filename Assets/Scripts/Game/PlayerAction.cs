using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    Player _player = null;

    /// <summary>���w�����̒�~�p</summary>
    private bool _actionBool = false;

    void FixedUpdate()
    {

        float h = Input.GetAxis("HorizontalKey");              // ���L�[�̐�������h�Œ�`
        float v = Input.GetAxis("VerticalKey");                // ���L�[�̐�������v�Œ�`
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
    /// �����w������
    /// </summary>
    /// <param name="h">��</param>
    /// <param name="v">�c</param>
    void ChangeTactics()
    {
        int index = TacticSlot.Instance._selectIndex;

        StartCoroutine(ActionStop(3.0f));

        _player.ConductTactics(index);
    }

    private void UseItems(float h, float v)
    {

        int i = -1;
        if (h > 0) { i = 0; } //�E
        else if (v > 0) { i = 1; }//��
        else if (h < 0) { i = 2; }//��
        else if (v < 0) { i = 3; }//��

        //���͂��Ȃ��Ƃ�
        if (i == -1) { Debug.Log("Error01"); return; }

        //�莝���̃����X�^�[�����Ȃ��Ƃ�
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
