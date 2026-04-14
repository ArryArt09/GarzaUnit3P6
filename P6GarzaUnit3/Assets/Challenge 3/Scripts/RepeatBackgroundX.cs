using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackgroundX : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    public float pastPoint = 50;

    private void Start()
    {
        startPos = transform.position;
        /*startPos = transform.position; // Establish the default starting position 
        repeatWidth = GetComponent<BoxCollider>().size.y / 2; // Set repeat width to half of the background
        */
    }

    private void Update()
    {
        if (transform.position.x < startPos.x - pastPoint)
        {
            transform.position = startPos;
        }
        /*
        // If background moves left by its repeat width, move it back to start position
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
        */
    }

 
}


