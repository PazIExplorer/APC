
using UnityEngine;

public class movement : MonoBehaviour
{
    public bool isJumping;
    public bool isGrounded;

    


    public Transform checkLeft;
    public Transform checkRight;

    public float moveSpeed;

    public float jumpForce;

    public Rigidbody2D rb;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(checkLeft.position, checkRight.position);

        float hori = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded==true)
        {
            isJumping = true;
        }

        Moveplay(hori);
    }
    //pour déplacer le personnage
    void Moveplay(float horizon)
    {
        Vector3 target = new Vector2(horizon, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, target,ref velocity,0.05f);

        if(isJumping == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }
}
