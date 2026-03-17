using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
