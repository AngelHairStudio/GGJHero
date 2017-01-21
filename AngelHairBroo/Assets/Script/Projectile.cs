using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float m_damage = 10.0f;

    private const float m_LIFESPAN = 3.0f;
    private const float m_RADIUS = 0.5f;

	void Start ()
    {
        gameObject.GetComponent<Collider>().isTrigger = true;
        Destroy(gameObject, m_LIFESPAN);
	}

    /// <summary>
    /// Collision handling
    /// </summary>
    /// <param name="other">Object the projectile collides with</param>
    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_RADIUS);

        if (other.gameObject.tag == "Enemy")
        {
                other.gameObject.GetComponent<Enemy>().TakeDamage(m_damage);
                Destroy(gameObject);
        }

        if (other.gameObject.tag == "Environment")
        {
            Destroy(gameObject);
        }
    }
}
