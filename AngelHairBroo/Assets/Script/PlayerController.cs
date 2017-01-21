using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float m_speed = 10.0f;
    private int m_floorMask;
    private Vector3 m_movement;
    private Rigidbody m_playerRB;

    void Awake()
    {
        m_floorMask = LayerMask.GetMask("Floor");
        m_playerRB = GetComponent<Rigidbody>();
    }

    void Start()
    {

    }
	
	void Update ()
    {
		
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
        transform.rotation = Quaternion.LookRotation(m_movement.normalized);
    }
}
