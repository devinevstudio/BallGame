using Unity.VisualScripting;
using UnityEngine;

public class BumperScript : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;
    private Vector2 _spriteSize;
    private bool _animating;
    float velX = 5;
    float velY = 5;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        _audioSource = GetComponent<AudioSource>();
        _spriteSize = _spriteRenderer.size;
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
        if(collision.collider.GetComponent<Rigidbody2D>().CompareTag("Player"))
        {
            Vector2 scaled = new Vector2(1.025F, 1.025F);
            _spriteRenderer.size *= scaled;
            _animating = true;
            velX = 5;
            velY = 5;
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }

        }
    }

    void ResetSpriteScale()
    {
        Vector2 size = _spriteRenderer.size;
        size.x = Mathf.SmoothDamp(size.x, _spriteSize.x, ref velX, 0.2F);
        size.y = Mathf.SmoothDamp(size.y, _spriteSize.y, ref velY, 0.2F);
        _spriteRenderer.size = size;
    }

}
