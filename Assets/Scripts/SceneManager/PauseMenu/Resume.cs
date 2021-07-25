using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject player;

    public void OnClick()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        player.GetComponent<Weapon>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
        PlayerController.isGamePaused = false;
    }
}
