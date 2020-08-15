using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menumanager : MonoBehaviour
{
    bool paused = false;

    public void Load(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Melt()
    {
        Time.timeScale = 1f;
    }

    //private void Update()
    //{ 
    //    var fingerCount = 0;
    //    foreach (Touch touch in Input.touches)
    //    {
    //        if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
    //        {
    //            fingerCount++;
    //        }
    //    }
    //    if (fingerCount > 1)
    //    {
    //        //paused = togglePause();
            
    //    }
        
    //}

    bool togglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            return (false);
        }
        else
        {
            Time.timeScale = 0f;
            return (true);
        }
    }
    
}
