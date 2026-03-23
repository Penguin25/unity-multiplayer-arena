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
    [Header("Charge Visual")]
    public Transform snowballSpawnPoint;
    public float minScale = 0.2f;
    public float maxScale = 1f;
    private GameObject chargingSnowball;

    [Header("Trajectory")]
    [SerializeField] private LineRenderer trajectoryLine;
    public int linePoints = 30;
    public float timeBetweenPoints = 0.05f;

    private float chargeTimer = 0f;
    private bool isCharging = false;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isCharging = true;
            chargeTimer = 0f;
            chargingSnowball = Instantiate(snowballPrefab, snowballSpawnPoint.position, Quaternion.identity);
            chargingSnowball.transform.localScale = Vector3.one * minScale;
            chargingSnowball.GetComponent<Rigidbody>().isKinematic = true;
            chargingSnowball.GetComponent<Collider>().enabled = false;
        }

        if (isCharging && Input.GetButton("Fire1"))
        {
            chargeTimer += Time.deltaTime;
            float chargePercent = Mathf.Clamp01(chargeTimer / chargeTime);
            float currentScale = Mathf.Lerp(minScale, maxScale, chargePercent);
            chargingSnowball.transform.localScale = Vector3.one * currentScale;
            chargingSnowball.transform.position = snowballSpawnPoint.position;
            ShowTrajectory();
        }

        if (Input.GetButtonUp("Fire1") && isCharging)
        {
            Throw();
            isCharging = false;
            trajectoryLine.positionCount = 0;
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

        Rigidbody rb = chargingSnowball.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        chargingSnowball.GetComponent<Collider>().enabled = true;
        Physics.IgnoreCollision(chargingSnowball.GetComponent<Collider>(), GetComponent<Collider>());

        rb.useGravity = true;
        rb.AddForce(direction * throwForce, ForceMode.Impulse);
        chargingSnowball.GetComponent<Snowball>().Launch();

        chargingSnowball = null;
    }
    void ShowTrajectory()
    {
        float chargePercent = Mathf.Clamp01(chargeTimer / chargeTime);
        float throwForce = Mathf.Lerp(minThrowForce, maxThrowForce, chargePercent);

        Transform cam = Camera.main.transform;
        Vector3 direction = cam.forward;
        direction = Quaternion.AngleAxis(-throwUpwardAngle * chargePercent, cam.right) * direction;

        Vector3 startPos = snowballSpawnPoint.position;
        Vector3 velocity = direction * throwForce;

        trajectoryLine.positionCount = linePoints;

        for (int i = 0; i < linePoints; i++)
        {
            float t = i * timeBetweenPoints;
            Vector3 point = startPos + velocity * t + 0.5f * Physics.gravity * t * t;
            trajectoryLine.SetPosition(i, point);
        }
    }
}
