using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameManager gameManager;
    private bool isDead = false;
    void Start()
    {
        healthSlider = GameObject.Find("HPbar").GetComponentInChildren<Slider>();
        deathPanel = GameObject.Find("Canvas").transform.Find("Panel").gameObject;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        health = maxHealth;
    }

    IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameManager.Respawn();
        deathPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;
        healthSlider.value = health / maxHealth;
        if (health <= 0)
        {
            isDead = true;
            deathPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerThrowing>().enabled = false;
            Camera.main.GetComponent<CameraFollow>().enabled = false;
            StartCoroutine(RespawnAfterDelay(5f));
        }
    }
    public void ResetHealth()
    {
        health = maxHealth;
        healthSlider.value = 1f;
        isDead = false;
    }
}
