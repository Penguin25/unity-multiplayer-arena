using UnityEngine;
using Photon.Pun;

public class GameSetup : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            int index = Random.Range(0, spawnPoints.Length);
            PhotonNetwork.Instantiate("Player", spawnPoints[index].position, spawnPoints[index].rotation);
        }
    }
}
