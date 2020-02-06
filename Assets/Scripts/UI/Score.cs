using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

       private Text text;
    private Character character;

    private void Awake()
    {
        character = FindObjectOfType<Character>();
        text = GetComponentInChildren<Text>();
        Refresh();
    }

    public void Refresh()
    {
        text.text = string.Format("Score: {0}", character.Score);
    }
}
