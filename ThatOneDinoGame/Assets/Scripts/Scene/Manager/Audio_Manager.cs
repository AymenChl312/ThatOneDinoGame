using UnityEngine;
using UnityEngine.Audio;

public class Audio_Manager : MonoBehaviour
{
    public AudioClip music;
    public AudioSource audioSource;
    public int musicIndex = 0;
    public AudioMixerGroup soundEffectMixer;

    public static Audio_Manager instance;

    private void Awake()
    {

        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Audio_Manager dans la scene.");
            return;
        }
        instance = this;

    }

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

    public AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = pos;
        AudioSource audioSource = tempGO.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = soundEffectMixer;
        audioSource.Play();
        Destroy(tempGO, clip.length);
        return audioSource;
    }
}
