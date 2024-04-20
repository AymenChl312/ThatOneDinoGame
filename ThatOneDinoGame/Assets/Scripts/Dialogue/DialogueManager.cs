using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public Animator animator;
    public static DialogueManager instance;
    public bool isTalking;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image characterSprite;

    private Queue<string> sentences;
    private Queue<Sprite> sprites;
    private Queue<string> names;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DialogueManager dans la scene.");
            return;
        }
        instance = this;

        sentences = new Queue<string>();
        sprites = new Queue<Sprite>();
        names = new Queue<string>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isTalking==true)
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        PlayerMovement.instance.rb.velocity = Vector3.zero;
        PlayerMovement.instance.animator.SetFloat("Speed", 0);
        PlayerMovement.instance.enabled=false;
        
        isTalking = true;
        animator.SetBool("isOpen", true);
        sentences.Clear();
        names.Clear();
        sprites.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (string name in dialogue.names)
        {
            names.Enqueue(name);
        }
        foreach (Sprite sprite in dialogue.sprites)
        {
            sprites.Enqueue(sprite);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(Input.GetKeyDown(KeyCode.E) && isTalking==true && PauseMenu.gameIsPaused==false)
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = sentences.Dequeue();
            nameText.text = names.Dequeue();
            characterSprite.sprite = sprites.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
    }
    void EndDialogue()
    { 
        animator.SetBool("isOpen", false);
        PlayerMovement.instance.enabled=true;
    }


}
