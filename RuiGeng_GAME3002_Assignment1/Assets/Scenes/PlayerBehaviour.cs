using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private Vector3 m_PlayerVComp = Vector3.zero;
    private Vector3 m_AimVComp = Vector3.zero;
    private Rigidbody m_PlayerRB = null;
    private GameObject m_AimObject = null;

    private float deltaSpeed1D = 0.01f;


    // Start is called before the first frame update
    void Start()
    {
        m_PlayerRB = GetComponent<Rigidbody>();
        createAimObject();
    }

    // Update is called once per frame
    void Update()
    {
        updatePlayerInput();
        updateAimObject();
    }

    private void createAimObject()
    {
        m_AimObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Vector3 tempPos = new Vector3(0.0f, 0.0f, transform.position.z + 5.0f);
        m_AimObject.transform.position = tempPos;
        //m_AimObject.transform.position = Vector3.zero;
        m_AimObject.transform.localScale = new Vector3(0.15f, 0.15f, 0.05f);
        m_AimObject.GetComponent<Renderer>().material.color = Color.red;
        m_AimObject.GetComponent<Collider>().enabled = false;
    }

    private void updatePlayerInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            m_PlayerVComp.z = deltaSpeed1D;
        }

        if (Input.GetKey(KeyCode.S))
        {
            m_PlayerVComp.z = -deltaSpeed1D;
        }

        if (Input.GetKey(KeyCode.A))
        {
            m_PlayerVComp.x = -deltaSpeed1D;
        }

        if (Input.GetKey(KeyCode.D))
        {
            m_PlayerVComp.x = deltaSpeed1D;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            //Firing arrow
        }

        if (Input.GetKeyUp(KeyCode.W)|| Input.GetKeyUp(KeyCode.S))
        {
            m_PlayerVComp.z = 0.0f;
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            m_PlayerVComp.x = 0.0f;
        }

        transform.position += m_PlayerVComp;
        m_AimObject.transform.position += m_PlayerVComp;
    }

    private void updateAimObject()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_AimVComp.y = deltaSpeed1D;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            m_AimVComp.y = -deltaSpeed1D;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_AimVComp.x = -deltaSpeed1D;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_AimVComp.x = deltaSpeed1D;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            m_AimVComp.y = 0.0f;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            m_AimVComp.x = 0.0f;
        }

        float xDir = Input.GetAxis("Horizontal");
        float yDir = Input.GetAxis("Vertical");

        m_AimObject.transform.position += m_AimVComp;
    }

    private void launchArrow()
    {

    }
}
