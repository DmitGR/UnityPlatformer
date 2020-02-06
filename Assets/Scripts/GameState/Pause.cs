using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    [SerializeField]
    GameObject pausePanel;

    private void Awake()
    {
        pausePanel.SetActive(false);
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Paused();
    }

    public void Paused()
    {
        AudioListener.pause = true;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void UnPaused()
    {
       AudioListener.pause = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        UnPaused();
        SceneManager.LoadScene("MainMenu");
    }
}
