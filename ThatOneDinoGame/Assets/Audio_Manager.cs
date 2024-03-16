using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    public AudioClip music;
    public AudioSource audioSource;
    public int musicIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = music;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //if(!audioSource.isPlaying)    //Si jamais je veux plusieurs musique en mode playlist j'ai juste a mettre playlist[] au lieu de musique et a reactiver tout ce qui est en vert
        
        //PlayNextSong();
        
    }

    void PlayNextSong()
    {
        //musicIndex = (musicIndex + 1) % playlist.Length;
        //audioSource.clip = playlist[musicIndex];
        // audioSource.Play();
    }
}
