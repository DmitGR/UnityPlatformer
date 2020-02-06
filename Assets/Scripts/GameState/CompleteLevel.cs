using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompleteLevel : MonoBehaviour {

    [SerializeField]
    GameObject panel;

    [SerializeField]
    GameObject StarPanel;

    [SerializeField]
    GameObject[] stars;

    [SerializeField]
    Text score;

    [SerializeField]
    Text highscore;


    private Character unit;

    private void Awake()
    {
        panel.SetActive(false);
        unit = FindObjectOfType<Character>();
    }

    public void Completed()
    {
        GetComponent<AudioSource>().Play();

        panel.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            stars[i].SetActive(false);
        }
        for (int i = 0; i < unit.Stars; i++)
        {
            stars[i].SetActive(true);
        }
        Time.timeScale = 0;
        if (PlayerPrefs.GetInt("Level") < PlayerPrefs.GetInt("MaxLevel"))
            PlayerPrefs.SetInt("OpenedLevel", PlayerPrefs.GetInt("Level") + 1);

        int temp = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt("Coins", temp + unit.Coins);

        string key = SceneManager.GetActiveScene().name + "stars";
        if (PlayerPrefs.GetInt(key) < unit.Stars)
            PlayerPrefs.SetInt(key, unit.Stars);

        Debug.Log("Stars: "+PlayerPrefs.GetInt(key));
        PlayerPrefs.SetInt("HP", unit.Lives);

        score.text = "Score \n" + unit.Score;
        key = "Score" + PlayerPrefs.GetInt("Level");
        int highS = PlayerPrefs.GetInt(key);
        highscore.text = "HighScore \n" + highS;
        if (unit.Score > highS)
            PlayerPrefs.SetInt(key, unit.Score);

        StarPanel.SetActive(false);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Shop");
        Time.timeScale = 1;
    }
}
