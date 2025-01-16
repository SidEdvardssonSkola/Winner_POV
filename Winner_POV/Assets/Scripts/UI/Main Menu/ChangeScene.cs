using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void LoadScene(int SceneID)
    {
        SceneManager.LoadScene(SceneID);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
