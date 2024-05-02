using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSkin : MonoBehaviour
{
    public int skinNr;
    public Skins[] skins;
    SpriteRenderer spriteRenderer;

    public static PowerUpSkin instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PowerUpSkin dans la scene.");
            return;
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (skinNr > skins.Length-1) skinNr = 0;
        else if (skinNr < 0) skinNr = skins.Length-1;
    }

    void LateUpdate()
    {
        SkinChoice();
    }
    
    void SkinChoice()
    {
        if (spriteRenderer.sprite.name.Contains("DinoRouge"))
        {
            string spriteName = spriteRenderer.sprite.name;
            spriteName = spriteName.Replace("DinoRouge_","");
            int spriteNr = int.Parse(spriteName);

            spriteRenderer.sprite = skins[skinNr].sprites[spriteNr];
        }
    }

    [System.Serializable]
    public struct Skins{
        public Sprite[] sprites;
    }
}
