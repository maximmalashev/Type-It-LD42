using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject sidePanel;
    [SerializeField] private GameObject board;
    [SerializeField] private GameObject piece;
    [SerializeField] private GameObject borderCollider;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private GameObject nextLength;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject gameOverScore;
    [SerializeField] private GameObject gameOverPanel;

    public ScreenSettings settings;
    private WordGenerator generator;

    private GameObject arr;
    private GameObject scrPanel;
    private GameObject nxtLength;

    public bool playing = false;
    public bool dead = false;
    public bool canGenerateNewWord = true;
    //private float timeElapsed = 0f;
    //private float timeThreshold = 5f;
    private float timeCounter = 0f;

    public float velocityModifier = 1f;
    private int seconds = 0;
    private int lastSeconds = -1;

    public int score = 0;

    public List<Piece> pieces = new List<Piece>();
    public Dictionary<int, Color32> colors = new Dictionary<int, Color32>();

    void Start ()
    {
        settings = new ScreenSettings(Screen.width, Screen.height);
        generator = FindObjectOfType<WordGenerator>();
        generator.LoadWords();

        InitUI();
        LoadColors();

        Debug.Log(settings.LeftBorder * settings.PixelToUnitScale);
	}
	
	void FixedUpdate ()
    {
		if (playing)
        {
            if (canGenerateNewWord)
            {
                canGenerateNewWord = false;
                Word w = generator.Generate();

                GameObject p = Instantiate(piece, new Vector2(-15, settings.RowHeights[w.Row - 1] * settings.PixelToUnitScale), Quaternion.identity);
                Debug.Log(w.Row);

                p.GetComponent<Piece>().letters = w.Length;
                p.GetComponentInChildren<PieceGraphics>().SetScale();
                p.GetComponent<BoxCollider>().size = new Vector3(p.GetComponent<Piece>().letters * settings.PieceHeight * settings.PixelToUnitScale, settings.PieceHeight * settings.PixelToUnitScale - .2f);
                p.GetComponent<SphereCollider>().center = new Vector3(-(float)w.Length / 2, 0, 0);
                p.GetComponentInChildren<TextMesh>().text = w.Value;
                p.GetComponentInChildren<SpriteRenderer>().color = colors[w.Length];

                pieces.Add(p.GetComponent<Piece>());

                arr.transform.position = new Vector3(arr.transform.position.x, settings.RowHeights[w.Row - 1] * settings.PixelToUnitScale, arr.transform.position.z);
                nxtLength.GetComponentInChildren<TextMesh>().text = w.Length.ToString();

                //timeElapsed = 0;
            }

            seconds = (int)timeCounter;
            if (seconds != lastSeconds)
            {
                if (velocityModifier < 5f)
                    velocityModifier = (float)seconds / 40;
                else
                    velocityModifier = 5f;

                lastSeconds = seconds;
            }

            scrPanel.GetComponentInChildren<TextMesh>().text = score.ToString();

            //timeElapsed += Time.deltaTime;
            timeCounter += Time.deltaTime;
            //timeThreshold *= .9999f;
        }

        if (dead)
        {
            gameOverScore.GetComponent<TextMesh>().text = score.ToString();

            gameOverPanel.SetActive(true);
        }
	}

    void InitUI()
    {
        GameObject sp = Instantiate(sidePanel, settings.SidepanelPos, Quaternion.identity);
        sp.transform.localScale = new Vector3(settings.SidepanelWidth * settings.PixelToUnitScale, settings.SidepanelHeight * settings.PixelToUnitScale);

        GameObject b = Instantiate(board, settings.BoardPos, Quaternion.identity);
        b.transform.localScale = new Vector3(settings.BoardWidth * settings.PixelToUnitScale, settings.BoardHeight * settings.PixelToUnitScale);

        GameObject col = Instantiate(borderCollider, new Vector3(settings.RightBorder * settings.PixelToUnitScale + .5f, 0), Quaternion.identity);

        arr = Instantiate(arrow, new Vector3(settings.LeftBorder * settings.PixelToUnitScale - .25f, 0, -1.5f), Quaternion.identity);

        scrPanel = Instantiate(scorePanel, settings.SidepanelPos, Quaternion.identity);
        scrPanel.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector3((float)settings.SidepanelWidth / 3 * settings.PixelToUnitScale, (float)settings.SidepanelWidth / 3 * settings.PixelToUnitScale);
        scrPanel.transform.position = new Vector3(scrPanel.transform.position.x, settings.ScreenHeight / 2 * settings.PixelToUnitScale - 1f, scrPanel.transform.position.z - .1f);

        nxtLength = Instantiate(nextLength, settings.SidepanelPos, Quaternion.identity);
        nxtLength.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector3((float)settings.SidepanelWidth / 3 * settings.PixelToUnitScale, (float)settings.SidepanelWidth / 3 * settings.PixelToUnitScale);
        nxtLength.transform.position = new Vector3(nxtLength.transform.position.x, settings.ScreenHeight / 2 * settings.PixelToUnitScale - 2f - (float)settings.SidepanelWidth / 3 * settings.PixelToUnitScale, nxtLength.transform.position.z - .1f);

        GameObject mainMenuBtn = Instantiate(button, settings.SidepanelPos, Quaternion.identity);
        mainMenuBtn.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector3((float)settings.SidepanelWidth / 2 * settings.PixelToUnitScale, mainMenuBtn.GetComponentInChildren<SpriteRenderer>().transform.localScale.y, mainMenuBtn.GetComponentInChildren<SpriteRenderer>().transform.localScale.z);
        mainMenuBtn.transform.position = new Vector3(mainMenuBtn.transform.position.x, -settings.ScreenHeight / 2 * settings.PixelToUnitScale + 1f, mainMenuBtn.transform.position.z - .1f);
        mainMenuBtn.GetComponentInChildren<TextMesh>().text = "Main Menu";
        mainMenuBtn.tag = "main menu";
    }

    void LoadColors()
    {
        colors[3]  = new Color32(34, 177, 76, 255);
        colors[4]  = new Color32(16, 206, 6, 255);
        colors[5]  = new Color32(238, 221, 13, 255);
        colors[6]  = new Color32(236, 158, 15, 255);
        colors[7]  = new Color32(231, 68, 20, 255);
        colors[8]  = new Color32(217, 34, 34, 255);
        colors[9]  = new Color32(176, 0, 0, 255);
        colors[10] = new Color32(204, 0, 148, 255);
        colors[11] = new Color32(142, 0, 204, 255);
        colors[12] = new Color32(0, 20, 204, 255);
    }
}
