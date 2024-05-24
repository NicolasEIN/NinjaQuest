using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCombat : MonoBehaviour
{
    // Animation variables
    private Animator animator;
    const string attackTrigger = "IsAttacking";

    // InputReader reference
    [SerializeField] private InputReader inputReader;

    private PlayerMove playerMove;

    // Referência para a arma
    [SerializeField] private GameObject weapon;

    // Variáveis de cooldown e tempo de ataque da espada
    [SerializeField] private float swordTime;
    [SerializeField] private float attackCooldown; // Tempo de cooldown entre ataques

    // Tempo do último ataque
    private float lastAttackTime = 0f;

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
        if (Time.time - lastAttackTime >= attackCooldown && !playerMove.isAttacking)
        {
            // Ativa a animação de ataque
            animator.SetBool(attackTrigger, true);

            // Ativa a arma
            weapon.SetActive(true);

            // Rotaciona a espada baseada na direção do jogador
            Vector3 facingDirection = playerMove.facingDirection;
            float angleZ = 0;
            float angleX = 0;

            if (facingDirection == Vector3.up)
            {
                angleX = 180;
                angleZ = 0;
            }
            else if (facingDirection == Vector3.down)
            {
                angleZ = 0;
                angleX = 0;
            }
            else if (facingDirection == Vector3.right)
            {
                angleZ = 90;
                angleX = 0;
            }
            else if (facingDirection == Vector3.left)
            {
                angleZ = -90;
                angleX = 180;
            }

            weapon.transform.rotation = Quaternion.Euler(angleX, 0, angleZ);

            // Inicia a rotina para desativar a arma após um tempo
            StartCoroutine(DeactivateWeaponAfterDelay());

            // Atualiza o tempo do último ataque
            lastAttackTime = Time.time;

            // Impede o movimento durante o ataque
            playerMove.isAttacking = true;
        }
    }

    private IEnumerator DeactivateWeaponAfterDelay()
    {
        // Aguarda o tempo da animação de ataque
        yield return new WaitForSeconds(swordTime);

        // Desativa a animação de ataque
        animator.SetBool(attackTrigger, false);

        // Desativa a arma
        weapon.SetActive(false);

        // Permite o movimento após o término do ataque
        playerMove.isAttacking = false;
    }
}
