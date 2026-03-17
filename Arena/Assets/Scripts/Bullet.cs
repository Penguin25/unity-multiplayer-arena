using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }
    void OnCollisionEnter(Collision collison)
    {
        PlayerHealth playerHealth = collison.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(15f);
        }

        Target target = collison.gameObject.GetComponent<Target>();
        
        if (target != null)
        {
            target.TakeDamage(1f);
        }
        Destroy(gameObject);        
    }
}
