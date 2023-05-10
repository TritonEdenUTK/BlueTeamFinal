using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Function for the big PLAY button on the main menu
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load the main menu build index + 1
    }
    // Function for the big QUIT button on the main menu
    public void QuitGame()
    {
        Application.Quit();
    }
}
