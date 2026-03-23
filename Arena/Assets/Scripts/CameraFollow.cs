using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    public Transform target;
    public float sensitivity = 3f;
    private Vector3 offset = new Vector3(0, 3, -6);
    private float yaw = 0f;
    private float pitch = 0f;
    void Update()
    {     
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y")* sensitivity;
        pitch = Mathf.Clamp(pitch, -80f, 60f);
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.position = target.position + rotation * offset;
        transform.LookAt(target.position + Vector3.up * 2.5f);
    }
}
