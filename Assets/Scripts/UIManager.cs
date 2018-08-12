using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameManager gm;

    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject aboutPanel;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("Game");
    }

    public void CloseMainMenu()
    {
        mainMenuPanel.SetActive(false);
    }

    public void OpenAboutPanel()
    {
        aboutPanel.SetActive(true);
    }

    public void CloseAboutPanel()
    {
        aboutPanel.SetActive(false);
    }
}
