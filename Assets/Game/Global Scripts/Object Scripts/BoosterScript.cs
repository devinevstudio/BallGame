using Unity.VisualScripting;
using UnityEngine;

public class BoosterScript : MonoBehaviour
{

    private BoxCollider2D _boxCollider;
    private Rigidbody2D _BoostObject;
    private Vector2 _boostVector;
    private float _boostMultiplier = 1;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _boostVector = new Vector2(1,1);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Vector2 boostVector = new Vector2();
        ContactPoint2D conPoint = collision.contacts[0];
        _BoostObject = collision.rigidbody;
        Vector2 boostNormal = conPoint.normal;
        Vector2 tangent = new Vector2(-boostNormal.y, boostNormal.x);
        Vector2 check = _BoostObject.linearVelocity * boostNormal;
        float angle = Vector2.Angle(check, tangent);
        if (Mathf.Abs(angle) < 90)
        {
            angle = Vector2.Angle(boostNormal, tangent);
            angle *= Mathf.Deg2Rad;
            _boostVector = new Vector2((Mathf.Cos(angle) * boostNormal.x) - (Mathf.Sin(angle) * boostNormal.y), (Mathf.Sin(angle) * boostNormal.x) + (Mathf.Cos(angle) * boostNormal.y));
            _boostMultiplier = 2.5F;
        }
        else if (Mathf.Abs(angle) > 90)
        {
            angle = Vector2.Angle(boostNormal, tangent);
            angle -= 180;
            angle *= Mathf.Deg2Rad;
            _boostVector = new Vector2((Mathf.Cos(angle) * boostNormal.x) - (Mathf.Sin(angle) * boostNormal.y), (Mathf.Sin(angle) * boostNormal.x) + (Mathf.Cos(angle) * boostNormal.y));
            _boostMultiplier = 2.5F;
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (_BoostObject == collision.rigidbody)
        {
            _BoostObject.linearVelocity += (_boostVector * _boostMultiplier);
            Debug.Log(_boostVector + " " + _boostMultiplier);
            Debug.Log(_BoostObject.linearVelocity * (_boostVector * _boostMultiplier));
            Debug.DrawLine(_BoostObject.transform.position, _BoostObject.transform.position + new Vector3(_BoostObject.linearVelocity.x, _BoostObject.linearVelocity.y, 0), Color.red);
        }
    }
}
