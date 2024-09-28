using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject playerCard;
    public GameObject cipherCard;

    [SerializeField] private float typingSpeed = 0.05f;

    [SerializeField] private bool CipherSpeakingFirst;

    [SerializeField] private TextMeshProUGUI playerDialogue;
    [SerializeField] private TextMeshProUGUI cipherDialogue;

    [SerializeField] private string[] playerDialogueSentences;
    [SerializeField] private string[] cipherDialogueSentences;

    [SerializeField] private GameObject playerReplyButton;
    [SerializeField] private GameObject cipherReplyButton;
    public GameObject PlayButton;

    private bool dialogueStarted;
    private int playerIndex;
    private int cipherIndex;


    public GameObject icon;
    public GameObject level;

    public GameObject self;
    public GameObject tutorialLevel;
    public object StartCoroutene { get; private set; }

    public void Start()
    {
        PlayButton.SetActive(false);
        icon.SetActive(true);
        level.SetActive(false);
        playerReplyButton.SetActive(false);
        cipherReplyButton.SetActive(false);
        StartDialogue();    
    }

    public void Update()
    {
        if (cipherIndex == 4)
        {
            icon.SetActive(false);
            level.SetActive(true);
        }

        if (cipherIndex == 5)
        {
            playerCard.SetActive(false);
            playerReplyButton.SetActive(false);
            cipherReplyButton.SetActive(false);
            PlayButton.SetActive(true);
        }

    }
    public void StartDialogue()
    {
        if (CipherSpeakingFirst)
        {
            StartCoroutine(TypeCipherDialogue());
        }
        else
        {
            StartCoroutine(TypePlayerDialogue());
           
        }
    }

    private IEnumerator TypePlayerDialogue()
    {
        foreach (char letter in playerDialogueSentences[playerIndex].ToCharArray())
        {
            playerDialogue.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        playerReplyButton.SetActive(true);
    }

    private IEnumerator TypeCipherDialogue()
    {
        foreach (char letter in cipherDialogueSentences[cipherIndex].ToCharArray())
        {
            cipherDialogue.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        cipherReplyButton.SetActive(true);
    }
    public void ContinuePlayerDialogue()
    {
        cipherReplyButton.SetActive(false);
        if (playerIndex < playerDialogueSentences.Length -1)
        {
            if (dialogueStarted)
            {
                playerIndex++;
            }
            else
            {
                dialogueStarted = true;
            }

            playerDialogue.text = string.Empty;
            StartCoroutine(TypePlayerDialogue());
        }
    }
    public void ContinueCipherDialogue()
    {
        playerReplyButton.SetActive(false);
        if (cipherIndex < cipherDialogueSentences.Length - 1)
        {
            if (dialogueStarted)
            {
                cipherIndex++;
            }
            else
            {
                dialogueStarted = true;
            }
            cipherDialogue.text = string.Empty;
            StartCoroutine(TypeCipherDialogue());
        }
        
    }

    public void PlayTutorial()
    {
        self.SetActive(false);
        tutorialLevel.SetActive(true);
    }
  

}

