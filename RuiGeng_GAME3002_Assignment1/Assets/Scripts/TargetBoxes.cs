using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBoxes : MonoBehaviour
{
    public float HitTimer;
    private Renderer m_BoxRenderer;

    private bool m_targetHit = false;
    public bool m_hitCounted = false;
    private float m_remainTimer = 0.0f;

    private ParticleSystem m_particles;
    private AudioSource m_explosionsfx;
    private AudioSource m_triggersfx;

    public bool m_IsDetection; // To check for stepping platform
    public PlayerBehaviour m_Player;

    // Start is called before the first frame update
    void Start()
    {
        m_BoxRenderer = GetComponent<Renderer>();
        m_BoxRenderer.material.color = Color.white;
        m_particles = GetComponent<ParticleSystem>();
        m_explosionsfx = GetComponent<AudioSource>();
        m_triggersfx = GetComponent<AudioSource>();
        m_explosionsfx.Pause();
        m_particles.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if ((m_Player.m_PlayerSteppedOnStrigger)&& !m_IsDetection && !m_targetHit)
        {
            m_BoxRenderer.material.color = Color.red;
        }

        if (m_targetHit)
        {
            if (m_remainTimer < HitTimer)
            {
                m_remainTimer += Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        var ColMesh = collision.gameObject.GetComponent<MeshFilter>();
        
        if (collision.gameObject.name == "Baseball(Clone)")
        {
            if ((!m_IsDetection) && m_Player.m_PlayerSteppedOnStrigger)
            {
                m_targetHit = true;
                m_BoxRenderer.material.color = Color.green;
                m_particles.Play();
                m_explosionsfx.Play();
            }
        }
        else if (collision.gameObject.name == "Player")
        {
            if (m_IsDetection)
            {
                if (!m_Player.m_PlayerSteppedOnStrigger)
                {
                    m_BoxRenderer.material.color = Color.cyan;
                    m_Player.m_PlayerSteppedOnStrigger = true;
                    m_triggersfx.Play();
                }
            }
        }
    }

    public bool getBoxHit()
    {
        return m_targetHit;
    }
}
