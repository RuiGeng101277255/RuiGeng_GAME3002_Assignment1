using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private Vector3 m_PlayerVi = Vector3.zero;
    private Rigidbody m_PlayerRB = null;
    private GameObject m_AimObject = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
        }

        if (Input.GetKey(KeyCode.S))
        {
        }

        if (Input.GetKey(KeyCode.A))
        {
        }

        if (Input.GetKey(KeyCode.D))
        {
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            //Firing arrow
        }
    }
}
