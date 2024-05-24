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
    [SerializeField] private float knockbackForce; // Adicione essa variável para controlar a força de knockbacks
    [SerializeField] private float knockbackCooldown;

    [SerializeField] private float attackAnim;

    [SerializeField] private Animator animator; // Adicionado para a animação de ataque

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
        // A lógica de detecção de colisão e ataque será tratada no método OnCollisionEnter2D
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

        // Toca a animação de ataque
        if (animator != null)
        {
            animator.SetBool(attackTrigger, true);
        }

        // Aguarda o tempo da animação (suponha 1 segundo para esta animação, ajuste conforme necessário)
        yield return new WaitForSeconds(attackAnim);

        // Aplica o dano
        IDamageable damageable = player.GetComponent<IDamageable>();
        damageable.TakeDamage(attackDamage);

        // Calcula a direção do knockback
        Vector2 knockbackDirection = (player.transform.position - transform.position).normalized;

        // Aplica a força de knockback ao jogador
        PlayerMove playerMove = player.GetComponent<PlayerMove>();
        if (playerMove != null)
        {
            Debug.Log("O Vetor zero foi aplicado");
            playerMove.ApplyKnockback(knockbackDirection, knockbackForce);
            Debug.Log($"{player.name} recebeu uma força de {knockbackForce} para a direção {knockbackDirection}");
        }

        // Redefine a animação de ataque
        if (animator != null)
        {
            Debug.Log("Animação é falsa");
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
