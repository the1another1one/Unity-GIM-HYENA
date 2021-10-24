using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject death_menu;
    public void GameOver()
    {
        Time.timeScale = 0f;
        death_menu.SetActive(true);
    }
}
