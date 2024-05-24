using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float attackDamage; // Dano do ataque
    [SerializeField] private float knockbackForce; // Força do knockback
    [SerializeField] private float knockbackDuration; // Duração do knockback
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);

                // Calcula a direção do knockback
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;

                // Aplica o knockback
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    StartCoroutine(ApplyKnockback(rb, knockbackDirection));
                }
            }
        }
    }

    private IEnumerator ApplyKnockback(Rigidbody2D rb, Vector2 knockbackDirection)
    {
        // Aplica o knockback
       rb.isKinematic = false;
        rb.velocity = Vector2.zero; // Zera a velocidade atual para evitar interferência
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

        // Aguarda a duração do knockback
        yield return new WaitForSeconds(knockbackDuration);
       rb.isKinematic = true;

        // Ao final da duração, define a velocidade do rigidbody como zero para interromper o knockback
        rb.velocity = Vector2.zero;
    }

}
