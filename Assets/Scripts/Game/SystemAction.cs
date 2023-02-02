using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SystemAction : MonoBehaviour
{
    PlayerInput _input = null;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        if (_input == null) { return; }

        _input.onActionTriggered += OnMenu;
    }

    private void OnDisable()
    {
        if (_input == null) { return; }

        _input.onActionTriggered -= OnMenu;
    }

    private void OnMenu(InputAction.CallbackContext context) 
    {
        if (context.action.name == "Menu" && context.performed) 
        {
            UiManager.Instance.MenuOpenOrClose();
        }
    }
}
