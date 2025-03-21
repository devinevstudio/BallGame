using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController2D : MonoBehaviour
{

    //private WorldScript worldScript;
    private Rigidbody2D _rigidBody;
    private CircleCollider2D _circleCollider;
    private bool _isGrounded;
    private bool _finishedGame = false;

    [SerializeField] float _maxSpeed = 30.0F;

    public bool Finished { get { return _finishedGame;  } set { _finishedGame = value; } }
    public bool Grounded { get { return _isGrounded; } }
    //public float InputDamping { get { return _inputDamping; } }

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RestrictSpeed();
        GroundCheck();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            _finishedGame = true;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {


        //TODO: Implement Speed Dumping when Uphill moving!

    }

    public void RestrictSpeed()
    {
        Vector2 currentVelocity = _rigidBody.linearVelocity;
        if(currentVelocity.magnitude > _maxSpeed)
        {
            _rigidBody.linearDamping = 0.3F;
        }
        _rigidBody.linearDamping = 0F;
    }

    public void GroundCheck()
    {
        _isGrounded = false;
        ContactPoint2D[] contacts = new ContactPoint2D[10];
        int count = _circleCollider.GetContacts(contacts);
        if(count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                ContactPoint2D contact = contacts[i];
                float angle = Vector2.Angle(Vector2.up, contact.normal);
                if (Mathf.Abs(angle) < 80.0F)
                {
                    _isGrounded = true;
                    return;
                }
            }
        }


    }

    public void ResetPosition()
    {
        _rigidBody.linearVelocity = new Vector2(0, 0);
        _rigidBody.angularVelocity = 0;
        _rigidBody.transform.position = new Vector2(0, 0);
        _rigidBody.rotation = 0;

    }

    public void WaitForStart()
    {
        float speed = 1.25F;
        float posX = _rigidBody.transform.position.x;
        float angle = Time.realtimeSinceStartup * speed % 360;
        posX = Mathf.Sin(angle) * 5.0F;
        Vector2 newVec = new Vector2(posX, 0);
        _rigidBody.transform.position = newVec;
    }
}
