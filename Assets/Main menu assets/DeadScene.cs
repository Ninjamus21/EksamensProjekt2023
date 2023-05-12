using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadScene : MonoBehaviour
{
public void Restart()
    {
        SceneManager.LoadScene("Dirt Pits");
    }
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
