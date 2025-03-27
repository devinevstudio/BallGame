using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{

    [SerializeField] GameObject PlayerObject;
    WorldScript _world;
    private Camera _orthoGraphicCamera;
    private Vector2 _currentPixelRect;

    private Rect _bounds;

    private bool _zoomedOut;
    public bool ZoomedOut { get { return _zoomedOut; } set { _zoomedOut = value; } }

    ///*

    //        Standard Resolution is 1920x1080
    //        And the standard Camera Size will be 6F

    // */
    //private void calculateSize()
    //{
    //    float defaultWidth = 1920;
    //    float ratio = 0F;
    //    float actualWidth = _orthoGraphicCamera.pixelWidth;
    //    ratio = defaultWidth / actualWidth;
    //    Debug.Log(ratio);
    //    _orthoGraphicCamera.orthographicSize = 6F * ratio;
    //    _currentPixelRect = new Vector2(_orthoGraphicCamera.pixelWidth, _orthoGraphicCamera.pixelHeight);
    //}

    private void Awake()
    {
        _orthoGraphicCamera = Camera.main;
        _world = GameObject.Find("WorldScript").GetComponent<WorldScript>();
        _currentPixelRect = new Vector2(_orthoGraphicCamera.pixelWidth, _orthoGraphicCamera.pixelHeight);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateZoom();
        _bounds = _world.WorldBounds; 
        //calculateSize();
    }

    // Update is called once per frame
    void Update()
    {
        RestrictCamera();
        //if(_currentPixelRect.x != _orthoGraphicCamera.pixelWidth || _currentPixelRect.y != _orthoGraphicCamera.pixelHeight)
        //{
        //    calculateSize();
        //}
    }

    public void RestrictCamera()
    {
        float newX = PlayerObject.transform.position.x;
        float newY = PlayerObject.transform.position.y;
        Vector2 bottomLeft = _orthoGraphicCamera.ViewportToWorldPoint(new Vector3(0, 0));
        Vector2 topRight = _orthoGraphicCamera.ViewportToWorldPoint(new Vector3(1, 1));
        float diffX = (topRight.x - bottomLeft.x) / 2;
        float diffY = (topRight.y - bottomLeft.y) / 2;

        newX = Mathf.Clamp(newX, _bounds.xMin + diffX, _bounds.xMax - diffX);
        newY = Mathf.Clamp(newY, _bounds.yMin + diffY, _bounds.yMax - diffY);
        _orthoGraphicCamera.transform.position = new Vector3(newX, newY, -10);
    }

    public void UpdateZoom()
    {
        float size = ZoomedOut ? 12.5F : 6F;
        _orthoGraphicCamera.orthographicSize = size;
    }
}
