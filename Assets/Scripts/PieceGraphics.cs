using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceGraphics : MonoBehaviour
{
    private ScreenSettings settings;
    private int letters;

    public void SetScale()
    {
        letters = GetComponentInParent<Piece>().letters;
        settings = FindObjectOfType<GameManager>().settings;
        transform.localScale = new Vector3(letters * settings.PieceHeight * settings.PixelToUnitScale, settings.PieceHeight * settings.PixelToUnitScale);
    }
}
