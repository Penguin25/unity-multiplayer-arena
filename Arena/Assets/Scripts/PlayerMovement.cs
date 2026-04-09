using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private float jumpForce = 7f;
    private float fallMultiplier = 2.5f;
    private Rigidbody rb;
    private bool isGrounded;
    private bool jumpPressed;
    private bool wasInAir;
    [SerializeField] private LayerMask groundMask;
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
        if (isGrounded && !jumpPressed && velocity.y > 0)
        {
            velocity.y = 0;
        }
        rb.velocity = velocity;
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.3f, groundMask);
        if (!isGrounded)
        {
            wasInAir = true;
        }
        if (isGrounded && wasInAir)
        {
            jumpPressed = false;
            wasInAir = false;
        }
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpPressed = true;
        }
    }
}
