using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
public class BossAttackAi : MonoBehaviour
{
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject player;
    [SerializeField] private float knockbackForce; // Adicione essa vari�vel para controlar a for�a de knockbacks
    [SerializeField] private float knockbackCooldown;

    [SerializeField] private float attackAnim;

    [SerializeField] private Animator animator; // Adicionado para a anima��o de ataque

    const string attackTrigger = "IsAttacking";

    private bool canKnockback = true;

    private void Awake()
    {
        if (player == null)
        {
            Debug.Log("Preciso que o componente Player seja adicionado");
        }
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component is missing on the enemy.");
            }
        }
    }

    private void Update()
    {
        // A l�gica de detec��o de colis�o e ataque ser� tratada no m�todo OnCollisionEnter2D
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Entrou no colisor");

        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null && canKnockback)
            {
                StartCoroutine(ApplyKnockbackAndCooldown(collision.gameObject));
            }
        }
    }

    private IEnumerator ApplyKnockbackAndCooldown(GameObject player)
    {
        canKnockback = false; // Desativa o knockback

        // Toca a anima��o de ataque
        if (animator != null)
        {
            animator.SetBool(attackTrigger, true);
        }

        // Aguarda o tempo da anima��o (suponha 1 segundo para esta anima��o, ajuste conforme necess�rio)
        yield return new WaitForSeconds(attackAnim);

        // Aplica o dano
        IDamageable damageable = player.GetComponent<IDamageable>();
        damageable.TakeDamage(attackDamage);

        // Calcula a dire��o do knockback
        Vector2 knockbackDirection = (player.transform.position - transform.position).normalized;

        // Aplica a for�a de knockback ao jogador
        PlayerMove playerMove = player.GetComponent<PlayerMove>();
        if (playerMove != null)
        {
            Debug.Log("O Vetor zero foi aplicado");
            playerMove.ApplyKnockback(knockbackDirection, knockbackForce);
            Debug.Log($"{player.name} recebeu uma for�a de {knockbackForce} para a dire��o {knockbackDirection}");
        }

        // Redefine a anima��o de ataque
        if (animator != null)
        {
            Debug.Log("Anima��o � falsa");
            animator.SetBool(attackTrigger, false);
        }

        // Aguarda o tempo de cooldown
        yield return new WaitForSeconds(knockbackCooldown);

        // Habilita o knockback novamente
        canKnockback = true;
    }

    private void OnDrawGizmosSelected()
    {
        // Desenha uma esfera de gizmos para representar o raio de ataque
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
