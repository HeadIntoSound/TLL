using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class dialogueUIController : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] TMP_Text dialogueText;

    // Start is called before the first frame update
    void Start()
    {
        gameEvents.current.onPassValue += controlDialogue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void controlDialogue(string parameter, string value)
    {
        if(parameter.Contains("dialogue"))
        {
            if(value != "")
            {
                dialogueText.text = value;
                dialogueBox.SetActive(true);
                
            }
            if(value == "")
            {
                dialogueBox.SetActive(false);
                gameEvents.current.passBool("endTalk",true);
            }
                

        }
    }
}
