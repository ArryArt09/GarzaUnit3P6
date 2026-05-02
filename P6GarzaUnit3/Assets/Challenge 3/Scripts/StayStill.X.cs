using UnityEditor.Rendering;
using UnityEngine;

public class StayStill : MonoBehaviour
{
    public float Xpos = 0f;
    public float Ypos = 0f;
    public float Zpos = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Xpos, Ypos, Zpos);
    }
}
