using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SystemAction : MonoBehaviour
{
    PlayerInput _systemInput = null;

    PlayerInput _playerInput = null;

    private void Awake()
    {
        _systemInput = GetComponent<PlayerInput>();
        _playerInput = Player.Instance.GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        if (_systemInput == null) { return; }

        _systemInput.onActionTriggered += OnMenu;
    }

    private void OnDisable()
    {
        if (_systemInput == null) { return; }

        _systemInput.onActionTriggered -= OnMenu;
    }

    private void OnMenu(InputAction.CallbackContext context) 
    {
        if (context.action.name == "Menu" && context.performed) 
        {
            bool menuActive = UiManager.Instance.MenuOpenOrClose();
            //if (menuActive)
            //{
            //    _playerInput.SwitchCurrentActionMap("UI");
            //}
            //else { _playerInput.SwitchCurrentActionMap("Player"); }
        }
    }
}
