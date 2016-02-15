using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using System.IO;

public class DialogueController : MonoBehaviour
{
	public static DialogueController instance;

	public Dictionary<object, DialogueParser> instanceDict = new Dictionary<object, DialogueParser>();

	void Awake()
	{
		instance = this;
	}

	public void Load(object obj, string assetLocation)
	{
		if (instanceDict.ContainsKey(assetLocation))
		{
			return;
		}

		DialogueParser parser = new DialogueParser(assetLocation);
		instanceDict.Add(obj, parser);
	}

	public void Step(object instance)
	{
		Step(instance, -1);
	}

	public void Step(object instance, int choice)
	{
		if (!instanceDict.ContainsKey(instance))
		{
			Debug.LogWarning(string.Format(
				"Class {0} is trying to use dialogue that hasn't been loaded.",
				instance.GetType().Name
			));
			return;
		}

		DialogueParser parser = instanceDict[instance];
		parser.StepDialogue(choice);
	}

	void OnGUI()
	{
		GUI.Label(new Rect(0, 0, 800, 20), Application.dataPath);
	}
}

public class DialogueParser
{
	public string assetLocation;
	private string[] rawTextArray;
	public Dictionary<string, object> varDict = new Dictionary<string, object>();

	// Parser info
	public Dictionary<string, int> labelIndexDict = new Dictionary<string, int>();
	public ArrayList commandList = new ArrayList();
	public int curIndex = 0;
	public bool isBlocking = false;
	public bool isAwaitingInput = false;

	public DialogueParser(string assetLocation)
	{
		this.assetLocation = assetLocation;

		if (Application.isEditor)
		{
			StreamReader sr = new StreamReader(Application.dataPath + "/../Dialogue/" + assetLocation);
			rawTextArray = sr.ReadToEnd().Split('\n');
			sr.Close();
		}
		else
		{
			StreamReader sr = new StreamReader(Application.dataPath + "/../Dialogue/" + assetLocation);
			rawTextArray = sr.ReadToEnd().Split('\n');
			sr.Close();
		}

		for (int i = 0; i < rawTextArray.Length; i++)
		{
			string line = rawTextArray[i].Trim();

			Command command = null;
			if (line != "")
			{
				TryComment(i, line, ref command);
				TryLabel(i, line, ref command);
				TryGoTo(i, line, ref command);
				TrySay(i, line, ref command);
				TryChoose(i, line, ref command);
				TryExit(i, line, ref command);

				if (command == null)
				{
					Debug.LogWarning(string.Format("Unknown function on line {0} in text '{1}'", i, assetLocation));
					command = new Command("NONE");
				}
			}
			else
			{
				command = new Command("NONE");
			}

			commandList.Add(command);
		}
		commandList.Add(new Command("EXIT"));
	}

	public void TryComment(int index, string line, ref Command outCommand)
	{
		if (outCommand != null) { return; }
		Regex r = new Regex("^\\s*#");
		Match match = r.Match(line);
		if (!match.Success) { return; }

		// Actual code
		outCommand = new Command("NONE");
 	}

	// [label]
	public void TryLabel(int index, string line, ref Command outCommand)
	{
		if (outCommand != null) { return; }
		Regex r = new Regex("^\\[(.+)\\]$");
		Match match = r.Match(line);
		if (!match.Success) { return; }

		// Actual code
		string label = match.Groups[1].ToString();
		labelIndexDict.Add(label, index);
		outCommand = new Command("NONE");
	}

	// GOTO
	public void TryGoTo(int index, string line, ref Command outCommand)
	{
		if (outCommand != null) { return; }
		Regex r = new Regex("^!GOTO\\[(.+)\\]$");
		Match match = r.Match(line);
		if (!match.Success) { return; }

		// Actual code
		string gotoLabel = match.Groups[1].ToString();

		ArrayList args = new ArrayList();
		args.Add(gotoLabel);
		outCommand = new Command("GOTO", args);
	}

	// SAY
	public void TrySay(int index, string line, ref Command outCommand)
	{
		if (outCommand != null) { return; }
		Regex r = new Regex("^(.+): (.+)$");
		Match match = r.Match(line);
		if (!match.Success) { return; }

		// Actual code
		string name = match.Groups[1].ToString();
		string dialogue = match.Groups[2].ToString();

		ArrayList args = new ArrayList();
		args.Add(name);
		args.Add(dialogue);
		outCommand = new Command("SAY", args);
	}

