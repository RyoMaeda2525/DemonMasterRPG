using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    Animator _animator;

    ActionState _state;

    /// <summary>アニメーションが再生しているか </summary>
    public ActionState Actionstate => _state;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    //AnimatorのSetFlortにいれる
    public void SetFloat(string name , float value) 
    {
        _animator.SetFloat(name, value);
    }

    /// <summary>
    /// 引数に合ったアニメーションを再生
    /// </summary>
    /// <param name="name"></param>
    /// <param name="layerIndex"></param>
    public void Action(string name , int layerIndex) 
    {
        _animator.Play(name);

        _state = ActionState.Running;

        StartCoroutine(MoveStop(layerIndex));
    }

    /// <summary>モンスターの死亡アニメーションを再生</summary>
    public void DethAnimation()
    { 
        _state = ActionState.Running;
        _animator.Play("Deth");
    }

    /// <summary>アニメーションの停止</summary>
    public void AnimationStop() 
    {
        _animator.speed = 0f;
    }

    /// <summary>アニメーションの再開</summary>
    public void AnimationResume() 
    {
        _animator.speed = 1f;
    }

    /// <summary>移動以外のアニメーションはモンスターの動きを止める</summary>
    private IEnumerator MoveStop(int layerIndex) 
    {
        yield return null;
        //アニメーションの時間を取得
        var state = _animator.GetCurrentAnimatorStateInfo(layerIndex);
        yield return new WaitForSeconds(state.length);

        _state = ActionState.Wait;
    }
}
/// <summary>アニメーションが再生しているか </summary>
public enum ActionState
{
    Wait,
    Running
}
