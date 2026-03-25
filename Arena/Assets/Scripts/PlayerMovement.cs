using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12f;
    public float jumpForce = 7f;
    private Rigidbody rb;
    private bool isGrounded;
    public LayerMask groundMask;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        Vector3 move = transform.right * h + transform.forward * v;
        Vector3 velocity = move * speed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.3f, groundMask);
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
