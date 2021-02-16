using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBehaviour : MonoBehaviour
{
    public Text m_Text;
    public PlayerBehaviour m_Player;
    public List<TargetBoxes> m_BoxList;
    public char m_TextType;

    private int m_BoxNum;
    // Start is called before the first frame update
    void Start()
    {
        m_Text = GetComponent<Text>();

        m_BoxNum = m_BoxList.Count;

        Debug.Log(m_BoxNum);
        Debug.Log(m_BoxList.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_TextType == 'b') //For Box Num
        {
            updateTarget();
            m_Text.text = "Targets Left: " + m_BoxNum;
            if (m_BoxNum == 0)
            {
                m_Player.m_LevelCompleted = true;
            }
        }
        else if (m_TextType == 'p') //For player's baseball left
        {
            m_Text.text = "Baseballs Left: " + m_Player.getBaseballLeft();
        }
    }

    private void updateTarget()
    {
        for (int i = 0; i < m_BoxList.Count; i++)
        {
            if (m_BoxList[i] != null)
            {
                if (m_BoxList[i].getBoxHit())
                {
                    if (!m_BoxList[i].m_hitCounted)
                    {
                        m_BoxNum -= 1;
                        m_BoxList[i].m_hitCounted = true;
                    }
                }
            }
        }
    }
}
