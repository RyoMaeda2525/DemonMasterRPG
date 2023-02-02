using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    Animator _animator;

    ActionState _state;

    public ActionState Actionstate => _state;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetFloat(string name , float value) 
    {
        _animator.SetFloat(name, value);
    }

    public void Action(string name , int layerIndex) 
    {
        _animator.Play(name);

        _state = ActionState.Running;

        StartCoroutine(MoveStop(layerIndex));
    }

    public void DethAnimation()
    { 
        _state = ActionState.Running;
        _animator.Play("Deth");
    }

    private IEnumerator MoveStop(int layerIndex) 
    {
        yield return null;

        var state = _animator.GetCurrentAnimatorStateInfo(layerIndex);
        yield return new WaitForSeconds(state.length);

        _state = ActionState.Wait;
    }
}
public enum ActionState
{
    Wait,
    Running
}
