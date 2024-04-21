using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string[] names;

    [TextArea(3, 10)]
    public string[] sentences;

    public Sprite[] sprites;

    public AudioClip[] characterVoices;

}
