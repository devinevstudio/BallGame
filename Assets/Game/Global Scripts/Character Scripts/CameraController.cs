using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{

    [SerializeField] GameObject PlayerObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(PlayerObject.transform.position.x, PlayerObject.transform.position.y, transform.position.z);
        transform.position = newPosition;
    }
}
