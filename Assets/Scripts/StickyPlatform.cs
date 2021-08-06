using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("player"))
        {
            other.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            other.gameObject.transform.SetParent(null);
        }
    }
}
