using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 30f;
    public float fireRate = 2f;
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
