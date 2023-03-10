using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GrayBox");
        //SceneManager.UnloadScene("MainMenu");            ???idk
        Debug.Log("Scene Change");
    }

    public void EndGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
