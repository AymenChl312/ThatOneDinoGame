using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Inventory : MonoBehaviour
{
    public AudioClip sound;
    public int coinsCount;
    public List<Item> content = new List<Item>();
    public int contentCurrentIndex = 0;
    public TextMeshProUGUI coinsCountText;

    public Image itemImage;
    public Sprite empty;
    public TextMeshProUGUI itemName;
    
    public static Inventory instance;

    private void Awake()
    {
        if(instance!= null)
        {
            Debug.LogWarning("Il y a plus d'une instance d'inventory dans la scene.");
            return;
        }

        instance = this;

        UpdateInventoryUI();
    }

    public void AddCoins(int count)
    {
        coinsCount += count;
        UpdateTextUI();
    }

    public void RemoveCoins(int count)
    {
        coinsCount -= count;
        UpdateTextUI();
    }

    public void UpdateTextUI()
    {
        coinsCountText.text = coinsCount.ToString();
    }

    public void ConsumeItem()
    {
        if(content.Count == 0)
        {
            return;
        }
        if(CurrentSceneManager.instance.active == true)
        {
            return;
        }
        Audio_Manager.instance.PlayClipAt(sound, transform.position);
        Item currentItem = content[contentCurrentIndex];
        //Bananailes
        CurrentSceneManager.instance.doubleJumpItem = currentItem.doubleJump;
        //Temporange
        CurrentSceneManager.instance.temporange = currentItem.temporange;
        PlayerEffects.instance.TemporangeOn(currentItem.speed, currentItem.duration, currentItem.slowTime);
        //
        CurrentSceneManager.instance.active = true;
        CurrentSceneManager.instance.powerUpActive();
        PowerUpSkin.instance.skinNr = currentItem.id;
        content.Remove(currentItem);
        GetNextItem();
        UpdateInventoryUI();
    }

    public void GetNextItem()
    {
        if(content.Count == 0)
        {
            return;
        }

        contentCurrentIndex++;
        if(contentCurrentIndex > content.Count - 1)
        {           
            contentCurrentIndex=0;
        }
        UpdateInventoryUI();
    }

    public void GetPreviousItem()
    {
        if(content.Count == 0)
        {
            return;
        }
        
        contentCurrentIndex--;
        if(contentCurrentIndex < 0)
        {
            contentCurrentIndex=content.Count - 1;
        }
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        if(content.Count > 0)
        {
            itemImage.sprite = content[contentCurrentIndex].image;
            itemName.text = content[contentCurrentIndex].itemName;
        }
        else
        {
            itemImage.sprite = empty;
            itemName.text = "Empty";
        }
        
    }
}
