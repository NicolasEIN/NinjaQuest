using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class ChaseAI : MonoBehaviour
{
    // Ponto para o qual o personagem irá se mover
    private GameObject Player;
    // Variável NavMeshAgent para configurar a movimentação do personagem
    private NavMeshAgent agent;
    // Ponto de retorno caso o jogador esteja fora do alcance
    public Transform returnPoint;
    // Distância máxima permitida para iniciar a perseguição
    public float chaseDistance;
    // Distância mínima para continuar a perseguição
    public float giveUpDistance;
    // LayerMask para detectar obstáculos
    public LayerMask obstacleLayer;

    // Componentes de animação
    private Animator animator;
    private Vector2 move;
    private Vector2 facingDirection = Vector2.down;

    // Animação variables
    private const string isWalking = "IsWalking";
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";

    void Start()
    {
        // Pega o Componente NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        // Variáveis setadas como False para não utilizar os eixos Y baseado em 3 dimensões
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        // Encontra o ponto na cena
        Player = GameObject.FindGameObjectWithTag("Player");
        // Pega o componente Animator
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Verifica a distância entre o inimigo e o jogador
        float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);

        // Verifica se há linha de visão direta para o jogador
        bool canSeePlayer = !Physics.Linecast(transform.position, Player.transform.position, obstacleLayer);

        if (distanceToPlayer <= chaseDistance && canSeePlayer)
        {
            // Faz o personagem se locomover pelo cenário até o jogador
            agent.SetDestination(Player.transform.position);
        }
        else if (distanceToPlayer > giveUpDistance || !canSeePlayer)
        {
            // Faz o personagem retornar para o ponto de retorno
            agent.SetDestination(returnPoint.position);
        }

        // Atualiza a variável de movimento para o Animator
        move = new Vector2(agent.velocity.x, agent.velocity.y);
        bool isMoving = move != Vector2.zero;
        animator.SetBool(isWalking, isMoving);

        // Atualiza a direção de movimento
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
        // Atualiza a variável de movimento para o Animator
        move = new Vector2(agent.velocity.x, agent.velocity.y);
        bool isMoving = move != Vector2.zero;
        animator.SetBool(isWalking, isMoving);

        // Atualiza a direção de movimento
        if (isMoving)
        {
            facingDirection = move.normalized;
            animator.SetFloat(horizontal, move.x);
            animator.SetFloat(vertical, move.y);
            animator.SetFloat(lastHorizontal, move.x);
            animator.SetFloat(lastVertical, move.y);
        }
    }

    ////Ponto para o qual o personagem irá se mover
    //private GameObject Player;
    ////Variável NavMeshAgent Para configurar A movimentação do personagem
    //private NavMeshAgent agent;
    //void Start()
    //{
    //    //Pega o Componente NavMeshAgent
    //    agent = GetComponent<NavMeshAgent>();
    //    //Variaveis setadas como False para Não utilizar os eixos Y Baseado em 3 dimensões
    //    agent.updateRotation = false;
    //    agent.updateUpAxis = false;
    //    // Encontra o ponto Na cena
    //    Player = GameObject.FindGameObjectWithTag("Player");

    //}


    //void Update()
    //{
    //    //Faz o personagem se locomover pelo cenario até o point
    //    agent.SetDestination(Player.transform.position);
    //}
}