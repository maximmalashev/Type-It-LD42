using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSettings
{
    public const int REFERENCE_SCREEN_WIDTH = 1920;
    public const int REFERENCE_SCREEN_HEIGHT = 1080;

    public const int REFERENCE_SIDEPANEL_WIDTH = 300;
    public const int REFERENCE_SIDEPANEL_HEIGHT = 1080;

    public const int REFERENCE_BOARD_WIDTH = 1500;
    public const int REFERENCE_BOARD_HEIGHT = 750;

    public ScreenSettings(int screenWidth, int screenHeight)
    {
        this.ScreenWidth = screenWidth;
        this.ScreenHeight = screenHeight;

        float wScale = (float)ScreenWidth / REFERENCE_SCREEN_WIDTH;
        float hScale = (float)ScreenHeight / REFERENCE_SCREEN_HEIGHT;

        if (wScale < hScale) Scale = wScale;
        else if (wScale > hScale) Scale = hScale;
        else Scale = 1;

        ScreenToWorldLeftBottomCorner = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        Vector3 ScreenToWorldLeftBottomCornerOnePixel = Camera.main.ScreenToWorldPoint(new Vector3(1, 1));
        PixelToUnitScale = ScreenToWorldLeftBottomCornerOnePixel.x - ScreenToWorldLeftBottomCorner.x;

        SidepanelWidth = (int)((float)REFERENCE_SIDEPANEL_WIDTH * Scale);
        SidepanelHeight = screenHeight;
        SidepanelPos = Camera.main.ScreenToWorldPoint(new Vector3(SidepanelWidth / 2, SidepanelHeight / 2));
        SidepanelPos = new Vector3(SidepanelPos.x, SidepanelPos.y, -1f);

        BoardWidth = (int)((float)REFERENCE_BOARD_WIDTH * Scale);
        BoardHeight = (int)((float)REFERENCE_BOARD_HEIGHT * Scale);
        BoardPos = Camera.main.ScreenToWorldPoint(new Vector3(BoardWidth / 2 + SidepanelWidth, screenHeight / 2));
        BoardPos = new Vector3(BoardPos.x, BoardPos.y, 1f);

        RowHeights = new float[10];
        for (int i = 0; i < RowHeights.Length; i++)
        {
            RowHeights[i] = (i * (float)BoardHeight / 10f + (float)BoardHeight / 10f / 2f) - (float)BoardHeight / 2f;
        }

        RightBorder = BoardWidth + SidepanelWidth - ScreenWidth / 2;
        LeftBorder = SidepanelWidth - ScreenWidth / 2;

        PieceHeight = (float)BoardHeight / 10f;
    }

    public int ScreenWidth { get; private set; }
    public int ScreenHeight { get; private set; }

    public float Scale { get; private set; }

    public Vector2 ScreenToWorldLeftBottomCorner { get; private set; }
    public float PixelToUnitScale { get; private set; }

    public int SidepanelWidth { get; private set; }
    public int SidepanelHeight { get; private set; }
    public Vector3 SidepanelPos { get; private set; }

    public int BoardWidth { get; private set; }
    public int BoardHeight { get; private set; }
    public Vector3 BoardPos { get; private set; }

    public float[] RowHeights { get; private set; }

    public float RightBorder { get; private set; }
    public float LeftBorder { get; private set; }

    public float PieceHeight { get; private set; }
}
