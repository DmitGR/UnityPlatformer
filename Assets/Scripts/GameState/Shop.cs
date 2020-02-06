using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    private const int maxHp = 5;
    private const float maxMp = 0.2f;
    private const int maxCoinsUp = 3;

    private const int HpCost = 25;
    private const int MpCost = 50;
    private const int CoinsUpCost = 75;

    private const int buffHp = 1;
    private const float buffMp = 0.05f;
    private const int buffCoin = 1;


    private Color block = new Color(0.6f, 0.6f, 0);

    [SerializeField]
    GameObject[] buttons;

    [SerializeField]
    Text[] Max;

    [SerializeField]
    Text Current;

    private int CurCoins;

    private void Awake()
    {
        Refresh();
    }

    public void Refresh()
    {
        CurCoins = PlayerPrefs.GetInt("Coins");
        Current.text = CurCoins.ToString();
        if (PlayerPrefs.GetInt("HP") < maxHp && CurCoins >= HpCost)
            buttons[0].GetComponentInChildren<Image>().color = Color.white;
        else
            buttons[0].GetComponentInChildren<Image>().color = block;
        if (PlayerPrefs.GetFloat("ManaBoost") < maxMp && CurCoins >= MpCost)
            buttons[1].GetComponentInChildren<Image>().color = Color.white;
        else
            buttons[1].GetComponentInChildren<Image>().color = block;
        if (PlayerPrefs.GetInt("CoinUp") < maxCoinsUp && CurCoins >= CoinsUpCost)
            buttons[2].GetComponentInChildren<Image>().color = Color.white;
        else
            buttons[2].GetComponentInChildren<Image>().color = block;
        Max[0].text = string.Format("{0} Of {1}", PlayerPrefs.GetInt("HP"), maxHp);
        Max[1].text = string.Format("{0} Of {1}", PlayerPrefs.GetFloat("ManaBoost") / buffMp, maxMp / buffMp);
        Max[2].text = string.Format("{0} Of {1}", PlayerPrefs.GetInt("CoinUp"), maxCoinsUp);


    }

    public void Exit()
    {
        var temp = PlayerPrefs.GetInt("Level") + 1;
        if (temp > PlayerPrefs.GetInt("MaxLevel") || PlayerPrefs.GetInt("Level") == 0)
            SceneManager.LoadScene("MainMenu");
        else
            SceneManager.LoadScene("Level_" + temp);
    }
    
    public void BuyHp()
    {
        if (PlayerPrefs.GetInt("HP") < maxHp && CurCoins >= HpCost)
        {
            PlayerPrefs.SetInt("HP", PlayerPrefs.GetInt("HP") + buffHp);
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - HpCost);
        }
        Refresh();

    }

    public void BuyMp()
    {
        Debug.Log("MP: " + PlayerPrefs.GetFloat("ManaBoost"));
        if (PlayerPrefs.GetFloat("ManaBoost") < maxMp && CurCoins >= MpCost)
        {
            PlayerPrefs.SetFloat("ManaBoost", (PlayerPrefs.GetFloat("ManaBoost") + buffMp));
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - MpCost);
        }
        Refresh();

    }


    public void CoinsUp()
    {
        if (PlayerPrefs.GetInt("CoinUp") < maxCoinsUp && CurCoins >= CoinsUpCost)
        {
            PlayerPrefs.SetInt("CoinUp", PlayerPrefs.GetInt("CoinUp") + buffCoin);
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - CoinsUpCost);
        }
        Refresh();

    }
}
