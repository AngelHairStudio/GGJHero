using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

    public enum State { AttackingPlayer, AttackingTower };
    State currentState;

    Transform playerTarget;


    float damage = 1;
    float attackingRadius;
    float attackDelay;



    bool hasPlayerAsTarget;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        currentState = State.AttackingTower;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetCharacteristics(int damage, float enemyHealth, float moveSpeed)
    {
        this.damage = damage;
        startingHealth = enemyHealth;

    }
}
