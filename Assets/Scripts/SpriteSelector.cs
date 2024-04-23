using UnityEngine;
using UnityEngine.EventSystems;

public class SpriteSelector : MonoBehaviour, IPointerClickHandler
{
    public GameManager gameManager;  

    private void Awake()
    {
        // assign gameManager
        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Sprite clicked: " + gameObject.name);
        // notify the GameManager about the selection
        gameManager.SelectSprite(gameObject); 
    }
}
