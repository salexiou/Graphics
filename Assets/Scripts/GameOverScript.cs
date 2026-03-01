using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{

    public Button button1;   
    public Button button2;   

    void Start()
    {
        if (button2 == null || button1 == null)
        {
            return;
        }

        button1.onClick.AddListener(LoadMainMenuScene);
        button2.onClick.AddListener(ExitApp);
    }

    public void ExitApp()
    {
        Application.Quit();
    }



    void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
