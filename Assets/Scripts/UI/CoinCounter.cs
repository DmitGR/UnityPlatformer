using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour {

    private Text text;
    private Character character;

    private void Awake()
    {
        character = FindObjectOfType<Character>();
        text = GetComponent<Text>();
        Refresh();
    }

    public void Refresh()
    {
        text.text = string.Format("X {0}",(PlayerPrefs.GetInt("Coins") + character.Coins));
    }

}