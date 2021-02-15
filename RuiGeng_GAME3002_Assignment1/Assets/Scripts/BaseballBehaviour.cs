using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballBehaviour : MonoBehaviour
{
    public PlayerBehaviour m_player;
    public float m_ArrowSpeed = 5.0f;
    public float m_BaseBallGravity;
    private Vector3 m_ArrowVelDir = Vector3.forward;
    private Rigidbody m_BaseballRB = null;
    private SphereCollider m_BaseBallCol = null;

    private Vector3 m_tempTarget = Vector3.zero;

    private bool m_inUse = false;

    public float m_MaxRange = 30.0f;
    public Bounds m_bounds;
    public bool m_collided;

    private MeshFilter m_meshFilter;

    private AudioSource m_swingingsfx;

    // Start is called before the first frame update
    void Start()
    {
        m_meshFilter = GetComponent<MeshFilter>();
        m_BaseballRB = GetComponent<Rigidbody>();
        m_BaseBallCol = GetComponent<SphereCollider>();
        m_BaseballRB.transform.SetParent(transform);
        m_BaseBallCol.transform.SetParent(transform);
        m_bounds = m_meshFilter.mesh.bounds;
        m_collided = false;
        m_player = FindObjectOfType<PlayerBehaviour>();
        m_swingingsfx = GetComponent<AudioSource>();
        m_swingingsfx.Play();
        CreateArrow();
    }

    // Update is called once per frame
    void Update()
    {
        m_move();
        m_rotate();
        m_checkLimit();
        //if (m_inUse)
        //{
        //    transform.position += Vector3.forward * m_ArrowSpeed;
        //    Debug.Log(transform.position);
        //}
    }

    public void SetUse(bool use)
    {
        m_inUse = use;
    }

    public bool GetUse()
    {
        return m_inUse;
    }

    public void setTarget(Vector3 tar)
    {
        m_tempTarget = tar;
        Vector3 posDiff = m_tempTarget - transform.position;
        m_ArrowVelDir = posDiff.normalized * m_ArrowSpeed;
        //m_swingingsfx.Play();
    }

    private void CreateArrow()
    {
        //m_ArrowObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //m_ArrowObj.transform.position = Vector3.zero;
        //m_ArrowObj.transform.localScale = new Vector3(0.1f, 1.0f, 0.1f);
        //m_ArrowObj.GetComponent<Renderer>().material.color = Color.green;
        //m_ArrowObj.GetComponent<Collider>().enabled = true;
    }

    private void m_move()
    {
        //m_BaseballRB.isKinematic = true;
        //m_BaseballRB.velocity += m_ArrowVelDir;
        //m_BaseballRB.AddForce(new Vector3(0.0f, -0.0098f, 0.0f));


        transform.position += m_ArrowVelDir * Time.deltaTime; //dx = vdt
        //Gravity


        //m_BaseballRB.position += m_BaseballRB.velocity * 0.01f;
        //m_ArrowVelDir = Vector3.zero;
        //m_BaseballRB.AddForce(m_ArrowVelDir * 500);
        //m_ArrowVelDir = Vector3.zero;

        //transform.position += m_ArrowVelDir * Time.deltaTime;
        //transform.position += new Vector3(0.0f, 0.0001f, 0.0f) * Physics.gravity.y;
    }

    private void m_rotate()
    {
        if (transform.position.z < m_tempTarget.z)
        {

            
            //Quaternion rotation = Quaternion.LookRotation(posDiff, Vector3.up);
            //transform.rotation = rotation;
        }
        else
        {
            m_ArrowVelDir.y -= m_BaseBallGravity * Time.deltaTime; //dv = adt

            //m_ArrowVelDir.y -= 0.1f;
            //m_ArrowVelDir.y = 0.0f;
        }
    }

    private void m_checkLimit()
    {
        if (Vector3.Distance(transform.position, m_tempTarget) > m_MaxRange)
        {
            m_BaseballRB.velocity = Vector3.zero;
            m_player.returnedArrow(this.gameObject);
        }
    }
}
