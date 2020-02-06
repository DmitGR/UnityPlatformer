using UnityEngine;
using UnityEngine.UI;

public class LevelCotrol : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer[] stars;

    [SerializeField]
    private GameObject[] levels;

    [SerializeField]
    private GameObject boss;

    private const int maxStars = 3;
    private Color block = new Color(0.6f, 0.6f, 0.6f);
    private void Awake()
    {
        OpenFirstLevel();
        Refresh();
    }

    public void Refresh()
    {
        for (int j = 1; j <= stars.Length / maxStars; j++)
        {
            string key = string.Format("Level_{0}stars", j);
            int count = PlayerPrefs.GetInt(key);

            for (int i = 0; i < count; i++)
            {
                stars[i + (j - 1) * maxStars].color = Color.white;
            }
        }

        for (int i = 0; i < PlayerPrefs.GetInt("OpenedLevel"); i++)
        {
            levels[i].GetComponent<Button>().interactable = true;
            levels[i].GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
        if (PlayerPrefs.GetInt("OpenedLevel") >= 2)
        {
            boss.GetComponent<Button>().interactable = true;
            boss.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
    }

    public void _Reset()
    {
        foreach (var item in stars)
        {
            item.color = block;
        }
        foreach (var item in levels)
        {
            item.GetComponent<Button>().interactable = false;
            item.GetComponentInChildren<SpriteRenderer>().color = block;
        }
    }

    private void OpenFirstLevel()
    {
        levels[0].GetComponent<Button>().interactable = true;
        levels[0].GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
}
