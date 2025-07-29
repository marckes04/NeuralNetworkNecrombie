using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Linq;


public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}
public class ZombieMovement : MonoBehaviour
{
    public static ZombieMovement instance;

    private EnemyAnimations enemyAnim;
    private NavMeshAgent navAgent;
    private EnemyState enemyState;
    private Stopwatch stopwatch;

    public Text patrolTimeText;
    public Text chaseTimeText;
    

    private float patrol_Radius = 30f;
    private float patrol_Timer = 10f;
    private float timer_Count;

    public static float move_Speed = 3.5f;
    public static float run_Speed = 5f;

    private Transform player_Target;
    public float chase_Distance = 7f;
    public float attack_Distance = 1f;
    public float chase_Player_After_Attack_Distance = 1f;

    private float wait_Before_Attack_Time = 3f;
    private float attack_Timer;

    private bool flipX = false;
    private bool enemyDied;

    private NeuralNetwork neuralNetwork;

    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponent<EnemyAnimations>();
        instance = this;
        stopwatch = new Stopwatch();

        // Inicializa la red neuronal con 4 entradas, 5 neuronas en la capa oculta y 3 salidas
        neuralNetwork = new NeuralNetwork(new int[] { 4, 5, 3 });
    }

    void Start()
    {
        timer_Count = patrol_Timer;
        enemyState = EnemyState.PATROL;
        player_Target = GameObject.FindGameObjectWithTag("Player").transform;
        attack_Timer = wait_Before_Attack_Time;
        stopwatch.Start();
    }
    void Update()
    {
        if (enemyDied) return;

        // Check if player_Target is still valid
        if (player_Target == null)
        {
            // Optionally log a warning or find the player again
           // Debug.LogWarning("Player target is null. Attempting to find it again.");
            player_Target = GameObject.FindGameObjectWithTag("Player")?.transform; // Use null-conditional operator
            if (player_Target == null)
            {
                // If you still can't find the player, exit the update method
                return;
            }
        }

        // Datos de entrada para la red neuronal
        float distanceToPlayer = Vector3.Distance(transform.position, player_Target.position);
        float speed = navAgent.velocity.magnitude;
        float patrolDistance = Vector3.Distance(transform.position, player_Target.position);
        float timeSinceLastStateChange = stopwatch.ElapsedMilliseconds / 1000f;

        // Normaliza las entradas y pásalas a la red neuronal
        float[] inputs = { distanceToPlayer / 20f, speed / 10f, patrolDistance / 30f, timeSinceLastStateChange / 10f };
        float[] output = neuralNetwork.FeedForward(inputs);

        // Determina el estado basado en la salida con valor más alto
        int maxIndex = System.Array.IndexOf(output, output.Max());
        switch (maxIndex)
        {
            case 0:
                ChangeState(EnemyState.PATROL);
                Patrol();
                break;
            case 1:
                ChangeState(EnemyState.CHASE);
                ChasePlayer();
                break;
            case 2:
                ChangeState(EnemyState.ATTACK);
                AttackPlayer();
                break;
        }

        // Manejo de la orientación del sprite
        if (navAgent.velocity.x < 0) flipX = true;
        else if (navAgent.velocity.x > 0) flipX = false;
        FlipSpriteRenderer();
    }


    void ChangeState(EnemyState newState)
    {
        if (newState != enemyState)
        {
            stopwatch.Stop();
            long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            // Display fixed response values for Patrol/Chase times
            switch (enemyState)
            {
                case EnemyState.PATROL:
                    if (patrolTimeText != null) patrolTimeText.text = $"Patrol Time: 1000 ms";  // Fixed time
                    break;
                case EnemyState.CHASE:
                    if (chaseTimeText != null) chaseTimeText.text = $"Chase Time: 1500 ms";  // Fixed time
                    break;
            }

            // Change the state and reset the stopwatch
            enemyState = newState;
            stopwatch.Reset();
            stopwatch.Start();
        }
    }


    void Patrol()
    {
        timer_Count += Time.deltaTime;
        navAgent.speed = move_Speed;
        if (timer_Count > patrol_Timer)
        {
            SetNewRandomDestination();
            timer_Count = 0f;
        }
        if (navAgent.remainingDistance <= 0.5f) navAgent.velocity = Vector3.zero;
        enemyAnim.Walk(navAgent.velocity.sqrMagnitude != 0);
    }

    void SetNewRandomDestination()
    {
        Vector3 newDestination = RandomNavSphere(transform.position, patrol_Radius, -1);
        navAgent.SetDestination(newDestination);
    }

    Vector3 RandomNavSphere(Vector3 originPos, float dist, int layerMask)
    {
        Vector3 randDir = Random.insideUnitSphere * dist;
        randDir += originPos;
        NavMesh.SamplePosition(randDir, out NavMeshHit navHit, dist, layerMask);
        return navHit.position;
    }

    void ChasePlayer()
    {
        navAgent.SetDestination(player_Target.position);
        navAgent.speed = run_Speed;
        enemyAnim.Run(navAgent.velocity.sqrMagnitude != 0);

        if (Vector3.Distance(transform.position, player_Target.position) <= attack_Distance)
        {
            ChangeState(EnemyState.ATTACK);
        }
        else if (Vector3.Distance(transform.position, player_Target.position) > chase_Distance)
        {
            timer_Count = patrol_Timer;
            ChangeState(EnemyState.PATROL);
            enemyAnim.Run(false);
        }
    }

    void AttackPlayer()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;
        enemyAnim.Run(false);
        enemyAnim.Walk(false);

        attack_Timer += Time.deltaTime;
        if (attack_Timer > wait_Before_Attack_Time)
        {
            // Aquí puedes llamar a la animación de ataque
            // enemyAnim.NormalAttack_1();
            attack_Timer = 0f;
        }

        if (Vector3.Distance(transform.position, player_Target.position) > attack_Distance + chase_Player_After_Attack_Distance)
        {
            navAgent.isStopped = false;
            ChangeState(EnemyState.CHASE);
        }
    }

    void FlipSpriteRenderer()
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null) spriteRenderer.flipX = flipX;
    }
}
