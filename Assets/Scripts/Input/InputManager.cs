using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Vector2 moveInput;
    // public event UnityAction<Vector2> Move = delegate { };

    // PlayerInputActions inputActions;
    // public Vector3 Direction => inputActions.Player.Move.ReadValue<Vector2>();

    // void OnEnable()
    // {
    //     if (inputActions == null)
    //     {
    //         inputActions = new PlayerInputActions();
    //         inputActions.Player.SetCallbacks(this);
    //     }
    // }

    // public void EnablePlayerActions()
    // {
    //     inputActions.Enable();
    // }
    //
    // public void OnMove(InputAction.CallbackContext context)
    // {
    //     Move.Invoke(context.ReadValue<Vector2>());
    // }

    public void OnMove(InputValue input) => moveInput = input.Get<Vector2>();
}