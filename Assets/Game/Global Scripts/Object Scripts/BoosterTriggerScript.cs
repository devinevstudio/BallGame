using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BoosterTriggerScript : MonoBehaviour
{

    //TRIGGER WITH THIS SCRIPT A ONE DIRECTIONAL! EVERY OBJECT ENTERING THIS TRIGGER WILL BE BOOSTED IN THE LOCAL POSITIVE Y DIRECTION OF THIS OBJECT

    private BoxCollider2D _boxCollider;
    private Vector2 _direction;
    private float _rotation;
    [SerializeField] float _boostStrength = 1;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _rotation = transform.eulerAngles.z;
        _direction = Vector2.up;
        ReCalculateDirection();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if (_rotation != transform.eulerAngles.z) {
            ReCalculateDirection();
        }
    }

    private void ReCalculateDirection()
    {
        //_direction
        _rotation = transform.eulerAngles.z;
        float rotation = _rotation*Mathf.Deg2Rad;
        _direction = new Vector2((Mathf.Cos(rotation) * Vector2.up.x) - (Mathf.Sin(rotation) * Vector2.up.y), (Mathf.Sin(rotation) * Vector2.up.x) + (Mathf.Cos(rotation) * Vector2.up.y)); //Rotating Vector using RotationMatrix
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Accelerate Collider in Direction specified
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            if (rb.linearVelocity.magnitude >= 0.125F)
            {
                rb.linearVelocity += (_direction * _boostStrength);
                Debug.DrawLine(rb.position, rb.position + _direction, Color.green);
            }
        }
    }

    private void rotateTrigger(float speed)
    {
        transform.Rotate(0, 0, Mathf.Abs(speed)); 
    }
}
