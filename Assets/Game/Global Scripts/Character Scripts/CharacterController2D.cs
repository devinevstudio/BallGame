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

    private Vector2 velocityBuffer;

    private HashSet<Collision2D> _currentCollisions;

    public bool Grounded { get { return _isGrounded; } }

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Finish")
        {

        }
        else { 
            _currentCollisions.Add(collision);
        }
    }
    void OnCollisionExit2D(Collision2D collision) => _currentCollisions.Remove(collision); 

    void OnCollisionStay2D(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            float angle = Vector2.Angle(Vector2.up, contact.normal);
            if (Mathf.Abs(angle) < 80.0F)
            {
                _isGrounded = true;
            }
        }
    }
}
