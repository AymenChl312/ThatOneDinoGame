using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadSpecificScene : MonoBehaviour
{
    public string sceneName;
    private Animator animator;

    private void Awake()
    {
        animator = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(loadNextScene());
            if (CurrentSceneManager.instance.active == true)
            {
                CurrentSceneManager.instance.active = false;
                CurrentSceneManager.instance.powerUpActive();
            }
            if(CurrentSceneManager.instance.temporange == true)
            {
                PlayerEffects.instance.deadPlayerOff(300);
            }
        }
    }

    public IEnumerator loadNextScene()
    {
        LoadAndSaveData.instance.SaveData();
        animator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
