using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    public enum State { AttackingPlayer, AttackingTower, Idle };
    State currentState;

    public bool pauseGame;
    public Rigidbody m_projectile;
    public Transform m_muzzle_Transform;

    Transform playerTarget;
    Transform castleTarget;
    Entity castleEntity;
    Entity playerEntity;

    UnityEngine.AI.NavMeshAgent pathfinder;

    [SerializeField]
    private float damage = 1;
    [SerializeField]
    private float attackSpped = 1;
    [SerializeField]
    private float attackingRadius = 0.5f;
    [SerializeField]
    private float chaseRadius = 3;
    [SerializeField]
    private float attackDelay;

    private float nextAttackTime;
    private float m_launchForce;

    bool hasCastleAsTarget;

    //Animation stuff
    private Animator animator;
    public Transform grapTrans;
    private Quaternion fixedRot;


    void Awake()
    {
        pathfinder = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        fixedRot = grapTrans.rotation;

        if (GameObject.FindGameObjectWithTag("Castle") != null)
        {
            hasCastleAsTarget = true;
            castleTarget = GameObject.FindGameObjectWithTag("Castle").transform;
            castleEntity = castleTarget.GetComponent<Entity>();
        }

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
            playerEntity = castleTarget.GetComponent<Entity>();
        }

    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        if (hasCastleAsTarget)
        {
            currentState = State.AttackingTower;
            StartCoroutine(UpdatePath());
        }
        m_launchForce = 30.0f;
        attackDelay = 0.4f;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!pauseGame)
        {
            grapTrans.rotation = fixedRot;
            //UpdatePath();
            CheckAttackTarget();

        }
    }

    private void CheckAttackTarget()
    {
        

        if (Time.time > nextAttackTime)
        {
            if (playerTarget != null && Vector3.Distance(playerTarget.position, transform.position) <= attackingRadius)
                StartCoroutine(Attack());

            if (castleTarget != null && Vector3.Distance(castleTarget.position, transform.position) <= attackingRadius)
                StartCoroutine(Attack());

            nextAttackTime = Time.time + attackDelay;    
        }

        if (playerTarget != null && Vector3.Distance(playerTarget.position, transform.position) <= chaseRadius && hasCastleAsTarget == true)
        {
            hasCastleAsTarget = false;
            currentState = State.AttackingPlayer;
            StartCoroutine(UpdatePath());
        }
        else
        {
            hasCastleAsTarget = true;
            currentState = State.AttackingTower;
        }
    }

    public void SetCharacteristics(int damage, float enemyHealth, float moveSpeed)
    {
        this.damage = damage;
        startingHealth = enemyHealth;
        pathfinder.speed = moveSpeed;

    }

    IEnumerator UpdatePath()
    {
        float refreshRate = .25f;
        //Change the enemy so it follow the player
        while (hasCastleAsTarget == false)
        {
            if (currentState == State.AttackingPlayer)
            {
                Vector3 dirToTarget = (playerTarget.position - transform.position).normalized;
                Vector3 targetPosition = playerTarget.position - dirToTarget;
                if (!dead && pathfinder.enabled == true)
                {
                    pathfinder.SetDestination(targetPosition);
                }
            }
            yield return new WaitForSeconds(refreshRate);

        }

        //Change the enemy so it moves twoards the tower
        while (hasCastleAsTarget)
        {
            if (currentState == State.AttackingTower)
            {
                Vector3 dirToTarget = (castleTarget.position - transform.position).normalized;
                Vector3 targetPosition = castleTarget.position - dirToTarget;
                if (!dead && pathfinder.enabled == true)
                {
                    pathfinder.SetDestination(targetPosition);
                }
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }

    IEnumerator Attack()
    {
        if (hasCastleAsTarget == false)
            currentState = State.AttackingPlayer;
        else if (hasCastleAsTarget)
            currentState = State.AttackingTower;

        pathfinder.enabled = false;

        Vector3 dirToTarget;

        switch (currentState)
        {
            case State.AttackingPlayer:
                dirToTarget = (playerTarget.position - transform.position).normalized;
                break;
            case State.AttackingTower:
                dirToTarget = (castleTarget.position - transform.position).normalized;
                break;
            default:
                break;
        }

        float percent = 0;
        bool haveAttacked = false;
        while (percent <= 1)
        {
            if(percent >= .5f && !haveAttacked)
            {
                haveAttacked = true;
                Shoot();                
            }

            percent += Time.deltaTime * attackSpped;
            yield return null;

        }

        pathfinder.enabled = true;
    }

    void OnTargetDeath()
    {
        hasCastleAsTarget = false;
        currentState = State.Idle;
    }

    void Shoot()
    {
        Rigidbody projInst = Instantiate(m_projectile, m_muzzle_Transform.position, m_muzzle_Transform.rotation) as Rigidbody;
        projInst.velocity = m_launchForce * m_muzzle_Transform.forward;
    }
}
