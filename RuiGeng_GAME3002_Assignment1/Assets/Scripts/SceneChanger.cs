using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private bool isPause = false;
    private AudioSource m_buttonSFX;
    public void openScene(string s)
    {
        if (s == "QUIT")
        {
            Application.Quit();
        }
        else if (s == "PauseScene")
        {
            isPause = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene("PauseScene", LoadSceneMode.Additive);
        }
        else if (s == "Unpause")
        {
            if (isPause)
            {
                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("PauseScene");
                isPause = false;
            }
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(s, LoadSceneMode.Single);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        m_buttonSFX = GetComponent<AudioSource>();
        m_buttonSFX.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound()
    {
        m_buttonSFX.Play();
    }
}
