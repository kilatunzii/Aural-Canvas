using UnityEngine;
using UnityEngine.UI;

public class SpriteChanger : MonoBehaviour
{
    public GameManager gameManager;
    public Button[] spriteButtons;  
    public AudioClip[] buttonSounds; 
    private AudioSource audioSource;
    public float soundVolume = 0.05f;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); 
        audioSource.volume = soundVolume;

        // ensure spriteButtons correspond to specific sprites
        for (int i = 0; i < spriteButtons.Length; i++)
        {
            int index = i;
            spriteButtons[i].onClick.AddListener(() => {
                PlayButtonSound(index);
                ChangeSelectedSprite(index);
            });
        }
    }

    //method when sprite button is pressed
    void ChangeSelectedSprite(int spriteIndex)
    {
        if (gameManager.selectedSprite != null)
        {
            gameManager.selectedSprite.GetComponent<SpriteRenderer>().sprite = gameManager.sprites[spriteIndex];
            // check for win condition after changing sprite
            gameManager.CheckForWin();  
        }
    }

    //play sound associated with the button
    void PlayButtonSound(int buttonIndex)
    {
        if (buttonSounds[buttonIndex] != null)
        {
            audioSource.clip = buttonSounds[buttonIndex];
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Audio clip for button index " + buttonIndex + " is not assigned!");
        }
    }
}
