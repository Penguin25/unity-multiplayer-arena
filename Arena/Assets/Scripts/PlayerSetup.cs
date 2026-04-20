using UnityEngine;
using Photon.Pun;

public class PlayerSetup : MonoBehaviourPun
{
    void Start()
    {
        if (photonView.IsMine)
        {
            Camera.main.GetComponent<CameraFollow>().target = transform;
            GameObject.Find("GameManager").GetComponent<GameManager>().RegisterPlayer(transform);
        }
        else
        {
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerThrowing>().enabled = false;
        }
    }
}
