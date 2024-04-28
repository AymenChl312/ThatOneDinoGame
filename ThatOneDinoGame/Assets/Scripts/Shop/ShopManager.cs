using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public AudioClip music;
    public AudioSource audioSource;
    public Animator animator;
    public GameObject sellButtonPrefab;
    public Transform sellButtonsParent;

    public TextMeshProUGUI dialogueText;
    private bool stopAudioSource=true;
    public float textSpeed;
    public static ShopManager instance;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de ShopManager dans la scene.");
            return;
        }
        instance = this;
    }

    public void OpenShop(Item[] items, string openSentence)
    {
        audioSource.Stop();
        audioSource.clip = music;
        audioSource.Play();
        UpdateItemToSell(items);
        animator.SetBool("isOpen", true);

        PlayerMovement.instance.rb.velocity = Vector3.zero;
        PlayerMovement.instance.animator.SetFloat("Speed", 0);
        PlayerMovement.instance.enabled=false;

        StartCoroutine(TypeSentence(openSentence, ShopDialogue.instance.characterVoice));
    }

    public IEnumerator TypeSentence(string sentence, AudioClip characterVoice)
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

    void UpdateItemToSell(Item[] items)
    {
        for (int i = 0; i < sellButtonsParent.childCount; i++)
        {
            Destroy(sellButtonsParent.GetChild(i).gameObject);
        }
        for (int i = 0; i < items.Length; i++)
        {
            GameObject button = Instantiate(sellButtonPrefab, sellButtonsParent); 
            SellButtonItem buttonScript = button.GetComponent<SellButtonItem>();
            buttonScript.itemName.text = items[i].name;
            buttonScript.itemImage.sprite = items[i].image;
            buttonScript.itemPrice.text = items[i].price.ToString();
            buttonScript.item = items[i];
            button.GetComponent<Button>().onClick.AddListener(delegate{buttonScript.BuyItem();});
        }
    }

    public void CloseShop()
    { 
        StartCoroutine(TypeSentence(ShopDialogue.instance.closeShopSentence, ShopDialogue.instance.characterVoice));
        animator.SetBool("isOpen", false);
        PlayerMovement.instance.enabled=true;
        audioSource.Stop();
        Audio_Manager.instance.Start();
    }
}
