using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private TileMap tileMap;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        tileMap = GameObject.FindObjectOfType<TileMap>();
    }

    public void Increase()
    {
        tileMap.Increase();
    }
}
