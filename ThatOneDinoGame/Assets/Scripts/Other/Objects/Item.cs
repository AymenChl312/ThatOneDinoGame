using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public string description;
    public Sprite image;
    public int price;

    //Bananailes
    public bool doubleJump;

    //Temporange
    public bool temporange;
    public int speed;
    public float slowTime;
    public float duration;

}
