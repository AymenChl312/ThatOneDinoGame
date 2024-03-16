using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadSpecificScene : MonoBehaviour
{
    public string sceneName;
    public Animator animator;

    private void Awake()
    {
        animator = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(loadNextScene());
        }
    }

    public IEnumerator loadNextScene()
    {
        animator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
