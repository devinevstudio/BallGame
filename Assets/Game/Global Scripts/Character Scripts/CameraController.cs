using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{

    [SerializeField] GameObject PlayerObject;
    WorldScript _world;
    private Camera _orthoGraphicCamera;

    private Rect _bounds;

    private bool _zoomedOut = false;
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
        Vector3 newPos = new Vector3(PlayerObject.transform.position.x, PlayerObject.transform.position.y, transform.position.z);


    }

    public void UpdateZoom()
    {
        _orthoGraphicCamera.orthographicSize = 10 + (Convert.ToInt32(ZoomedOut) * 20F);
    }
}
