using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProj : MonoBehaviour
{
    private float m_damage = 10.0f;

    private const float m_LIFESPAN = 3.0f;
    private const float m_RADIUS = 0.5f;

    void Start()
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

        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Entity>().TakeDamage(m_damage);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Environment")
        {
            Destroy(gameObject);
        }

        if(other.gameObject.tag == "Castle")
        {
            other.gameObject.GetComponent<HQ>().TakeDamage(m_damage);
            Destroy(gameObject);
        }

        if(other.gameObject.tag == "Enemy") 
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
        }
    }

}
