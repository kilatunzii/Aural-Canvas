using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject hintPanel;
    public GameObject menuPanel;
    public GameObject gameCompletePanel;

    // Start is called before the first frame update
    void Start()
    {
        //hide the panels at start
        hintPanel.SetActive(false);
        menuPanel.SetActive(false);
        gameCompletePanel.SetActive(false);
        
    }

    public void ToggleHintPanel()
    {
        hintPanel.SetActive(!hintPanel.activeSelf);
    }

    public void ToggleMenuPanel()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
    }

    public void ShowGameCompletePanel()
    {
        gameCompletePanel.SetActive(true);
    }
    public void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
