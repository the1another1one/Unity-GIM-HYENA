using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject death_menu;
    public GameObject player;
    public void GameOver()
    {
        Destroy(player);
        death_menu.SetActive(true);
    }
}
