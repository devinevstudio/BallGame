using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{

    [SerializeField] GameObject PlayerObject;
    [SerializeField] GameObject _debug;
    WorldScript _world;
    private Camera _orthoGraphicCamera;

    private Rect _bounds;

    private bool _zoomedOut;
    public bool ZoomedOut { get { return _zoomedOut; } set { _zoomedOut = value; } }


    private void Awake()
    {
        _orthoGraphicCamera = GetComponent<Camera>();
        _world = GameObject.Find("WorldScript").GetComponent<WorldScript>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateZoom();
        _bounds = _world.WorldBounds;
    }

    // Update is called once per frame
    void Update()
    {
        RestrictCamera();
    }

    public void RestrictCamera()
    {
        float newX = PlayerObject.transform.position.x;
        float newY = PlayerObject.transform.position.y;
        if (!ZoomedOut)
        {
            Vector2 bottomLeft = _orthoGraphicCamera.ViewportToWorldPoint(new Vector3(0, 0));
            Vector2 topRight = _orthoGraphicCamera.ViewportToWorldPoint(new Vector3(1, 1));
            float diffX = (topRight.x - bottomLeft.x) / 2;
            float diffY = (topRight.y - bottomLeft.y) / 2;

            newX = Mathf.Clamp(newX, _bounds.xMin + diffX, _bounds.xMax - diffX);
            newY = Mathf.Clamp(newY, _bounds.yMin + diffY, _bounds.yMax - diffY);
        }
        else
        {
            newX = 0;
        }
        _orthoGraphicCamera.transform.position = new Vector3(newX, newY, -10);
    }

    public void UpdateZoom()
    {
        float size = ZoomedOut ? 56.25F : 15F;
        _orthoGraphicCamera.orthographicSize = size;
    }
}
