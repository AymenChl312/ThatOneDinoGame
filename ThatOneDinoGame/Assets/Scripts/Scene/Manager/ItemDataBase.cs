using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public Item[] allItems;
    public static ItemDataBase instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de ItemDataBase dans la scene.");
            return;
        }
        instance = this;
    }
}
