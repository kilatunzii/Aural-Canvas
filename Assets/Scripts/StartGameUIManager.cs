using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameUIManager : MonoBehaviour
{

    public GameObject howToPlayPanel;


    // Start is called before the first frame update
    void Start()
    {
        howToPlayPanel.SetActive(false); //disable panel at start of game
    }

    public void howToPlay()
    {
        howToPlayPanel.SetActive(true);
    }

    public void StartGame()
    {
        //load level scene
        SceneManager.LoadScene("level");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
