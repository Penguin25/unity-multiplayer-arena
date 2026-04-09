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
    void Start()
    {

        health = maxHealth;
    }

    IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        gameManager.Respawn();
        Time.timeScale = 1f;
        deathPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthSlider.value = health / maxHealth;
        if (health <= 0)
        {
            deathPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerThrowing>().enabled = false;
            StartCoroutine(RespawnAfterDelay(5f));
        }
    }
    public void ResetHealth()
    {
        health = maxHealth;
        healthSlider.value = 1f;
    }
}