	// CHOOSE
	public void TryChoose(int index, string line, ref Command outCommand)
	{
		if (outCommand != null) { return; }
		Regex r = new Regex("^!CHOOSE\\[(.+)\\]\\s?(.*)$");
		Match match = r.Match(line);
		if (!match.Success)
		{
			// if it is a dialogue option, don't mark it as an error but keep its answer
			r = new Regex("^>\\s?(.+)");
			match = r.Match(line);
			if (match.Success)
			{
				string option = match.Groups[1].ToString();
				ArrayList args2 = new ArrayList();
				args2.Add(option);
				outCommand = new Command("OPTION", args2);
			}
			return;
		}

		// Actual code
		string label = match.Groups[1].ToString();
		string question = match.Groups[2].ToString();

		ArrayList args = new ArrayList();
		args.Add(label);
		args.Add(question);
		outCommand = new Command("CHOOSE", args);
	}

	// EXIT
	public void TryExit(int index, string line, ref Command outCommand)
	{
		if (outCommand != null) { return; }
		Regex r = new Regex("^!EXIT$");
		Match match = r.Match(line);
		if (!match.Success) { return; }

		// Actual code
		outCommand = new Command("EXIT");
	}

	// == EXECUTE DIALOGUE CODE ==
	public void StepDialogue()
	{
		StepDialogue(-1);
	}

	public void StepDialogue(int choice)
	{
		while (true)
		{
			if (curIndex == -1)
			{
				isBlocking = false;
				return;
			}

			// if there are no more commands, throw an error. last thing in list should be Exit command.
			Command command = commandList[curIndex] as Command;
			if (curIndex < 0 || curIndex >= commandList.Count)
			{
				Debug.LogError(string.Format("Line {0} contains no command in text '{1}'", curIndex, assetLocation));
				curIndex = -1;
				isBlocking = false;
				return;
			}

			if (command.commandType == "NONE" || command.commandType == "OPTION")
			{
				Debug.Log("NONE");
				curIndex += 1;
				isBlocking = false;
			}
			else if (command.commandType == "GOTO")
			{
				string gotoLabelName = command.arguments[0] as string;
				int gotoLabelIndex = labelIndexDict[gotoLabelName] + 1;
				Debug.Log(string.Format("[GOTO] cur: {0}, next: {1}", curIndex, gotoLabelIndex));
				curIndex = gotoLabelIndex;
				isBlocking = false;
			}
			else if (command.commandType == "SAY")
			{
				string name = command.arguments[0] as string;
				string dialogue = command.arguments[1] as string;
				Debug.Log(string.Format(
					"[SAY] {0}: {1}",
					name,
					dialogue
				));
				ChatController.Show(name + ": " + dialogue);
				curIndex += 1;
				isBlocking = true;
				return;
			}
			else if (command.commandType == "CHOOSE")
			{
				if (!isAwaitingInput) // no choice made... yet
				{
					string label = command.arguments[0] as string;
					string question = command.arguments[1] as string;
					Debug.Log(string.Format(
						"[CHOOSE] ({0}) {1}; {2}",
						label,
						question,
						choice
					));

					//accumulate all questions
					int optionIndex = curIndex + 1;
					ArrayList options = new ArrayList();
					string text = question;
					while (true)
					{
						Command cmdOption = commandList[optionIndex] as Command;
						if (cmdOption.commandType != "OPTION")
						{
							break;
						}
						options.Add(cmdOption.arguments[0] as string);
						optionIndex += 1;
					}

					// propose the choice
					ChatController.Choose(question, options);
					isBlocking = true;
					isAwaitingInput = true;
					return;
				}
				else
				{
					string label = command.arguments[0] as string;
					string gotoLabelName = label + " " + (choice+1);

					int gotoLabelIndex = labelIndexDict[gotoLabelName] + 1;
					Debug.Log(string.Format("[CHOOSE] cur: {0}, next: {1}", curIndex, gotoLabelIndex));
					curIndex = gotoLabelIndex;
					isBlocking = false;
					isAwaitingInput = false;
				}
			}
			else if (command.commandType == "EXIT")
			{
				Debug.Log("[EXIT]");
				curIndex = -1;
				isBlocking = false;
				return;
			}
			else
			{
				Debug.LogError(string.Format(
					"Line {0} contains unknown function {1} in text '{2}'",
					curIndex,
					command.commandType,
					assetLocation
				));
			}
		}
	}

	public string GetVariable(string varName)
	{
		object output;
		if (varDict.TryGetValue(varName, out output))
		{
			return output.ToString();
		}
		else
		{
			return string.Format("<var {0} is not in dictionary>", varName);
		}
	}
}

public class Command
{
	public string commandType;
	public ArrayList arguments;

	public Command(string commandType)
	{
		this.commandType = commandType;
	}

	public Command(string commandType, ArrayList arguments)
	{
		this.commandType = commandType;
		this.arguments = arguments;
	}
}

/*   COMMAND TYPES
 * 
 *   NONE	- does nothing
 *   GOTO   - jumps execution to label
 *   SAY    - says something
 *   CHOOSE - choose from a list of things
 *   OPTION - <used as a container for CHOOSE>
 *   EXIT   - exits execution
 *   
 */
