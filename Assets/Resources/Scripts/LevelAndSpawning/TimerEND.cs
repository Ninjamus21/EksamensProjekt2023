using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TimerEND : MonoBehaviour
{
    private float timer = 0f;
    public Text TimerTxt;
    bool timerActive = false;


   void Start ()
    {
         // Create a temporary reference to the current scene.
         Scene currentScene = SceneManager.GetActiveScene ();
 
         // Retrieve the name of this scene.
         string sceneName = currentScene.name;
 
         if (sceneName == "Dirt Pits") 
         {
            timerActive = true;
            updateTimer(timer);
             if (timerActive == true)
        {
             timer += Time.deltaTime;
        }
         }
         else if (sceneName == "WinScreen")
         {
             timerActive = false;
         }
    }

    void OnDisable()
    {
        CancelInvoke("UpdateTimer");
        gameObject.SetActive(false);
    }

    void updateTimer(float currentime)
    {
        currentime += 1;

        float minutes = Mathf.FloorToInt(currentime / 60);
        float seconds = Mathf.FloorToInt(currentime % 60);

        TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}