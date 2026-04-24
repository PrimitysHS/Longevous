using UnityEngine;

public class EnemyMeleeBasic : MonoBehaviour
{
    public enum State { Idle, Patrol, Chase, Attack, Stunned }

    [Header("Stats (área 1 - melee básico)")]
    public int maxHP = 120;
    public int damage = 25;
    public float attackCooldown = 1.2f;

    [Header("Movement")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.2f;
    public float chaseRange = 6f;
    public float attackRange = 1.1f;

    [Header("Patrol")]
    public Transform leftPoint;
    public Transform rightPoint;

    [Header("Stun")]
    public float stunnedTime = 0.25f;

    private State state = State.Idle;
    private Rigidbody2D rb;
    private Transform player;
    private float lastAttackTime = -999f;
    private float stunEndTime = -999f;
    private int patrolDir = 1; // 1 = direita, -1 = esquerda

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        state = (leftPoint != null && rightPoint != null) ? State.Patrol : State.Idle;
    }

    void Update()
    {
        if (player == null) return;

        if (state == State.Stunned)
        {
            if (Time.time >= stunEndTime)
                state = State.Chase;
            return;
        }

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= attackRange)
            state = State.Attack;
        else if (dist <= chaseRange)
            state = State.Chase;
        else
            state = (leftPoint != null && rightPoint != null) ? State.Patrol : State.Idle;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        switch (state)
        {
            case State.Idle:
                rb.velocity = new Vector2(0f, rb.velocity.y);
                break;

            case State.Patrol:
                PatrolMove();
                break;

            case State.Chase:
                ChaseMove();
                break;

            case State.Attack:
                Attack();
                break;

            case State.Stunned:
                // não mexe; knockback já empurrou
                break;
        }
    }

    void PatrolMove()
    {
        if (leftPoint == null || rightPoint == null)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            return;
        }

        float targetX = (patrolDir == 1) ? rightPoint.position.x : leftPoint.position.x;
        float dir = Mathf.Sign(targetX - transform.position.x);

        rb.velocity = new Vector2(dir * patrolSpeed, rb.velocity.y);

        // vira ao chegar perto do ponto
        if (Mathf.Abs(transform.position.x - targetX) < 0.1f)
            patrolDir *= -1;
    }

    void ChaseMove()
    {
        float dir = Mathf.Sign(player.position.x - transform.position.x);
        rb.velocity = new Vector2(dir * chaseSpeed, rb.velocity.y);
    }

    void Attack()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);

        if (Time.time - lastAttackTime < attackCooldown)
            return;

        lastAttackTime = Time.time;

        // DANO simples por proximidade (sem hitbox extra ainda)
        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        if (ph != null)
            ph.TakeDamage(damage);
    }

    // chamado quando tomar dano (pra state Stunned)
    public void OnHitStun()
    {
        state = State.Stunned;
        stunEndTime = Time.time + stunnedTime;
    }
}