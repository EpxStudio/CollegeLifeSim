using UnityEngine;
using System.Collections;

public class ChatController : MonoBehaviour
{
    private static ChatController instance;

    public static bool isVisible = false;
    public ArrayList messages = new ArrayList();
    public GUISkin guiSkin;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (InputController.GetKeyDown(Keys.Advance))
        {
            NextMessage();
        }
    }

    void OnGUI()
    {
        if (!isVisible)
        {
            return;
        }

        GUI.skin = guiSkin;
        int height = 160;
        Rect rect = new Rect(0, Screen.height - height, Screen.width, height);
        GUI.Box(rect, GetCurrentMessage().GetText());
    }

    public void NextMessage()
    {
        if (messages.Count == 0)
        {
            return;
        }

        messages.RemoveAt(0);
        if (messages.Count == 0)
        {
            isVisible = false;
        }
    }

    public Message GetCurrentMessage()
    {
        return messages.Count == 0 ? null : (Message) messages[0];
    }

    public static void Show(string message)
    {
        isVisible = true;
        instance.messages.Add(new Message(message));
    }
}
