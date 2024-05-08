using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private bool attackCheck; 
    [SerializeField] float animationTimeControl;
    private const string attack = "Attack";
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (InputManager.isAttacking && !attackCheck)
        {
            StartCoroutine(AttackCourotine()); ;
        }
    }


    private IEnumerator AttackCourotine()
    {
        attackCheck = true;
        PerformAttack();
        yield return new WaitForSeconds(animationTimeControl);
        attackCheck = false;
    }

    void PerformAttack()
    {
        animator.SetTrigger(attack);
        Debug.Log("Ataque");
    }
}
