using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpactEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroyImpactEffect());
    }

    private IEnumerator destroyImpactEffect()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
