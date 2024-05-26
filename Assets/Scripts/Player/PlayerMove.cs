using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    // Movement variables
    [SerializeField] private float speed;
    private Vector2 move;
    public bool isAttacking { get; set; }
    public bool isInteracting { get; set; }

    // Knockback variables
    private bool isKnockedBack;
    private float knockbackDuration = 0.2f; // Duration for which the knockback effect is applied

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

    // Direction the player is facing
    public Vector2 facingDirection { get; private set; } = Vector2.down;

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

    private void Update()
    {

        if (isAttacking)
        {
            // Stop movement
            move = Vector2.zero;
            rigidBody2d.velocity = Vector2.zero;

            animator.SetFloat(horizontal, 0);
            animator.SetFloat(vertical, 0);
        }
    }

    private void FixedUpdate() // Use FixedUpdate for physics updates
    {
        if (!isAttacking && !isKnockedBack)
        {
            rigidBody2d.velocity = move * speed;
        }
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
        if (isKnockedBack) return; // Ignore movement input during knockback

        move = movement;
        rigidBody2d.velocity = move * speed;

        animator.SetFloat(horizontal, move.x);
        animator.SetFloat(vertical, move.y);

        if (move != Vector2.zero)
        {
            animator.SetFloat(lastHorizontal, move.x);
            animator.SetFloat(lastVertical, move.y);

            facingDirection = move.normalized;
        }

        // You can uncomment this section if you want to smooth movement
        _smoothedMovementInput = Vector2.SmoothDamp(_smoothedMovementInput, move,
           ref _movementInputSmoothVelocity, 0.1f);

        Vector3 direction = new Vector3(_smoothedMovementInput.x, _smoothedMovementInput.y, 0f);
    }

    public void ApplyKnockback(Vector2 direction, float force)
    {
        if (!isKnockedBack)
        {
            isKnockedBack = true;
            rigidBody2d.velocity = Vector2.zero;
            rigidBody2d.AddForce(direction * force, ForceMode2D.Impulse);
            StartCoroutine(KnockbackRecovery());
        }
    }

    private IEnumerator KnockbackRecovery()
    {
        yield return new WaitForSeconds(knockbackDuration);
        isKnockedBack = false;
    }

}

