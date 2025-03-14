using System;
using UnityEngine;

public class WorldScript : MonoBehaviour
{

    bool game;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        game = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        if (!game) { game = true;return; }
        Debug.Log("Game's already running.");
    }

    public void StopGame()
    {
        if (game) { game = false; return; }
        Debug.Log("Game's not running.");
    }

    public bool isGameRunning() => game;
}
