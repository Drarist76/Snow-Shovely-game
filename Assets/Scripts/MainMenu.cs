using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        SceneManager.UnloadScene("MainMenu");
        Debug.Log("Scene");
    }

    public void EndGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
