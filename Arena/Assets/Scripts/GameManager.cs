using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform player;
    private int deathCount = 0;
    void Start()
    {
        Debug.Log("Respawn points: " + spawnPoints.Length);
        int index = Random.Range(0, spawnPoints.Length);
        player.position = spawnPoints[index].position;
    }
    public void Respawn()
    {
        int index = UnityEngine.Random.Range(0, spawnPoints.Length);
        player.position = spawnPoints[index].position;
        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        ph.ResetHealth();
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerThrowing>().enabled = true;
        Camera.main.GetComponent<CameraFollow>().enabled = true;
        deathCount++;
        Debug.Log("Death count: " + deathCount);

    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public int GetDeathCount()
    {
        return deathCount;
    }
}
