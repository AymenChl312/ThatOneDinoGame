using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    public GameObject monster;
    public void KillEnemy()
    {
        Destroy(monster);

    }
}
