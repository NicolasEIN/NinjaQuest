using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCombat : MonoBehaviour
{
    // Animation variables
    private Animator animator;
    const string attackTrigger = "IsAttacking";


    // Attack variables
    [SerializeField] private GameObject swordPlaceHolder;
    [SerializeField] private float swordTime;
    [SerializeField] private float attackRange;

    // Cooldown variables
    private float lastAttackTime = 0f;
    [SerializeField] private float attackCooldown; // Tempo de cooldown entre ataques

    // InputReader reference
    [SerializeField] private InputReader inputReader;

    private PlayerMove playerMove;

    //// Knockback variables
    //[SerializeField] private float knockbackForce = 10f;
    //[SerializeField] private float knockbackDuration = 0.2f;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerMove = GetComponent<PlayerMove>();
    }

    private void Start()
    {

        if (inputReader == null)
        {
            Debug.LogError("InputReader reference is missing in PlayerAttack script!");
            return;
        }
    }

    private void OnEnable()
    {
        inputReader.AttackEvent += OnAttack;
    }

    private void OnDisable()
    {
        inputReader.AttackEvent -= OnAttack;
    }

    private void OnAttack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time; // Atualiza o tempo do último ataque
            animator.SetBool(attackTrigger, true);
            ActivateSwordAttack();
            StartCoroutine(DeactivateSwordAfterDelay());
            playerMove.isAttacking = true;
            Debug.Log("Attack");
        }
    }

    void ActivateSwordAttack()
    {

        // Rotaciona a espada baseada na direção do jogador
        Vector2 facingDirection = playerMove.facingDirection;
        float angleZ = 0;
        float angleX = 0;

        if (facingDirection == Vector2.up)
        {
            angleX = 180;
            angleZ = 0;
        }
        else if (facingDirection == Vector2.down)
        {
            angleZ = 0;
            angleX = 0;
        }
        else if (facingDirection == Vector2.right)
        {
            angleZ = 90;
            angleX = 0; 
        }
        else if (facingDirection == Vector2.left)
        {
            angleZ = -90;
            angleX = 180;
        }

        swordPlaceHolder.transform.rotation = Quaternion.Euler(angleX, 0, angleZ);
        swordPlaceHolder.SetActive(true);


    }

    void DeactivateSwordAttack()
    {
        swordPlaceHolder.SetActive(false);
        Debug.Log("O Ataque foi concluído");
    }

    IEnumerator DeactivateSwordAfterDelay()
    {
        yield return new WaitForSeconds(swordTime);
        DeactivateSwordAttack();
        animator.SetBool(attackTrigger, false);
        playerMove.isAttacking = false;
    }

}
