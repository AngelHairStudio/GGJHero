using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float m_speed = 10.0f;
    private float m_launchForce;
    private float m_attackRate;
    private float m_cooldown;

    private string m_enemyTag = "Enemy";

    private Vector3 m_movement;
    private Rigidbody m_playerRB;

    public Rigidbody m_projectile;
    public Transform m_muzzle_Transform;

    //Animation stuff
    private Animator animator;
    public Transform grapTrans;
    private Quaternion fixedRot;
    bool facingRight;

    void Awake()
    {
        m_playerRB = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        fixedRot = grapTrans.rotation;
        facingRight = true;

        m_launchForce = 30.0f;
        m_cooldown = 0.1f;
    }
	
	void Update ()
    {

        grapTrans.rotation = fixedRot;

        m_attackRate += Time.deltaTime;
        if (m_attackRate >= m_cooldown)
        {
            if (Input.GetButton("Fire1"))
            {
                Shoot(m_enemyTag);
                m_attackRate = 0;
            }
        }
	}

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        MovePlayer(moveHorizontal, moveVertical);
    }

    void MovePlayer(float horiz, float vertic)
    {
        m_movement.Set(horiz, 0.0f, vertic);
        m_movement = m_movement.normalized * m_speed * Time.deltaTime;
        m_playerRB.MovePosition(transform.position + m_movement);
        if(horiz != 0 || vertic != 0)
        {
            transform.rotation = Quaternion.LookRotation(m_movement.normalized);
            animator.SetFloat("speed", 1);

            
            if (horiz > 0 && !facingRight ||horiz < 0 && facingRight)
            {
                facingRight = !facingRight;

                Vector3 tempScal = grapTrans.localScale;
                tempScal.x *= -1;

                grapTrans.localScale = tempScal;
            }


        }
        else
            animator.SetFloat("speed", 0);
    }

    void Shoot(string tagname)
    {
        Rigidbody projInst = Instantiate(m_projectile, m_muzzle_Transform.position, m_muzzle_Transform.rotation) as Rigidbody;
        projInst.velocity = m_launchForce * m_muzzle_Transform.forward;
    }
}
