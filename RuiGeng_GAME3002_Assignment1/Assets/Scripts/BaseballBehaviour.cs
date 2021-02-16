using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballBehaviour : MonoBehaviour
{
    public PlayerBehaviour m_player;
    public float m_BaseballSpeed = 5.0f;
    public float m_BaseBallGravity;
    private Vector3 m_BaseballVDir = Vector3.forward;
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
    }

    // Update is called once per frame
    void Update()
    {
        m_move();
        m_rotate();
        m_checkLimit();
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
        m_BaseballVDir = posDiff.normalized * m_BaseballSpeed;
    }

    private void m_move()
    {
        //Applying change in position as velocity by change in time
        transform.position += m_BaseballVDir * Time.deltaTime; //dx = vdt
    }

    private void m_rotate()
    {
        if (!(transform.position.z < m_tempTarget.z))
        {
            m_BaseballVDir.y -= m_BaseBallGravity * Time.deltaTime; //dv = adt
        }
    }

    private void m_checkLimit()
    {
        if (Vector3.Distance(transform.position, m_tempTarget) > m_MaxRange)
        {
            m_BaseballRB.velocity = Vector3.zero;
            m_player.returnedBaseball(this.gameObject);
        }
    }
}
