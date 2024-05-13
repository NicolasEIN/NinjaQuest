using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerAttack : MonoBehaviour
{
    //Variáveis de animação
    Animator animator;
    const string attackTrigger = "Attack";
    private bool isPressed = false;
    bool isAttackPressed = false;

    //Variáveis de ataque
    [SerializeField] GameObject swordPlaceHolder;
    public InputActionReference attackAction;
    [SerializeField]float swordTime; 

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }


    private void OnEnable()
    {
        attackAction.action.started += OnAttackStarted;
        attackAction.action.performed += OnAttackPerformed;
        attackAction.action.canceled += OnAttackCanceled;
    }

    private void OnDisable()
    {
        attackAction.action.started -= OnAttackStarted;
        attackAction.action.performed -= OnAttackPerformed;
        attackAction.action.canceled -= OnAttackCanceled;
    }

   public void OnAttackStarted(InputAction.CallbackContext context)
    {
        if (!isPressed)
        {
            isPressed = true;
            InputManager.isAttacking = true;
            animator.SetTrigger(attackTrigger);
            ActivateSwordAttack();
            //Debug.Log("Attack");
        }
    }

   public void OnAttackPerformed(InputAction.CallbackContext context)
    {
        if (isPressed)
        {
            isPressed = false;
            InputManager.isAttacking = false;
            StartCoroutine(DeactivateSwordAfterDelay());

        }
    }

    public void OnAttackCanceled(InputAction.CallbackContext context)
    {
        if (isPressed)
        {
            isPressed = false;
            InputManager.isAttacking = false;
            StartCoroutine(DeactivateSwordAfterDelay());
        }
    }

    void ActivateSwordAttack()
    {
        if (isAttackPressed)
        {
            swordPlaceHolder.SetActive(true);

        }
    }
    void DeactivateSwordAttack()
    {
        if (!isAttackPressed)
        {
            swordPlaceHolder.SetActive(false);
        }
    }
    IEnumerator DeactivateSwordAfterDelay()
    {
        // Aguarda pelo tempo especificado
        yield return new WaitForSeconds(swordTime);
        // Desativa a espada após o tempo especificado
        DeactivateSwordAttack();
    }
}
