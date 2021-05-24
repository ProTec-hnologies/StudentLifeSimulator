using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesChanger : MonoBehaviour
{
    public static void ChangeLevel(string sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public static void ExitGame()
    {
        Application.Quit();
    }
}
