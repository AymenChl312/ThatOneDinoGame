using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public BoxCollider2D itemBox;
    public AudioClip sound;
    public Item item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Inventory.instance.content.Count >= 3)
        {
            return;
        }
        if (collision.CompareTag("Player"))
        {
            TakeItem();
        }
    }

    void TakeItem()
    {
        Inventory.instance.content.Add(item);
        Inventory.instance.UpdateInventoryUI();
        Audio_Manager.instance.PlayClipAt(sound, transform.position);
        Destroy(gameObject);
    }

}
