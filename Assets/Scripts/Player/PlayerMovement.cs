using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;

    private Vector2 move;

    private Rigidbody2D rigidBody2d;

    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;


    private void Awake()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        OnMove();
    }

    void OnMove()
    {
        move.Set(InputManager.movement.x, InputManager.movement.y);

        rigidBody2d.velocity = move * speed;

        _smoothedMovementInput = Vector2.SmoothDamp(_smoothedMovementInput, move,
         ref _movementInputSmoothVelocity, 0.1f);
    }
}
