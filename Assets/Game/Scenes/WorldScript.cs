using System;
using UnityEngine;

public class WorldScript : MonoBehaviour
{

    [SerializeField] GameObject WorldBoundsObject;

    private Rect _worldBounds;

    public Rect WorldBounds { get { return _worldBounds; } }

    private void Awake()
    {
        if(WorldBoundsObject != null)
        {
            _worldBounds.xMin = WorldBoundsObject.transform.position.x - (WorldBoundsObject.transform.localScale.x / 2);
            _worldBounds.xMax = WorldBoundsObject.transform.position.x + (WorldBoundsObject.transform.localScale.x / 2);
            _worldBounds.yMin = WorldBoundsObject.transform.position.y - (WorldBoundsObject.transform.localScale.y / 2);
            _worldBounds.yMax = WorldBoundsObject.transform.position.y + (WorldBoundsObject.transform.localScale.y / 2);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
