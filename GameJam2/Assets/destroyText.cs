using System.Collections;
using UnityEngine;

public class destroyText : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(WaitDestroy());
    }

    IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
