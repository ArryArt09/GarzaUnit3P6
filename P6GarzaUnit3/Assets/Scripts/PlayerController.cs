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

    public bool doubleSpeed = false;

    //Can jump
    public bool isOnGround = true;
    public float jumpCount = 2;
    public float jumpLess = 1;

    public bool gameOver = false;
    public bool windUp = false;

    //Animation
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;


    //SFX
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;

    //Score
    public float scoreTotal = 0f;
    public float addUp = 3.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Side to side movement
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * horizontalInput * Time.deltaTime * speed);
        if (transform.position.x < xRangeMin && !windUp)
        {
            transform.position = new Vector3(xRangeMin, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRangeMax && !windUp)
        {
            transform.position = new Vector3(xRangeMax, transform.position.y, transform.position.z);
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount = jumpCount - jumpLess;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);        
        }

        if (jumpCount == 0)
        {
            isOnGround = false;
            jumpCount = 2;
        }

        //Run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            doubleSpeed = true;
            playerAnim.SetFloat("Speed_Multiplier", 2.0f);
        }
        else if (doubleSpeed)
        {
            doubleSpeed = false;
            playerAnim.SetFloat("Speed_Multiplier", 1.0f);
        }


        //Player flies off when "dead"
        if (gameOver)
        {
            isOnGround = false;
            playerRb.AddForce(Vector3.left * overFlyForce, ForceMode.Impulse);
            playerRb.AddForce(Vector3.up * 1);
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            explosionParticle.Play();
            dirtParticle.Stop();
        }
    }
}
