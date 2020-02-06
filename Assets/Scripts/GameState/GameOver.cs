using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    [SerializeField]
    private GameObject panel;

    private Character unit;

    private void Awake()
    {
        panel.SetActive(false);
        unit = FindObjectOfType<Character>();
    }

    public void Over()
    {
        panel.SetActive(true);
        GetComponent<AudioSource>().Play();
        Time.timeScale = 0;
    }

    public void ToMenu()
    {
        ClearData();
        panel.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("HP", 5);
        PlayerPrefs.SetInt("CoinUp", 1);
        PlayerPrefs.SetInt("Coins", 50);
        PlayerPrefs.SetInt("MaxLevel", 6);
        PlayerPrefs.SetInt("OpenedLevel", 1);
    }
}
