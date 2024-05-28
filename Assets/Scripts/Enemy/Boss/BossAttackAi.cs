using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
public class BossAttackAi : MonoBehaviour
{
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private float knockbackForce;
    [SerializeField] private float knockbackCooldown;
    [SerializeField] private Animator animator;

    private bool canKnockback = true;
    private const string attackTrigger = "IsAttacking";

    private void Awake()
    {

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component is missing on the enemy.");
            }
        }
       
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (canKnockback)
            {
                StartCoroutine(AttackCoroutine(collision.gameObject));
            }
        }
    }

    private IEnumerator AttackCoroutine(GameObject player)
    {
        canKnockback = false;

        // Inicia a animação de ataque
        animator.SetBool(attackTrigger, true);


        // Aguarda até o final da animação
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Aplica o dano
        IDamageable damageable = player.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(attackDamage);
        }

        // Calcula a direção do knockback
        Vector2 knockbackDirection = (player.transform.position - transform.position).normalized;

        // Aplica o knockback ao jogador
        PlayerMove playerMove = player.GetComponent<PlayerMove>();
        if (playerMove != null)
        {
            playerMove.ApplyKnockback(knockbackDirection, knockbackForce);
        }

        // Aguarda o tempo de cooldown
        yield return new WaitForSeconds(knockbackCooldown);

        // Reinicia a animação de ataque
        animator.SetBool(attackTrigger, false);

        canKnockback = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
