using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBehaviour : MonoBehaviour
{
    Transform barrelT;
    private Rigidbody barrel;
    private Quaternion rot = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
    // Start is called before the first frame update
    void Start()
    {
        barrelT = GetComponent<Transform>();
        barrel = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("w"))
        {
            rot.x += 0.5f;
            if(rot.x > 1.0f)
            {
                rot.x -= 1.0f;
            }
            barrel.rotation = new Quaternion(rot.x, rot.y, rot.z, 0.0f);
            //barrel.MoveRotation(new Quaternion(rot.x + 0.5f, rot.y, rot.z, 0.0f));
            //barrelT.rotation.Set(transform.rotation.x + 10.0f, transform.rotation.y, transform.rotation.z, 0.0f);
        }
        else if(Input.GetKeyDown("s"))
        {
            rot.x -= 0.5f;
            
            if (rot.x < 0.0f)
            {
                rot.x += 1.0f;
            }
            barrel.rotation = new Quaternion(rot.x, rot.y, rot.z, 0.0f);
            //barrel.MoveRotation(new Quaternion(rot.x - 0.5f, rot.y, rot.z, 0.0f));
            //barrelT.rotation.Set(transform.rotation.x - 10.0f, transform.rotation.y, transform.rotation.z, 0.0f);
        }

        if (Input.GetKeyDown("a"))
        {
            rot.y += 0.5f;
            if (rot.y > 1.0f)
            {
                rot.y -= 1.0f;
            }
            barrel.rotation = new Quaternion(rot.x, rot.y, rot.z, 0.0f);
            //barrel.MoveRotation(new Quaternion(rot.x, rot.y + 0.5f, rot.z, 0.0f));
            //barrelT.rotation.Set(transform.rotation.x, transform.rotation.y + 10.0f, transform.rotation.z, 0.0f);
        }
        else if(Input.GetKeyDown("d"))
        {
            rot.y -= 0.5f;
            if (rot.y < 0.0f)
            {
                rot.y += 1.0f;
            }
            barrel.rotation = new Quaternion(rot.x, rot.y, rot.z, 0.0f);
            //barrel.MoveRotation(new Quaternion(rot.x, rot.y - 0.5f, rot.z, 0.0f));
            //barrelT.rotation.Set(transform.rotation.x, transform.rotation.y - 10.0f, transform.rotation.z, 0.0f);
        }
    }
}
