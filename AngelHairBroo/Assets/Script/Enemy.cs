using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    public enum State { AttackingPlayer, AttackingTower, Idle };
    State currentState;

    Transform playerTarget;
    Transform castleTarget;
    Entity castleEntity;
    UnityEngine.AI.NavMeshAgent pathfinder;

    float damage = 1;
    float attackingRadius;
    float attackDelay;


    bool hasCastleAsTarget;
    bool hasPlayerAsTarget;

    void Awake()
    {
        pathfinder = GetComponent<UnityEngine.AI.NavMeshAgent>();

        if (GameObject.FindGameObjectWithTag("Castle") != null)
        {
            hasCastleAsTarget = true;
            castleTarget = GameObject.FindGameObjectWithTag("Castle").transform;
            castleEntity = castleTarget.GetComponent<Entity>();

         
        }
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        if (hasCastleAsTarget)
        {
            currentState = State.AttackingTower;
            //castleEntity.onDeath += OnTargetDeath;
            StartCoroutine(UpdatePath());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCharacteristics(int damage, float enemyHealth, float moveSpeed)
    {
        pathfinder.speed = moveSpeed;

        this.damage = damage;
        startingHealth = enemyHealth;

    }

    IEnumerator UpdatePath()
    {
        float refreshRate = .25f;

        while (hasCastleAsTarget)
        {
            if (currentState == State.AttackingTower)
            {
                Vector3 dirToTarget = (castleTarget.position - transform.position).normalized;
                Vector3 targetPosition = castleTarget.position - dirToTarget;
                if (!dead)
                {
                    pathfinder.SetDestination(targetPosition);
                }
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }


    void OnTargetDeath()
    {
        hasCastleAsTarget = false;
        currentState = State.Idle;
    }
}
