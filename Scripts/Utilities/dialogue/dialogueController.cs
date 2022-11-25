using System.Collections.Generic;
using UnityEngine;

// Controls the dialogue of an NPC
// Dialogues should be placed into a .txt file whiout formating in the relative directory ./Txts/Dialogues/
public class dialogueController : MonoBehaviour
{
    [SerializeField]string fileName;
    [SerializeField] int wordsAmount;
    [SerializeField][TextArea] string txt;
    string[] cutTxt;
    [SerializeField]List<string> finalTxt = new List<string>();
    int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        gameEvents.current.onPassBool += talk;
    }

    // This should be moved to another class
    void talk(string parameter, bool onOff)
    {
        if(parameter.Contains("UI"))
        {
            if(currentIndex == finalTxt.Count)
            {
                gameEvents.current.passValue("dialogue","");
                gameEvents.current.passBool("paralyze",false);
                currentIndex = 0;
                return;
            }
            if(onOff)
            {
                gameEvents.current.passValue("dialogue",finalTxt[currentIndex]);
                gameEvents.current.passBool("paralyze",true);
                currentIndex++;
            }
        }
    }

    // Separetes text into chunks that fit the text box
    public void trimText()
    {
        finalTxt.Add(new string(""));
        cutTxt = txt.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < cutTxt.Length; i++)
        {
            finalTxt[finalTxt.Count - 1] += cutTxt[i] + " ";
            if (i % wordsAmount == 0 && i > 0)
                finalTxt.Add(new string(""));
        }
        if (finalTxt[finalTxt.Count - 1].Split(' ', System.StringSplitOptions.RemoveEmptyEntries).Length <= 3)
        {
            finalTxt[finalTxt.Count - 2] += " " + finalTxt[finalTxt.Count - 1];
            finalTxt.RemoveAt(finalTxt.Count - 1);
        }
        print("trimmed");
    }

    // Looks for the file with the corresponding text and loads it into the scene
    public void loadText()
    {
        txt = System.IO.File.ReadAllText(Application.dataPath + $"/Txts/Dialogues/{fileName}.txt");
    }

}
