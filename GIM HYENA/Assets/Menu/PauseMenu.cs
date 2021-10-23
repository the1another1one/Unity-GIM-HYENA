using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool game_paused = false;
    public GameObject pause_menu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (game_paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        // pause menu
        pause_menu.SetActive(true);
        // freeze time
        Time.timeScale = 0f;

        // game is pause
        game_paused = true;
    }
    public void Resume()
    {
        // deactive pause menu
        pause_menu.SetActive(false);

        // unfreeze time
        Time.timeScale = 1f;

        // game is resume
        game_paused = false;
    }
}
