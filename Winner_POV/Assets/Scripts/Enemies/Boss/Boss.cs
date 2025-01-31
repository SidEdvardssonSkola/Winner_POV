using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : Enemy
{
    public void LoadVictoyScreen()
    {
        SceneManager.LoadScene("Victory Scene");
    }
}
