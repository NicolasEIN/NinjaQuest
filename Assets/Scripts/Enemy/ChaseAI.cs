using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class ChaseAI : MonoBehaviour
{

    public float chaseDistance;
    public float giveUpDistance;
    public LayerMask obstacleLayer;
    public Transform returnPoint;


    private NavMeshAgent agent;
    private GameObject player;
    private Animator animator;
    private bool isChasing = false;
    private Vector2 move;
    private Vector2 facingDirection;

    // Animator parameters
    private readonly int isWalking = Animator.StringToHash("IsWalking");
    private readonly int horizontal = Animator.StringToHash("Horizontal");
    private readonly int vertical = Animator.StringToHash("Vertical");
    private readonly int lastHorizontal = Animator.StringToHash("LastHorizontal");
    private readonly int lastVertical = Animator.StringToHash("LastVertical");

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        bool canSeePlayer = !Physics.Linecast(transform.position, player.transform.position, obstacleLayer);

        if (distanceToPlayer <= chaseDistance && canSeePlayer)
        {
            // Se estiver dentro da distância de perseguição, começa a perseguição
            agent.SetDestination(player.transform.position);
            isChasing = true;
        }
        else if (isChasing && (distanceToPlayer > giveUpDistance || !canSeePlayer))
        {
            // Se estava perseguindo mas saiu do alcance, para e retorna ao ponto de origem
            isChasing = false;
            agent.SetDestination(returnPoint.position);
        }

        // Atualiza a animação e o som de movimento
        UpdateAnimationAndSound();
    }

    private void UpdateAnimationAndSound()
    {
        move = new Vector2(agent.velocity.x, agent.velocity.z); // Considerando o eixo Y para movimento horizontal e Z para vertical no NavMesh
        bool isMoving = move != Vector2.zero;
        animator.SetBool(isWalking, isMoving);

        if (isMoving)
        {
            facingDirection = move.normalized;
            animator.SetFloat(horizontal, move.x);
            animator.SetFloat(vertical, move.y);
            animator.SetFloat(lastHorizontal, move.x);
            animator.SetFloat(lastVertical, move.y);
        }

    }

    private void OnDrawGizmosSelected()
    {
        // Desenha uma esfera de gizmos para representar o raio de ataque
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }

    //public float chaseDistance;
    //public float giveUpDistance;
    //public LayerMask obstacleLayer;
    //public Transform returnPoint;

    //private NavMeshAgent agent;
    //private GameObject player;
    //private Animator animator;
    //private bool isChasing = false;
    //private Vector2 move;
    //private Vector2 facingDirection;

    //// Animator parameters
    //private readonly int isWalking = Animator.StringToHash("IsWalking");
    //private readonly int horizontal = Animator.StringToHash("Horizontal");
    //private readonly int vertical = Animator.StringToHash("Vertical");
    //private readonly int lastHorizontal = Animator.StringToHash("LastHorizontal");
    //private readonly int lastVertical = Animator.StringToHash("LastVertical");

    //void Start()
    //{
    //    agent = GetComponent<NavMeshAgent>();
    //    agent.updateRotation = false;
    //    agent.updateUpAxis = false;
    //    player = GameObject.FindGameObjectWithTag("Player");
    //    animator = GetComponent<Animator>();
    //}

    //void Update()
    //{
    //    float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
    //    bool canSeePlayer = !Physics.Linecast(transform.position, player.transform.position, obstacleLayer);

    //    if (distanceToPlayer <= chaseDistance && canSeePlayer)
    //    {
    //        // Se estiver dentro da distância de perseguição, começa a perseguição
    //        agent.SetDestination(player.transform.position);
    //        isChasing = true;
    //    }
    //    else if (isChasing && (distanceToPlayer > giveUpDistance || !canSeePlayer))
    //    {
    //        // Se estava perseguindo mas saiu do alcance, para e retorna ao ponto de origem
    //        isChasing = false;
    //        agent.SetDestination(returnPoint.position);
    //    }

    //    // Atualiza a animação
    //    move = new Vector2(agent.velocity.x, agent.velocity.z); // Considerando o eixo Y para movimento horizontal e Z para vertical no NavMesh
    //    bool isMoving = move != Vector2.zero;
    //    animator.SetBool(isWalking, isMoving);

    //    if (isMoving)
    //    {
    //        facingDirection = move.normalized;
    //        animator.SetFloat(horizontal, move.x);
    //        animator.SetFloat(vertical, move.y);
    //        animator.SetFloat(lastHorizontal, move.x);
    //        animator.SetFloat(lastVertical, move.y);
    //    }
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    // Desenha uma esfera de gizmos para representar o raio de ataque
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, chaseDistance);
    //}

}