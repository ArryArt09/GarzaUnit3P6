using UnityEngine;

public class MoveLeft : MonoBehaviour
{

    private float speed = 30;
    private PlayerController playerControllerScript;

    public float OOB = -5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);


            //Destroy OOB - out of bounds
            if (transform.position.y < OOB && gameObject.CompareTag("Obstacle"))
            {
                Destroy(gameObject);
            }
        }
    }
}
