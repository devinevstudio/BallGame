using Unity.VisualScripting;
using UnityEngine;

public class BoosterTriggerScript : MonoBehaviour
{

    //TRIGGER WITH THIS SCRIPT A ONE DIRECTIONAL! EVERY OBJECT ENTERING THIS TRIGGER WILL BE BOOSTED IN A SPECIFIC DIRECTION

    private BoxCollider2D _boxCollider;
    private Rigidbody2D _BoostObject;
    private Vector2 _boostVector;
    [SerializeField] float _boostStrength = 1;

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Accelerate Collider in Direction specified
    }
}
