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
    private bool _isGrounded;
    private bool _finishedGame = false;
    //private float _inputDamping = 1F;

    private HashSet<Collision2D> _currentCollisions;

    public bool Finished { get { return _finishedGame;  } set { _finishedGame = value; } }
    public bool Grounded { get { return _isGrounded; } }
    //public float InputDamping { get { return _inputDamping; } }

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _currentCollisions = new HashSet<Collision2D>();
        //worldScript = FindFirstObjectByType<WorldScript>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_isGrounded)
        {
            if(_currentCollisions.Count <= 0)
            {
                _isGrounded = false;
            }
        }
    }

    public int CollisionCount { get { return _currentCollisions.Count; } }
    public HashSet<Collision2D> Collisions { get { return _currentCollisions; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            _finishedGame = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        _currentCollisions.Add(collision);
    }
    void OnCollisionExit2D(Collision2D collision) => _currentCollisions.Remove(collision); 

    void OnCollisionStay2D(Collision2D collision)
    {
        ContactPoint2D strongestContact = collision.contacts[0];
        float maxForce = strongestContact.normalImpulse;
        foreach (var contact in collision.contacts)
        {

            if(contact.normalImpulse > maxForce)
            {
                strongestContact = contact;
                maxForce = contact.normalImpulse;
            }
        }


        float angle = Vector2.Angle(Vector2.up, strongestContact.normal);
        if (Mathf.Abs(angle) < 80.0F)
        {
            _isGrounded = true;

        }


        //TODO: Implement Speed Dumping when Uphill moving!

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
