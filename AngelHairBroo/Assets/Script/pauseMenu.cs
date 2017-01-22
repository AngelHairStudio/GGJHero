using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour {

    public Canvas pauseCanvas;

    public bool pauseGame = false;

    private bool showGUI = false;

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            pauseGame = !pauseGame;

            if (pauseGame)
            {
                pauseGame = true;
                showGUI = true;
            }
            else
            {
                pauseGame = false;
                showGUI = false;
            }
        }

        if (showGUI)
        {
            pauseCanvas.enabled = true;
        }
        else
        {
            pauseCanvas.enabled = false;
        }
    }
}
