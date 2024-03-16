using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public int coinsCount;
    public TextMeshProUGUI coinsCountText;

    public static Inventory instance;

    private void Awake()
    {
        if(instance!= null)
        {
            Debug.LogWarning("Il y a plus d'une instance d'inventory dans la scene.");
            return;
        }

        instance = this;
    }

    public void AddCoins(int count)
    {
        coinsCount += count;
        coinsCountText.text = coinsCount.ToString();
    }

    public void RemoveCoins(int count)
    {
        coinsCount -= count;
        coinsCountText.text = coinsCount.ToString();
    }
}
