using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    private UIManager ui;

    private void Start()
    {
        ui = FindObjectOfType<UIManager>();
    }

    private void OnMouseDown()
    {
        if (this.tag == "main menu")
        {
            ui.OpenMainMenu();
        }

        else if (this.tag == "new game")
        {
            ui.CloseMainMenu();
            ui.gm.playing = true;
        }

        else if (this.tag == "about")
        {
            ui.OpenAboutPanel();
        }

        else if (this.tag == "exit")
        {
            Application.Quit();
        }

        else if (this.tag == "back")
        {
            ui.CloseAboutPanel();
        }
    }

    private void OnMouseEnter()
    {
        GetComponentInChildren<TextMesh>().color = new Color32(140, 140, 140, 255);
    }

    private void OnMouseExit()
    {
        GetComponentInChildren<TextMesh>().color = Color.white;
    }
}
