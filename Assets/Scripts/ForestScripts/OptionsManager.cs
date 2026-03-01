using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsManager : MonoBehaviour
{
    public GameObject obj;
 
    public Button button1;  
    public Button button2; 
 
    void Start()
    {
        if (obj == null || button1 == null || button2 == null)
        {
            return;
        }
        obj.SetActive(false);
        button1.onClick.AddListener(LoadMainMenuScene);
        button2.onClick.AddListener(CloseOpt);
    }

    public void CloseOpt()
    {
        obj.SetActive(false);
    }



    void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
