using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    Animator _animator;

    ActionState _state;

    /// <summary>�A�j���[�V�������Đ����Ă��邩 </summary>
    public ActionState Actionstate => _state;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    //Animator��SetFlort�ɂ����
    public void SetFloat(string name , float value) 
    {
        _animator.SetFloat(name, value);
    }

    /// <summary>
    /// �����ɍ������A�j���[�V�������Đ�
    /// </summary>
    /// <param name="name"></param>
    /// <param name="layerIndex"></param>
    public void Action(string name , int layerIndex) 
    {
        _animator.Play(name);

        _state = ActionState.Running;

        StartCoroutine(MoveStop(layerIndex));
    }

    /// <summary>�����X�^�[�̎��S�A�j���[�V�������Đ�</summary>
    public void DethAnimation()
    { 
        _state = ActionState.Running;
        _animator.Play("Deth");
    }

    /// <summary>�A�j���[�V�����̒�~</summary>
    public void AnimationStop() 
    {
        _animator.speed = 0f;
    }

    /// <summary>�A�j���[�V�����̍ĊJ</summary>
    public void AnimationResume() 
    {
        _animator.speed = 1f;
    }

    /// <summary>�ړ��ȊO�̃A�j���[�V�����̓����X�^�[�̓������~�߂�</summary>
    private IEnumerator MoveStop(int layerIndex) 
    {
        yield return null;
        //�A�j���[�V�����̎��Ԃ��擾
        var state = _animator.GetCurrentAnimatorStateInfo(layerIndex);
        yield return new WaitForSeconds(state.length);

        _state = ActionState.Wait;
    }
}
/// <summary>�A�j���[�V�������Đ����Ă��邩 </summary>
public enum ActionState
{
    Wait,
    Running
}
