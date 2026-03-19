using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 35f;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            Vector3 targetPoint;
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(100f);
            }

            Vector3 spawnPos = transform.position + transform.forward * 2f;
            GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
            Vector3 direction = (targetPoint - spawnPos).normalized;
            bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
        }
    }
}
