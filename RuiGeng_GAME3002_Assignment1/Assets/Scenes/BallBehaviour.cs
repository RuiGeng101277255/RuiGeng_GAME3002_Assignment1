using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public float cannonForce;
    private bool spacePressed = false;
    private Rigidbody ball;
    private bool fired = false;

    // Start is called before the first frame update
    void Start()
    {
        ball = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            spacePressed = true;
        }
        if (Input.GetKeyUp("space"))
        {
            spacePressed = false;
        }
        if (!fired)
        {
            fire();
        }
    }

    //Fire cannon ball
    private void fire()
    {
        if(spacePressed)
        {
            ball.useGravity = true;
            float angle = 45.0f;
            Vector3 dir = new Vector3(0.0f, Mathf.Sin(angle), Mathf.Cos(angle));
            ball.AddForce(dir * cannonForce);
            fired = true;
        }
        else
        {
            ball.useGravity = false;
        }
    }
}
