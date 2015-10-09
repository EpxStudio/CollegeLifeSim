using UnityEngine;
using System.Collections;

public class Message
{
    public string text;

    public Message(string text)
    {
        this.text = text;
    }

    public string GetText()
    {
        return text;
    }

    public bool Advance()
    {
        return true;
    }
}
