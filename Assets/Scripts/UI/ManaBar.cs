using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour {

    private Image circle;

    //  private Character character;

    private const float maxFill = 1f;
    private const float fill = 0.05f;

    private float currentValue;

    public float CurrentValue
    {
        get { return currentValue; }
        set { if (value <= maxFill && value >= 0) currentValue = value; }
    }


    private void Awake()
    {
        circle = GetComponent<Image>();
        currentValue = maxFill;
    }


    private void Update()
    {
        circle.fillAmount = currentValue;
        CurrentValue += (fill + PlayerPrefs.GetFloat("ManaBoost")) * Time.deltaTime;
    }


}
