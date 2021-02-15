using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private bool isPause = false;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
