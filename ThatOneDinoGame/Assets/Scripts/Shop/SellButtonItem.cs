using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellButtonItem : MonoBehaviour
{
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemPrice;
    public Image itemImage;
    public Item item;

    public void BuyItem()
    {
        Inventory inventory = Inventory.instance;
        string buySentence = ShopDialogue.instance.buySentences[Random.Range(0, ShopDialogue.instance.buySentences.Length)];
        if(inventory.coinsCount >= item.price && inventory.content.Count < 3)
        {
            inventory.content.Add(item);
            inventory.UpdateInventoryUI();
            inventory.coinsCount -= item.price;
            inventory.UpdateTextUI();
            ShopManager.instance.StartCoroutine(ShopManager.instance.TypeSentence(buySentence, ShopDialogue.instance.characterVoice));
        }
        else if(inventory.content.Count >= 3)
        {
            ShopManager.instance.StartCoroutine(ShopManager.instance.TypeSentence(ShopDialogue.instance.noSpaceSentence, ShopDialogue.instance.characterVoice));
        }
        else if(inventory.coinsCount < item.price)
        {
            ShopManager.instance.StartCoroutine(ShopManager.instance.TypeSentence(ShopDialogue.instance.noMoneySentence, ShopDialogue.instance.characterVoice));
        }
        
    }
}
