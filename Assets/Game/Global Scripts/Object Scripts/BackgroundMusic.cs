using Unity.VisualScripting;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{

    public static BackgroundMusic Instance { get; private set; }

    private AudioSource _audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            if(_audioSource == null)
            {
                _audioSource = GetComponent<AudioSource>();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
            _audioSource.loop = true;
            _audioSource.volume = 0.25F;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
