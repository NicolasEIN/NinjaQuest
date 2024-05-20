using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class ChaseAI : MonoBehaviour
{
    // Ponto para o qual o personagem ir� se mover
    private GameObject Player;
    // Vari�vel NavMeshAgent para configurar a movimenta��o do personagem
    private NavMeshAgent agent;
    // Ponto de retorno caso o jogador esteja fora do alcance
    public Transform returnPoint;
    // Dist�ncia m�xima permitida para iniciar a persegui��o
    public float chaseDistance;
    // Dist�ncia m�nima para continuar a persegui��o
    public float giveUpDistance;
    // LayerMask para detectar obst�culos
    public LayerMask obstacleLayer;

    // Componentes de anima��o
    private Animator animator;
    private Vector2 move;
    private Vector2 facingDirection = Vector2.down;

    // Anima��o variables
    private const string isWalking = "IsWalking";
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";

    void Start()
    {
        // Pega o Componente NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        // Vari�veis setadas como False para n�o utilizar os eixos Y baseado em 3 dimens�es
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        // Encontra o ponto na cena
        Player = GameObject.FindGameObjectWithTag("Player");
        // Pega o componente Animator
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Verifica a dist�ncia entre o inimigo e o jogador
        float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);

        // Verifica se h� linha de vis�o direta para o jogador
        bool canSeePlayer = !Physics.Linecast(transform.position, Player.transform.position, obstacleLayer);

        if (distanceToPlayer <= chaseDistance && canSeePlayer)
        {
            // Faz o personagem se locomover pelo cen�rio at� o jogador
            agent.SetDestination(Player.transform.position);
        }
        else if (distanceToPlayer > giveUpDistance || !canSeePlayer)
        {
            // Faz o personagem retornar para o ponto de retorno
            agent.SetDestination(returnPoint.position);
        }

        // Atualiza a vari�vel de movimento para o Animator
        move = new Vector2(agent.velocity.x, agent.velocity.y);
        bool isMoving = move != Vector2.zero;
        animator.SetBool(isWalking, isMoving);

        // Atualiza a dire��o de movimento
        if (isMoving)
        {
            facingDirection = move.normalized;
            animator.SetFloat(horizontal, move.x);
            animator.SetFloat(vertical, move.y);
            animator.SetFloat(lastHorizontal, move.x);
            animator.SetFloat(lastVertical, move.y);
        }
    }

    void FixedUpdate()
    {
        // Atualiza a vari�vel de movimento para o Animator
        move = new Vector2(agent.velocity.x, agent.velocity.y);
        bool isMoving = move != Vector2.zero;
        animator.SetBool(isWalking, isMoving);

        // Atualiza a dire��o de movimento
        if (isMoving)
        {
            facingDirection = move.normalized;
            animator.SetFloat(horizontal, move.x);
            animator.SetFloat(vertical, move.y);
            animator.SetFloat(lastHorizontal, move.x);
            animator.SetFloat(lastVertical, move.y);
        }
    }

    ////Ponto para o qual o personagem ir� se mover
    //private GameObject Player;
    ////Vari�vel NavMeshAgent Para configurar A movimenta��o do personagem
    //private NavMeshAgent agent;
    //void Start()
    //{
    //    //Pega o Componente NavMeshAgent
    //    agent = GetComponent<NavMeshAgent>();
    //    //Variaveis setadas como False para N�o utilizar os eixos Y Baseado em 3 dimens�es
    //    agent.updateRotation = false;
    //    agent.updateUpAxis = false;
    //    // Encontra o ponto Na cena
    //    Player = GameObject.FindGameObjectWithTag("Player");

    //}


    //void Update()
    //{
    //    //Faz o personagem se locomover pelo cenario at� o point
    //    agent.SetDestination(Player.transform.position);
    //}
}