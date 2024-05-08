using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputManager : MonoBehaviour
{
    public static Vector2 movement;
    public static bool isAttacking;

    //Código Antigo 
    //private PlayerInput playerInput;
    //private InputAction moveAction;

    //private InputAction attackAction;

    private void Awake()
    {
        //playerInput = GetComponent<PlayerInput>();
        //moveAction = playerInput.actions["Move"];
        //attackAction = playerInput.actions["Attack"];

    }

    private void Update()
    {
        //movement = moveAction.ReadValue<Vector2>();

        //isAttacking = attackAction.triggered;

    }

    public void OnMove(InputAction.CallbackContext moveAction)
    {
        movement = moveAction.ReadValue<Vector2>();
    }


}
