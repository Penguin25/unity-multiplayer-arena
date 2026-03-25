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
    [SerializeField] private GameObject landingMarker;
    [SerializeField] private LayerMask trajectoryLayerMask;
    public int linePoints = 30;
    public float timeBetweenPoints = 0.05f;

    [Header("Quick Throw")]
    public float quickThrowForce = 15f;
    public float quickSnowballScale = 0.2f;
    private float chargeTimer = 0f;
    private bool isChargingQuick = false;
    private bool isChargingSuper = false;

    void Start()
    {
        trajectoryLine.useWorldSpace = true;
        landingMarker.SetActive(false);
    }

    void Update()
  {
      // === FIRE1 — быстрый бросок ===
      if (Input.GetButtonDown("Fire1") && !isChargingSuper)
      {
          isChargingQuick = true;
          SpawnChargingSnowball(quickSnowballScale);
      }

      if (isChargingQuick && Input.GetButton("Fire1"))
      {
          chargingSnowball.transform.position = snowballSpawnPoint.position;
          chargeTimer = 0f; // сила не растёт
          ShowTrajectory(quickThrowForce);
      }

      if (Input.GetButtonUp("Fire1") && isChargingQuick)
      {
          ThrowWithForce(quickThrowForce, 0f);
          isChargingQuick = false;
          trajectoryLine.positionCount = 0;
          landingMarker.SetActive(false);
      }

      // === FIRE2 — усиленный бросок ===
      if (Input.GetButtonDown("Fire2") && !isChargingQuick)
      {
          isChargingSuper = true;
          chargeTimer = 0f;
          SpawnChargingSnowball(minScale);
      }

      if (isChargingSuper && Input.GetButton("Fire2"))
      {
          chargeTimer += Time.deltaTime;
          float chargePercent = Mathf.Clamp01(chargeTimer / chargeTime);
          float currentScale = Mathf.Lerp(minScale, maxScale, chargePercent);
          chargingSnowball.transform.localScale = Vector3.one * currentScale;
          chargingSnowball.transform.position = snowballSpawnPoint.position;

          float force = Mathf.Lerp(minThrowForce, maxThrowForce, chargePercent);
          ShowTrajectory(force);
      }

      if (Input.GetButtonUp("Fire2") && isChargingSuper)
      {
          float chargePercent = Mathf.Clamp01(chargeTimer / chargeTime);
          float force = Mathf.Lerp(minThrowForce, maxThrowForce, chargePercent);
          ThrowWithForce(force, chargePercent);
          isChargingSuper = false;
          trajectoryLine.positionCount = 0;
          landingMarker.SetActive(false);
      }
  }
  void SpawnChargingSnowball(float scale)
  {
      chargingSnowball = Instantiate(snowballPrefab, snowballSpawnPoint.position, Quaternion.identity);
      chargingSnowball.transform.localScale = Vector3.one * scale;
      chargingSnowball.GetComponent<Rigidbody>().isKinematic = true;
      chargingSnowball.GetComponent<Collider>().enabled = false;
  }
  void ThrowWithForce(float force, float chargePercent)
  {
      Transform cam = Camera.main.transform;
      Vector3 direction = cam.forward;
      direction = Quaternion.AngleAxis(-throwUpwardAngle * chargePercent, cam.right) * direction;

      Rigidbody rb = chargingSnowball.GetComponent<Rigidbody>();
      rb.isKinematic = false;
      chargingSnowball.GetComponent<Collider>().enabled = true;
      Physics.IgnoreCollision(chargingSnowball.GetComponent<Collider>(), GetComponent<Collider>());

      rb.useGravity = true;
      rb.AddForce(direction * force, ForceMode.Impulse);
      chargingSnowball.GetComponent<Snowball>().Launch();

      chargingSnowball = null;
  }
  void ShowTrajectory(float force)
  {
      float chargePercent = Mathf.Clamp01(chargeTimer / chargeTime);

      Transform cam = Camera.main.transform;
      Vector3 direction = cam.forward;
      direction = Quaternion.AngleAxis(-throwUpwardAngle * chargePercent, cam.right) * direction;

      Vector3 startPos = snowballSpawnPoint.position;
      float mass = chargingSnowball.GetComponent<Rigidbody>().mass;
      Vector3 velocity = direction * (force / mass);

      trajectoryLine.positionCount = linePoints;
      bool hitFound = false;

      for (int i = 0; i < linePoints; i++)
      {
          float t = i * timeBetweenPoints;
          Vector3 point = startPos + velocity * t + 0.5f * Physics.gravity * t * t;

          if (i > 0)
          {
              Vector3 prevPoint = trajectoryLine.GetPosition(i - 1);
              Vector3 dir = point - prevPoint;

              if (Physics.Raycast(prevPoint, dir.normalized, out RaycastHit hit, dir.magnitude, trajectoryLayerMask))
              {
                  trajectoryLine.positionCount = i + 1;
                  trajectoryLine.SetPosition(i, hit.point);
                  landingMarker.SetActive(true);
                  landingMarker.transform.position = hit.point + Vector3.up * 0.05f;
                  hitFound = true;
                  break;
              }
          }

          trajectoryLine.SetPosition(i, point);
      }

      if (!hitFound)
      {
          landingMarker.SetActive(false);
      }
  }
}
