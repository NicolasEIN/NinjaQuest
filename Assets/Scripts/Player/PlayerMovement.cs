using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    //variáveis de movimento
    [SerializeField] private float speed;
    private Vector2 move;

    //variáveis de componente.
    private Rigidbody2D rigidBody2d;
    private Animator animator;

    //Variáveis para suavizar o movimento
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;


    //variáveis de animação
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";


    private void Awake()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        OnMove();
    }

    public void OnMove()
    {
        move.Set(InputManager.movement.x, InputManager.movement.y);

        rigidBody2d.velocity = move * speed;

        animator.SetFloat(horizontal, move.x);
        animator.SetFloat(vertical, move.y);    

        if(move != Vector2.zero)
        {
            animator.SetFloat(lastHorizontal, move.x);
            animator.SetFloat(lastVertical, move.y);
        }

        _smoothedMovementInput = Vector2.SmoothDamp(_smoothedMovementInput, move,
         ref _movementInputSmoothVelocity, 0.1f);
    }
}
