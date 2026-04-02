using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Jump and body
    private Rigidbody playerRb;
    public float jumpForce = 10;
    public float overFlyForce = 100;
    public float gravityModifier;

    //Side movement
    public float horizontalInput;
    public float speed = 10;
    public float xRangeMin = -1;
    public float xRangeMax = 16;

    //Can jump
    public bool isOnGround = true;
    public float jumpCount = 2;
    public float jumpLess = 1;

    public bool gameOver = false;


    private Animator playerAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        //Side to side movement
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * horizontalInput * Time.deltaTime * speed);
        if (transform.position.x < xRangeMin)
        {
            transform.position = new Vector3(xRangeMin, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRangeMax)
        {
            transform.position = new Vector3(xRangeMax, transform.position.y, transform.position.z);
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount = jumpCount - jumpLess;
            playerAnim.SetTrigger("Jump_trig");
        }

        if (jumpCount == 0)
        {
            isOnGround = false;
            jumpCount = 2;
        }

        //Player flies off when "dead"
        if (gameOver)
        {
            isOnGround = false;
            playerRb.AddForce(Vector3.left * overFlyForce, ForceMode.Impulse);
            playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
        }
    }
}
