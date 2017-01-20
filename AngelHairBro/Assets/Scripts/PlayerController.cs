using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float m_maxSpeed = 10f;

    private bool m_facingRight = true;
    private bool m_facingDown = true;

    private Vector2 m_move;

    public Rigidbody2D shot;
    public Transform shotSpawn;
    public float firerate;

    private float nextFire;

	void Start ()
    {
		
	}

    /// <summary>
    /// Physics
    /// </summary>
    void FixedUpdate()
    {
        m_move.x = Input.GetAxis("Horizontal");
        m_move.y = Input.GetAxis("Vertical");

        GetComponent<Rigidbody2D>().velocity = new Vector2(m_move.x * m_maxSpeed, m_move.y * m_maxSpeed);

        if (m_move.x > 0 && !m_facingRight)
        {
            FlipChar();
        }
        else if(m_move.x < 0  && m_facingRight)
        {
            FlipChar();
        }
    }
	
	void Update ()
    {
		if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            Shoot();
        }
	}

    public float GetSpeed()
    {
        return m_maxSpeed;
    }

    void Shoot()
    {
        nextFire = Time.time + firerate;
        Rigidbody2D shotInstance = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        shotInstance.velocity = transform.forward * shot.GetComponent<Shooting>().speed;
    }

    void FlipChar()
    {
        m_facingRight = !m_facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
