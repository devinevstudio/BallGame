using Unity.VisualScripting;
using UnityEngine;

public class BumperScript : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private bool _animating;
    float vel = 5;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        _spriteRenderer.size = new Vector2(1, 1);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_animating)
        {
            if (_spriteRenderer.size.x != 1 && _spriteRenderer.size.y != 1)
            {
                ResetSpriteScale();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.GetComponent<Rigidbody2D>() != null)
        {
            Vector2 scaled = new Vector2(1.1F, 1.1F);
            _spriteRenderer.size = scaled;
            _animating = true;
            vel = 5;
        }
    }

    void ResetSpriteScale()
    {
        Vector2 size = _spriteRenderer.size;
        size.x = Mathf.SmoothDamp(size.x, 1, ref vel, 0.8F);
        size.y = Mathf.SmoothDamp(size.y, 1, ref vel, 0.8F);
        _spriteRenderer.size = size;
    }

}
