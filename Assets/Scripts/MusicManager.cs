using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public AudioClip backgroundMusic;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // make music persistent across scenes
            DontDestroyOnLoad(gameObject);  
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;  
            audioSource.playOnAwake = false;
            audioSource.volume = 0.25f;  // set music volume
            audioSource.Play();
        }
        else
        {
            //make sure only one music instance exists
            Destroy(gameObject);  
        }
    }
}
