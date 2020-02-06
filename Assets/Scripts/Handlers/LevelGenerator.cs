using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [SerializeField]
    private Texture2D map;

    [SerializeField]
    private int level;

    public ColorToPrefab[] colorMappings;

    void Start()
    {
        PlayerPrefs.SetInt("Level",level);
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x,y);
            }
        }
    }

    void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        if (pixelColor.a == 0)
            return;  // Air

        foreach (var item in colorMappings)
        {
         //   Debug.Log("pixel: " + pixelColor);
            if (item.color.Equals(pixelColor))
            {
             //   Debug.Log("Eq");
                Vector2 position = new Vector2(x, y);
                Instantiate(item.prefab, position, Quaternion.identity, transform);
            }
        }
    }
}

