using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 30f;
    [SerializeField] private float fireRate = 2f;
    private float timer = 0f;
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= fireRate)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.forward * 2f, transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
            timer = 0f;
        }
    }
}
