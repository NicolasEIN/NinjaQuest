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
    [SerializeField] private float knockbackForce;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackDamage; // Adiciona a variável de dano

    // Cooldown variables
    private float lastAttackTime = 0f;
    [SerializeField] private float attackCooldown; // Tempo de cooldown entre ataques

    // InputReader reference
    [SerializeField] private InputReader inputReader;

    private PlayerMove playerMove;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerMove = GetComponent<PlayerMove>();
    }

    private void Start()
    {
        if (inputReader == null)
        {
            Debug.LogError("InputReader reference is missing in PlayerCombat script!");
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

    // Método para detectar colisão da espada com inimigos
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (swordPlaceHolder.activeSelf)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);

                // Calcula a direção do knockback
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;

                // Aplica a força de knockback
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = Vector2.zero; // Zera a velocidade atual para evitar interferência
                    rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                }
            }
        }

    }
 }
