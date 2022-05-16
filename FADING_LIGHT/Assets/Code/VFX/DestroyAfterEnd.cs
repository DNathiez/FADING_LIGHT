using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterEnd : MonoBehaviour
{
    private ParticleSystem p;
    // Start is called before the first frame update
    void Start()
    {
        p = GetComponent<ParticleSystem>();

        StartCoroutine(WaitForDestroy());
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitWhile(() => p.isPlaying);
        Destroy(gameObject);
    }
}
