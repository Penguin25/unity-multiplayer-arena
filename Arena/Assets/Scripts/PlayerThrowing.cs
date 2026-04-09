using UnityEngine;
using UnityEngine.UI;

public class PlayerThrowing : MonoBehaviour
{
    [Header("Snowball")]
    [SerializeField] private GameObject snowballPrefab;

    [Header("Throw Settings")]
    [SerializeField] private float minThrowForce = 10f;
    [SerializeField] private float maxThrowForce = 30f;
    [SerializeField] private float chargeTime = 3f;
    [SerializeField] private float throwUpwardAngle = 15f;

    [Header("Charge Visual")]
    [SerializeField] private Transform snowballSpawnPoint;
    [SerializeField] private float minScale = 0.2f;
    [SerializeField] private float maxScale = 1f;
    private GameObject chargingSnowball;

    [Header("Trajectory")]
    [SerializeField] private LineRenderer trajectoryLine;
    [SerializeField] private GameObject landingMarker;
    [SerializeField] private LayerMask trajectoryLayerMask;
    [SerializeField] private int linePoints = 30;
    [SerializeField] private float timeBetweenPoints = 0.05f;

    [Header("Quick Throw")]
    [SerializeField] private float quickThrowForce = 15f;
    [SerializeField] private float quickSnowballScale = 0.2f;

    [Header("Cooldowns")]
    [SerializeField] private float quickThrowCooldown = 1f;
    [SerializeField] private float superThrowCooldown = 12f;
    private float quickThrowTimer = 0f;
    private float superThrowTimer = 0f;

    [Header("Cooldown UI")]
    [SerializeField] private Image quickCooldownImage;
    [SerializeField] private Image superCooldownImage;

    [Header("Damage")]
    [SerializeField] private float baseDamage = 10f;
    [SerializeField] private float damagePerSecond = 10f;

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
      quickThrowTimer -= Time.deltaTime;
      superThrowTimer -= Time.deltaTime;

      // обновляем UI кулдаунов
      quickCooldownImage.fillAmount = quickThrowTimer > 0 ? quickThrowTimer / quickThrowCooldown : 0f;
      superCooldownImage.fillAmount = superThrowTimer > 0 ? superThrowTimer / superThrowCooldown : 0f;

      // === FIRE1 — быстрый бросок ===
      if (Input.GetButtonDown("Fire1") && !isChargingSuper && quickThrowTimer <= 0)
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
          ThrowWithForce(quickThrowForce, 0f, baseDamage);
          isChargingQuick = false;
          quickThrowTimer = quickThrowCooldown;
          trajectoryLine.positionCount = 0;
          landingMarker.SetActive(false);
      }

      // === FIRE2 — усиленный бросок ===
      if (Input.GetButtonDown("Fire2") && !isChargingQuick && superThrowTimer <= 0)
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
          float damage = baseDamage + damagePerSecond * Mathf.Min(chargeTimer, chargeTime);
          ThrowWithForce(force, chargePercent, damage);
          isChargingSuper = false;
          superThrowTimer = superThrowCooldown;
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
  void ThrowWithForce(float force, float chargePercent, float damage)
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
      Snowball snowball = chargingSnowball.GetComponent<Snowball>();
      snowball.SetDamage(damage);
      snowball.Launch();

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
