using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform player;
    public void Respawn()
    {
        int index = UnityEngine.Random.Range(0, spawnPoints.Length);
        player.position = spawnPoints[index].position;
        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        ph.ResetHealth();
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerShooting>().enabled = true;
        Camera.main.GetComponent<CameraFollow>().enabled = true;

    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
