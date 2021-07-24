using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private PlayerController playerController;
    private Rigidbody2D rb;
    private Collider2D KeyCollider;
    [SerializeField] private GameObject key;
    [SerializeField] private float speed;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float duration;


    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        KeyCollider = GetComponent<BoxCollider2D>();
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("player"))
        {
            KeyCollider.enabled = false;
            playerController.KeyPickUp();
            rb.velocity = transform.up * Time.deltaTime * speed;
            StartCoroutine("FadeOutAnimation");
            Invoke(nameof(DestroyObject), 1f);
        }
    }

    private IEnumerator FadeOutAnimation()
    {
        float counter = 0;
        Color spriteColor = spriteRenderer.material.color;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, counter/duration);
            Debug.Log(alpha);
            spriteRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            yield return null;
        }       
    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
