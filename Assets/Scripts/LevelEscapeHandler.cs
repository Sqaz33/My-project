using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEscapeHandler : MonoBehaviour
{
    [SerializeField] private string _mainMenuScene = "MainMenu";
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainMenu();
        }
    }
    
    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(_mainMenuScene);
    }

}