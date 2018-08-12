using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{
    [SerializeField] private TextAsset txtWords3;
    [SerializeField] private TextAsset txtWords4;
    [SerializeField] private TextAsset txtWords5;
    [SerializeField] private TextAsset txtWords6;
    [SerializeField] private TextAsset txtWords7;
    [SerializeField] private TextAsset txtWords8;
    [SerializeField] private TextAsset txtWords9;
    [SerializeField] private TextAsset txtWords10;
    [SerializeField] private TextAsset txtWords11;
    [SerializeField] private TextAsset txtWords12;

    private static List<string> Words3 = new List<string>();
    private static List<string> Words4 = new List<string>();
    private static List<string> Words5 = new List<string>();
    private static List<string> Words6 = new List<string>();
    private static List<string> Words7 = new List<string>();
    private static List<string> Words8 = new List<string>();
    private static List<string> Words9 = new List<string>();
    private static List<string> Words10 = new List<string>();
    private static List<string> Words11 = new List<string>();
    private static List<string> Words12 = new List<string>();

    TextAsset[] textAssets;
    List<string>[] wordLists;

    private int prevRow = 0;

    public void LoadWords()
    {
        textAssets = new TextAsset[] { txtWords3, txtWords4, txtWords5, txtWords6, txtWords7, txtWords8, txtWords9, txtWords10, txtWords11, txtWords12 };
        wordLists = new List<string>[] { Words3, Words4, Words5, Words6, Words7, Words8, Words9, Words10, Words11, Words12 };

        for (int i = 0; i < textAssets.Length; i++)
        {
            wordLists[i].AddRange(textAssets[i].ToString().Split('\n'));
        }

        for (int i = 0; i < wordLists.Length; i++)
            for (int j = 0; j < wordLists[i].Count; j++)
            {
                if (wordLists[i][j].EndsWith("\r"))
                     wordLists[i][j] = wordLists[i][j].Remove(wordLists[i][j].Length - 1);
            }
    }

    public Word Generate()
    {
        int r;
        do
        {
            r = Random.Range(1, 11);
        } while (r == prevRow);

        Word w = new Word(GetWord(), r);
        prevRow = w.Row;

        return w;
    }

    private int GetWordLength()
    {
        int r = Random.Range(0, 100);

        if (r >= 0 && r < 20) return 3;
        if (r >= 20 && r < 38) return 4;
        if (r >= 38 && r < 53) return 5;
        if (r >= 53 && r < 67) return 6;
        if (r >= 67 && r < 79) return 7;
        if (r >= 79 && r < 86) return 8;
        if (r >= 86 && r < 91) return 9;
        if (r >= 91 && r < 95) return 10;
        if (r >= 95 && r < 99) return 11;
        if (r == 99) return 12;

        return -1;
    }

    private string GetWord()
    {
        int l = GetWordLength();
        int r = Random.Range(0, wordLists[l - 3].Count - 1);

        return wordLists[l - 3][r];
    }
}
