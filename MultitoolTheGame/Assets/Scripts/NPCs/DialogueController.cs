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

    void Start()
    {
        dialogueBox.SetActive(false);
        canTalk = false;
    }

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

        DialogueLine preLine = lines[0];

        while(currentLine < lines.Count)
        {
            DialogueLine curLine = lines[currentLine];
            if (!curLine.completed)
            {
                StartCoroutine(sendLine(curLine));
            } else
            {
                currentLine++;
            }
        }

        StartCoroutine(endChat());
        yield return null;
    }

    IEnumerator sendLine(DialogueLine line)
    {
        if (line != lines[0]) yield return new WaitForSeconds(0.3f);
        dialogueText.text = "";
        for (int i = 0; i < line.line.Length; i++)
        {
            dialogueText.text += line.line[i];
            yield return new WaitForSeconds(0.1f);
        }
        if(dialogueText.text == line.line)
        {
            line.completed = true;
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

    public bool completed = false;
}
