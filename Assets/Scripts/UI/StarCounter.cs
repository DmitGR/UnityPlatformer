using UnityEngine;
using UnityEngine.UI;

public class StarCounter : MonoBehaviour
{
    [SerializeField]
    private Image[] stars;

    Character hero;

    private void Awake()
    {
        hero = FindObjectOfType<Character>();
    }

    public void Refresh()
    {
        for (int i = 0; i < hero.Stars; i++)
        {
            stars[i].color = Color.white;
        }
    }

}
