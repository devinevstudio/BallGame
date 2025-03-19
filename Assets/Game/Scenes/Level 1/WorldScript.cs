using System;
using UnityEngine;

public class WorldScript : MonoBehaviour
{

    private bool _game;

    [SerializeField] GameObject WorldBoundsObject;

    private Rect _worldBounds;

    public bool GameStarted { get { return _game; } set { _game = value; } }
    public Rect WorldBounds { get { return _worldBounds; } }

    private void Awake()
    {
        if(WorldBoundsObject != null)
        {
            _worldBounds.xMin = 0 - (WorldBoundsObject.transform.localScale.x / 2);
            _worldBounds.xMax = 0 + (WorldBoundsObject.transform.localScale.x / 2);
            _worldBounds.yMin = 0 - (WorldBoundsObject.transform.localScale.y / 2);
            _worldBounds.yMax = 0 + (WorldBoundsObject.transform.localScale.y / 2);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _game = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        if (!_game) { _game = true; return; }
    }

    public void StopGame()
    {
        if (_game) { _game = false; return; }
    }

    public bool isGameRunning() => _game;
}
