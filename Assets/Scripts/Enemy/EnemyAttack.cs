using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float knockbackForce = 5f; // Adicione essa vari�vel para controlar a for�a de knockback

    private void Update()
    {
        // A l�gica de detec��o de colis�o e ataque ser� tratada no m�todo OnCollisionEnter2D
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);

                // Calcula a dire��o do knockback
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;

                // Aplica a for�a de knockback ao jogador
                Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerRigidbody != null)
                {
                    playerRigidbody.velocity = Vector2.zero; // Zera a velocidade atual para evitar interfer�ncia
                    playerRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                }
                else
                {
                    Debug.Log("Rigibody nulo");
                }
            }
            else
            {
                Debug.Log("Player Nulo");
            }
        }
        else
        {
            Debug.Log("Tag Na� encontrada");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

//SCRIPT ANTIGO FUNCIONAL 


    //[SerializeField] private float attackDamage;
    //[SerializeField] private float attackRange;
    //[SerializeField] private LayerMask playerLayer;
    //[SerializeField] private float knockbackForce; // Adicione essa vari�vel para controlar a for�a de knockback

    //private void Update()
    //{
    //    if (CanAttackPlayer())
    //    {
    //        Attack();
    //    }
    //}

    //private bool CanAttackPlayer()
    //{
    //    Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

    //    return hitPlayers.Length > 0;
    //}

    //private void Attack()
    //{
    //    Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

    //    foreach (Collider2D playerCollider in hitPlayers)
    //    {
    //        IDamageable damageable = playerCollider.GetComponent<IDamageable>();
    //        if (damageable != null)
    //        {
    //            damageable.TakeDamage(attackDamage);

    //            // Calcula a dire��o do knockback
    //            Vector2 knockbackDirection = (playerCollider.transform.position - transform.position).normalized;

    //            // Aplica a for�a de knockback ao jogador
    //            Rigidbody2D playerRigidbody = playerCollider.GetComponent<Rigidbody2D>();
    //            if (playerRigidbody != null)
    //            {
    //                playerRigidbody.velocity = Vector2.zero; // Zera a velocidade atual para evitar interfer�ncia
    //                playerRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    //            }
    //        }
    //    }
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //}

