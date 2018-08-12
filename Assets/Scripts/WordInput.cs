using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordInput : MonoBehaviour
{
    private GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        SetActive(true);
    }

    public void SetActive(bool value)
    {
        if (value)
            GetComponent<InputField>().ActivateInputField();
        else
            GetComponent<InputField>().DeactivateInputField();

    }

    public void CheckForWord()
    {
        foreach (Piece p in gm.pieces)
        {
            if (p.GetComponentInChildren<TextMesh>().text == this.GetComponent<InputField>().text)
            {
                gm.pieces.Remove(p);
                p.TakeAway();

                break;
            }
        }

        this.GetComponent<InputField>().text = "";
        SetActive(true);

    }
}
