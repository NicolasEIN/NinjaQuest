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

    // InputReader reference
    [SerializeField]private InputReader inputReader;

    private void Awake()
    {
         animator = GetComponentInChildren<Animator>();
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
        animator.SetBool(attackTrigger, true);
        ActivateSwordAttack();
        StartCoroutine(DeactivateSwordAfterDelay());
        Debug.Log("Attack");
    }

    void ActivateSwordAttack()
    {
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
    }
}
