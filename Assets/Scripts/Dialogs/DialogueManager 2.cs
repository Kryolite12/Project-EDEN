using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager2 : MonoBehaviour
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
    public GameObject SelectLevelButton;
    

    private bool dialogueStarted;
    private int playerIndex;
    private int cipherIndex;
    public string sceneName;


    public GameObject background;

    public GameObject tutorialLevel;
    public GameObject self;
    public object StartCoroutene { get; private set; }

    public string levelSelectorSceneName = "LevelSelectorScene";

    public void Start()
    {
        tutorialLevel.SetActive(false);
        SelectLevelButton.SetActive(false);
        background.SetActive(true);
        playerReplyButton.SetActive(false);
        cipherReplyButton.SetActive(false);
        StartDialogue();
    }
    public void Update()
    {
        if (cipherIndex == 3 && playerIndex == 2)
        {
            playerReplyButton.SetActive(false);
            cipherReplyButton.SetActive(false);
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
        if (playerIndex < playerDialogueSentences.Length - 1)
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
        if (cipherIndex == 3)
        {
            playerReplyButton.SetActive(false);
            cipherReplyButton.SetActive(false);
            SelectLevelButton.SetActive(true);
        }
    }

    public void GoToLevelSelector()
    {
        self.SetActive(false);
        SceneManager.LoadScene(sceneName); // Switch to the desired scene
        CompleteTutorial();
    }

    public void CompleteTutorial()
    {
        // Mark the tutorial as completed
        PlayerPrefs.SetInt("TutorialCompleted", 1);
        PlayerPrefs.Save(); // Make sure to save the PlayerPrefs

        // Load the level selector scene
        SceneManager.LoadScene(levelSelectorSceneName);
    }

}
