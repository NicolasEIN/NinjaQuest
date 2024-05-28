using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float knockbackForce;
    [SerializeField] private float knockbackCooldown;

    private bool canKnockback = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
        canKnockback = false;

        // Aplica o dano
        IDamageable damageable = player.GetComponent<IDamageable>();
        damageable.TakeDamage(attackDamage);

        // Calcula a dire��o do knockback
        Vector2 knockbackDirection = (player.transform.position - transform.position).normalized;

        // Aplica a for�a de knockback ao jogador
        PlayerMove playerMove = player.GetComponent<PlayerMove>();
        if (playerMove != null)
        {
            playerMove.ApplyKnockback(knockbackDirection, knockbackForce);
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

    //[SerializeField] private float attackDamage;
    //[SerializeField] private float attackRange;
    //[SerializeField] private LayerMask playerLayer;
    //[SerializeField] private GameObject player;
    //[SerializeField] private float knockbackForce; // Adicione essa vari�vel para controlar a for�a de knockbacks
    //[SerializeField] private float knockbackCooldown;


    //private bool canKnockback = true;


    //private void Awake()
    //{
    //    if (player == null)
    //    {
    //        Debug.Log("Preciso que o componente Player seja adicionado");
    //    }
    //}

    //private void Update()
    //{
    //    // A l�gica de detec��o de colis�o e ataque ser� tratada no m�todo OnCollisionEnter2D
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("Entrou no colisor");

    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
    //        if (damageable != null && canKnockback)
    //        {
    //            StartCoroutine(ApplyKnockbackAndCooldown(collision.gameObject));
    //        }
    //    }
    //}

    //private IEnumerator ApplyKnockbackAndCooldown(GameObject player)
    //{
    //    canKnockback = false; // Desativa o knockback

    //    // Aplica o dano
    //    IDamageable damageable = player.GetComponent<IDamageable>();
    //    damageable.TakeDamage(attackDamage);

    //    // Calcula a dire��o do knockback
    //    Vector2 knockbackDirection = (player.transform.position - transform.position).normalized;

    //    // Aplica a for�a de knockback ao jogador
    //    PlayerMove playerMove = player.GetComponent<PlayerMove>();
    //    if (playerMove != null)
    //    {
    //        Debug.Log("O Vetor zero foi aplicado");
    //        playerMove.ApplyKnockback(knockbackDirection, knockbackForce);
    //        Debug.Log($"{player.name} recebeu uma for�a de {knockbackForce} para a dire��o {knockbackDirection}");
    //    }

    //    // Aguarda o tempo de cooldown
    //    yield return new WaitForSeconds(knockbackCooldown);

    //    // Habilita o knockback novamente
    //    canKnockback = true;
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    // Desenha uma esfera de gizmos para representar o raio de ataque
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //}
}