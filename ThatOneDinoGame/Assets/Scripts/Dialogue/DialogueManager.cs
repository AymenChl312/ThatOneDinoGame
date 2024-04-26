using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public Animator animator;

    public bool isTalking;
    public float textSpeed;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image characterSprite;
    private AudioSource audioSource;
    private bool stopAudioSource=true;
    private Queue<string> sentences;
    private Queue<Sprite> sprites;
    private Queue<string> names;
    private Queue<AudioClip> characterVoices;
    public static DialogueManager instance;


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
        characterVoices = new Queue<AudioClip>();
        audioSource =this.gameObject.AddComponent<AudioSource>();
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
        characterVoices.Clear();

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
        foreach (AudioClip characterVoice in dialogue.characterVoices)
        {
            characterVoices.Enqueue(characterVoice);
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
            AudioClip characterVoice = characterVoices.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence, characterVoice));
        }
    }

    IEnumerator TypeSentence(string sentence, AudioClip characterVoice)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            if(stopAudioSource)
            {
                Audio_Manager.instance.stop = true;
            }
            Audio_Manager.instance.PlayClipAt(characterVoice, transform.position);    
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void EndDialogue()
    { 
        animator.SetBool("isOpen", false);
        PlayerMovement.instance.enabled=true;
    }


}
