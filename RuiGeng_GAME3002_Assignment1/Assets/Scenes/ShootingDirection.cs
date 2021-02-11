using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingDirection : MonoBehaviour
{
    private float SpeedX = 1.0f;
    private float SpeedY = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = SpeedX * Input.GetAxis("Horizontal");
        float v = SpeedY * Input.GetAxis("Vertical");

        transform.Rotate(v, 0, h);
    }
}
