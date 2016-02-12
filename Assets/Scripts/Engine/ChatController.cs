using UnityEngine;
using System.Collections;

public class ChatController : MonoBehaviour
{
    public static ChatController instance;

    public bool isVisible = false;
    public ArrayList messages = new ArrayList();
    public GUISkin guiSkin;
	public object curDialogueInstance = null;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
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

	public void SetDialogue(object instance)
	{
		curDialogueInstance = instance;
		if (instance != null)
		{
			DialogueController.instance.Step(curDialogueInstance);
		}
	}

    public void NextMessage()
    {
		if (curDialogueInstance != null)
		{
			Message curMsg = GetCurrentMessage();
			if (curMsg != null && curMsg is MessageChoose)
			{
				DialogueController.instance.Step(curDialogueInstance, (curMsg as MessageChoose).curOption);
			}
			DialogueController.instance.Step(curDialogueInstance);
		}

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
		instance.isVisible = true;
		instance.messages.Add(new MessageText(message));
	}

	public static void Choose(string message, ArrayList options)
	{
		instance.isVisible = true;
		instance.messages.Add(new MessageChoose(message, options));
	}

	public static void Choose(string message, params string[] options)
	{
		instance.isVisible = true;
		instance.messages.Add(new MessageChoose(message, options));
	}

	public void SelectNext()
	{
		MessageChoose msg = GetCurrentMessage() as MessageChoose;
		if (msg is MessageChoose)
		{
			msg.curOption = Mathf.Clamp(msg.curOption + 1, 0, msg.options.Length - 1);
		}
	}

	public void SelectPrev()
	{
		MessageChoose msg = GetCurrentMessage() as MessageChoose;
		if (msg is MessageChoose)
		{
			msg.curOption = Mathf.Clamp(msg.curOption - 1, 0, msg.options.Length - 1);
		}
	}
}

public interface Message
{
	string GetText();
}

class MessageText : Message
{
	public string text;

	public MessageText(string text)
	{
		this.text = text;
	}

	public string GetText()
	{
		return this.text;
	}

	public bool Advance()
	{
		return true;
	}
}

class MessageChoose : Message
{
	public string question;
	public string[] options;
	public int curOption = 0;

	public MessageChoose(string question, params string[] options)
	{
		this.question = question;
		this.options = options;
	}

	public MessageChoose(string question, ArrayList options)
	{
		this.question = question;
		this.options = new string[options.Count];
		for (int i = 0; i < options.Count; i++)
		{
			this.options[i] = options[i] as string;
		}
	}

	public string GetText()
	{
		string output = question;
		for (int i = 0; i < options.Length; i++)
		{
			string option = options[i];
			output += "\n";

			if (curOption == i)
			{
				output += "> ";
			}

			output += option;
		}
		return output;
	}

	public bool Advance()
	{
		return true;
	}
}