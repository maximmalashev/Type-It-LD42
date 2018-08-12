using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word
{
    public Word(string value, int row)
    {
        this.Value = value;
        this.Row = row;
        this.Length = value.Length;
    }

    public string Value { get; private set; }
    public int Row { get; private set; }
    public int Length { get; private set; }

}
