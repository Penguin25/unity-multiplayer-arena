using UnityEngine;

public class Snowball : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    [SerializeField] private float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        Target target = collision.gameObject.GetComponent<Target>();
        if (target != null)
        {
            target.TakeDamage(1f);
        }

        // TODO: spawn splatter VFX here
        Destroy(gameObject);
    }
}
