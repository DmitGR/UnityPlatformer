using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public GameObject SettPanel;
    public GameObject BtnPanel;
    public GameObject SelectLvlPanel;
    private void Awake()
    {
       BackToMenu();
      PlayerPrefs.SetInt("Level", 0);
    }

    public void StartGame()
    {
        BtnPanel.SetActive(false);
        SelectLvlPanel.SetActive(true);
        GetComponent<AudioSource>().Play();
    }

    public void Settings()
    {
        BtnPanel.SetActive(false);
        SettPanel.SetActive(true);
        GetComponent<AudioSource>().Play();
    }

    public void BackToMenu()
    {
        BtnPanel.SetActive(true);
        SettPanel.SetActive(false);
        SelectLvlPanel.SetActive(false);
        GetComponent<AudioSource>().Play();
    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void LoadLevel(int Level)
    {
        SceneManager.LoadScene("Level_"+Level);
    }

    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("HP", 5);
        PlayerPrefs.SetInt("CoinUp", 1);
        PlayerPrefs.SetInt("MaxLevel", 6);
        PlayerPrefs.SetInt("OpenedLevel", 1);
        GetComponent<AudioSource>().Play();

    }
    public void Unlock()
    {
        PlayerPrefs.SetInt("HP", 5);
        PlayerPrefs.SetInt("CoinUp", 1);
        PlayerPrefs.SetInt("Coins", 500);
        PlayerPrefs.SetInt("MaxLevel", 6);
        PlayerPrefs.SetInt("OpenedLevel", 6);
        GetComponent<AudioSource>().Play();

    }

    public void Exit()
    {
        Application.Quit();
    }
}
