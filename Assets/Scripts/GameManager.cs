using UnityEngine;
using TMPro; //for textMeshPro elements


public class GameManager : MonoBehaviour
{
    public GameObject spritePrefab;
    public Transform computerCanvas;
    public Transform playerCanvas;
    public Sprite[] sprites;
    public int currentLevel = 0;

    private GameObject[,] playerSprites;
    private GameObject[,] computerSprites;
    public GameObject selectedSprite;  // track the selected/clicked sprite on the player's canvas
    public UIManager uiManager;
    public TextMeshProUGUI timerText;
    public GameObject losePanel;
    public float timeLeft = 50.0f; // 50 seconds for the start of level 4 (5x5 grid)


    // Start is called before the first frame update
    void Start()
    {
        SetupLevel(currentLevel);
    }

    void SetupLevel(int level)
    {
        ClearSprites(); // clear canvas of old sprites from previous level grid

        int numSpritesX, numSpritesY; // number of sprites in x and y axis
        switch (level)
        {
            case 0: numSpritesX = numSpritesY = 1; break; //  sprites in a 1x1 grid
            case 1: numSpritesX = 2; numSpritesY = 1; break; // 2x1 grid
            case 2: numSpritesX = numSpritesY = 2; break; // 2x2 grid
            case 3: numSpritesX = numSpritesY = 4; break; // 4x4 grid
            case 4: numSpritesX = numSpritesY = 5; break; // 5x5 grid
            case 5: numSpritesX = numSpritesY = 6; break; // 6x6 grid
            case 6: numSpritesX = numSpritesY = 8; break; // 8x8 grid
            default: return; // Exit if the level is incorrectly specified
        }

        float canvasWidth = computerCanvas.GetComponent<RectTransform>().rect.width; //get size of canvas for sprite display
        float canvasHeight = computerCanvas.GetComponent<RectTransform>().rect.height;

        float spriteWidth = canvasWidth / numSpritesX;
        float spriteHeight = canvasHeight / numSpritesY;

        playerSprites = new GameObject[numSpritesX, numSpritesY];
        computerSprites = new GameObject[numSpritesX, numSpritesY];

        for (int i = 0; i < numSpritesX; i++)
        {
            for (int j = 0; j < numSpritesY; j++)
            {
                float x = (i + 0.5f) * spriteWidth - (canvasWidth / 2);
                float y = (j + 0.5f) * spriteHeight - (canvasHeight / 2);

                Vector3 position = new Vector3(x, y, -0.1f); // put sprites in front of the canvas

                // instantiate sprites for computer and player canvases
                InstantiateAndScaleSprites(position, spriteWidth, spriteHeight, i, j);
            }
        }
    }

    // sprite instantiation method
    void InstantiateAndScaleSprites(Vector3 position, float width, float height, int i, int j)
    {
        GameObject compSprite = Instantiate(spritePrefab, computerCanvas);
        compSprite.transform.localPosition = position;
        compSprite.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        compSprite.transform.localScale = new Vector3(width / compSprite.GetComponent<SpriteRenderer>().bounds.size.x, height / compSprite.GetComponent<SpriteRenderer>().bounds.size.y, 1);

        GameObject playerSprite = Instantiate(spritePrefab, playerCanvas);
        playerSprite.transform.localPosition = position;

        // start with the first sprite
        playerSprite.GetComponent<SpriteRenderer>().sprite = sprites[0];
        playerSprite.transform.localScale = new Vector3(width / playerSprite.GetComponent<SpriteRenderer>().bounds.size.x, height / playerSprite.GetComponent<SpriteRenderer>().bounds.size.y, 1);

        computerSprites[i, j] = compSprite;
        playerSprites[i, j] = playerSprite;
    }




    // method to set the selected sprite based on grid position
    public void SelectSprite(GameObject sprite)
    {
        if (selectedSprite != null)
            selectedSprite.GetComponent<SpriteRenderer>().color = Color.white; // deselect by resetting color

        selectedSprite = sprite;
        selectedSprite.GetComponent<SpriteRenderer>().color = Color.yellow; // highlight the selected sprite
        Debug.Log("Selected sprite: " + selectedSprite.name);
    }



    // method to change the sprite of the selected object
    public void ChangeSelectedSprite(int spriteIndex)
    {
        if (selectedSprite != null)
        {
            selectedSprite.GetComponent<SpriteRenderer>().sprite = sprites[spriteIndex];
            CheckForWin();
        }
    }

    //method to check if the sprite change leads to a win state
    public void CheckForWin()
    {
        int numRows = playerSprites.GetLength(0); // number of rows in the array
        int numCols = playerSprites.GetLength(1); // number of columns in the array

        bool allMatched = true;
        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                if (playerSprites[i, j].GetComponent<SpriteRenderer>().sprite != computerSprites[i, j].GetComponent<SpriteRenderer>().sprite)
                {
                    allMatched = false;
                    break;
                }
            }
            if (!allMatched) break;
        }

        if (allMatched)
        {
            currentLevel++;
            if (currentLevel <= 4)
            {
                ClearSprites();  // clear sprites from both canvases
                SetupLevel(currentLevel);
            }
            else
            {
                uiManager.ShowGameCompletePanel(); //display win panel
                Debug.Log("Game Completed!");
            }
        }
    }


    // method to clear previous sprites
    void ClearSprites()
    {
        Debug.Log("clearing sprites from computer canvas...");
        foreach (Transform child in computerCanvas)
            Destroy(child.gameObject);

        Debug.Log("clearing sprites from player canvas...");
        foreach (Transform child in playerCanvas)
            Destroy(child.gameObject);
    }

    void Update()
    {
        //start timer when game reaches level 4 to add difficulty
        if (currentLevel == 4)  
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                timerText.text = Mathf.RoundToInt(timeLeft).ToString();
            }
            else
            {
                timeLeft = 0;
                timerText.text = "0";
                GameOver();
            }

        void GameOver()
        {
            // display panel and end game
            losePanel.SetActive(true);
           
        }
    }
}
    }

