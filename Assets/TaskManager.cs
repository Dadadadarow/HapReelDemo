using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public GameObject ball;
    public GameObject racket;
    public Transform respawnRight;
    public Transform respawnLeft;
    public Transform respawnMid;
    //aaaa
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            racket.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            Instantiate(ball, respawnLeft.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            racket.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            Instantiate(ball, respawnMid.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            racket.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            Instantiate(ball, respawnRight.position, Quaternion.identity);
        }
    }
}
