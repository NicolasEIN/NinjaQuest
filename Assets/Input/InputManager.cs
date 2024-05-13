using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputManager : MonoBehaviour
{
    public static Vector2 movement;
    public static bool isAttacking; 

    public void OnMove(InputAction.CallbackContext moveAction)
    {
        movement = moveAction.ReadValue<Vector2>();
    }


}
