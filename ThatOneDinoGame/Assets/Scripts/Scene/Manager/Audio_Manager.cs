using UnityEngine;
using UnityEngine.Audio;

public class Audio_Manager : MonoBehaviour
{
    public AudioClip music;
    public AudioSource audioSource;
    public int musicIndex = 0;
    public AudioMixerGroup soundEffectMixer;
    public bool stop=false;

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
    public void Start()
    {
        audioSource.clip = music;
        audioSource.Play();
    }

    

    public AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = pos;
        AudioSource audioSource = tempGO.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = soundEffectMixer;
        audioSource.Play();
        if(stop)
        {
            Destroy(tempGO, 0.5f); 
            stop = false; 
        }
        else
        {
            Destroy(tempGO, clip.length);
        } 
        return audioSource;
    }
}

