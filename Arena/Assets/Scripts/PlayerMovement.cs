using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        transform.Translate(h * speed * Time.deltaTime, 0, v * speed * Time.deltaTime);
    }
}
