using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public bool isInRange;

    public Item[] itemToSell;

    
    void Update()
    {
        if(isInRange == true && Input.GetKeyDown(KeyCode.E))
        {
            ShopManager.instance.OpenShop(itemToSell,ShopDialogue.instance.openShopSentence);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isInRange=true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isInRange=false;
        }
    }

}
