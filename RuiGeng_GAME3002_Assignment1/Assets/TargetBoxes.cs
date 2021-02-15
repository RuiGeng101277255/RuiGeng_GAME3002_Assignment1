using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBoxes : MonoBehaviour
{
    public float HitTimer;
    private Renderer m_BoxRenderer;

    private bool m_targetHit = false;
    private float m_remainTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_BoxRenderer = GetComponent<Renderer>();
        m_BoxRenderer.material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (collision.gameObject.name == "BaseballBehaviour")
        {
            m_targetHit = true;
            m_BoxRenderer.material.color = Color.green;
        }
    }
}
