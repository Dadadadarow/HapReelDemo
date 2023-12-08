using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public GameObject ball;
    // public GameObject racket;
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
            // racket.transform.rotation = new transform.Quarternion(racket.transform.rotation.x, racket.transform.rotation.y, 0);
            Instantiate(ball, respawnLeft.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            // racket.transform.rotation = new Quaternion(racket.transform.rotation.x, racket.transform.rotation.y, 0);
            Instantiate(ball, respawnMid.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            // racket.transform.rotation = new Quaternion(racket.transform.rotation.x, racket.transform.rotation.y, 0);
            Instantiate(ball, respawnRight.position, Quaternion.identity);
        }
    }
}
