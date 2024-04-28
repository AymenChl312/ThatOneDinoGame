using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDialogue : MonoBehaviour
{
    public string openShopSentence;
    public string closeShopSentence;
    public string noMoneySentence;
    public string noSpaceSentence;
    public string[] buySentences;
    public AudioClip characterVoice;

    public static ShopDialogue instance;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de ShopDialogue dans la scene.");
            return;
        }
        instance = this;
    }
}
