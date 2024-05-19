using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Movement variables
    [SerializeField] private float speed;
    private Vector2 move;

    // Component variables
    private Rigidbody2D rigidBody2d;
    private Animator animator;

    // InputReader reference
    [SerializeField] private InputReader inputReader; // Add serialized field for InputReader

    // Variables for smoothing movement (optional)
     private Vector2 _smoothedMovementInput;
     private Vector2 _movementInputSmoothVelocity;

    // Animation variables
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";

    private void Awake()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {

        if (inputReader == null)
        {
            Debug.LogError("InputReader reference is missing in PlayerMove script!");
            return;
        }

        inputReader.MoveEvent += OnMove;
    }

    private void OnDestroy()
    {
        if (inputReader != null)
        {
            inputReader.MoveEvent -= OnMove;
        }
    }

    public void OnMove(Vector2 movement)
    {
        move = movement;
        rigidBody2d.velocity = move * speed;

        animator.SetFloat(horizontal, move.x);
        animator.SetFloat(vertical, move.y);

        if (move != Vector2.zero)
        {
            animator.SetFloat(lastHorizontal, move.x);
            animator.SetFloat(lastVertical, move.y);
        }

        // You can uncomment this section if you want to smooth movement
         _smoothedMovementInput = Vector2.SmoothDamp(_smoothedMovementInput, move,
            ref _movementInputSmoothVelocity, 0.1f);
    }
}