using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILose : UICanvas
{

    public void GoHome()
    {
        Character.OutPlayGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
