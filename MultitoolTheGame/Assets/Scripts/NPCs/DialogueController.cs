using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public string NPCName;

    public List<DialogueLine> lines = new List<DialogueLine>();

    [Header("GameOjects")]
    public GameObject pressToTalk;
    public GameObject dialogueBox;

    [Header("Texts")]
    public Text dialogueText;
    public Text dialogueName;

    public bool canTalk;

    void Update()
    {
        if (canTalk)
        {
            pressToTalk.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(startChat());
            }
        } else
        {
            pressToTalk.SetActive(false);
        }
    }

    IEnumerator startChat()
    {
        pressToTalk.SetActive(false);
        dialogueBox.SetActive(true);
        dialogueName.text = NPCName;
        dialogueText.text = "";
        int currentLine = 0;

        DialogueLine preLine = lines[currentLine];

        while(currentLine < lines.Count)
        {
            DialogueLine curLine = lines[currentLine];
            if(currentLine == 0)
            {
                StartCoroutine(sendLine(lines[0]));
            } else
            {
                if (curLine != preLine)
                {
                    dialogueText.text = "";
                    StartCoroutine(sendLine(curLine));
                }
            }

            if (dialogueText.text == curLine.line)
            {
                if (Input.GetKeyDown(KeyCode.KeypadEnter))ss
                {
                    currentLine++;
                }
            }
        }
        StartCoroutine(endChat());
        yield return null;
    }

    IEnumerator sendLine(DialogueLine line)
    {
        for (int i = 0; i < line.line.Length; i++)
        {
            dialogueText.text += line.line[i];
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator endChat()
    {
        dialogueBox.SetActive(false);
        dialogueName.text = "";
        dialogueText.text = "";
        yield return null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTalk = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTalk = false;
        }   
    }
}

[System.Serializable]
public class DialogueLine
{
    [TextArea]
    public string line;
    public bool question;
    public List<string> options;
}
