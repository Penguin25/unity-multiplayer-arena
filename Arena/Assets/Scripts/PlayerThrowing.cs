using UnityEngine;

public class PlayerThrowing : MonoBehaviour
{
    [Header("Snowball")]
    public GameObject snowballPrefab;

    [Header("Throw Settings")]
    public float minThrowForce = 10f;
    public float maxThrowForce = 30f;
    public float chargeTime = 3f;
    public float throwUpwardAngle = 15f;

    private float chargeTimer = 0f;
    private bool isCharging = false;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isCharging = true;
            chargeTimer = 0f;
        }

        if (isCharging && Input.GetButton("Fire1"))
        {
            chargeTimer += Time.deltaTime;
        }

        if (Input.GetButtonUp("Fire1") && isCharging)
        {
            Throw();
            isCharging = false;
        }
    }

    void Throw()
    {
        float chargePercent = Mathf.Clamp01(chargeTimer / chargeTime);
        float throwForce = Mathf.Lerp(minThrowForce, maxThrowForce, chargePercent);

        // Direction: camera forward with slight upward arc
        Transform cam = Camera.main.transform;
        Vector3 direction = cam.forward;
        direction = Quaternion.AngleAxis(-throwUpwardAngle * chargePercent, cam.right) * direction;

        Vector3 spawnPos = transform.position + cam.forward * 1.5f + Vector3.up * 1.5f;

        GameObject snowball = Instantiate(snowballPrefab, spawnPos, Quaternion.identity);
        Rigidbody rb = snowball.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.AddForce(direction * throwForce, ForceMode.Impulse);
    }
}
