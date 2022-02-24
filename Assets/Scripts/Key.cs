﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float duration;
    [SerializeField] private GameObject key;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private PlayerController playerController;
    private Collider2D KeyCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = FindObjectOfType<PlayerController>();
        KeyCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("player"))
        {
            KeyCollider.enabled = false;
            playerController.KeyPickUp();
            SoundManager.Instance.Play(Sounds.KeyPick);
            rb.velocity = transform.up * Time.deltaTime * speed;
            StartCoroutine("FadeOutAnimation");
            Destroy(gameObject,1f); 
        }
    }

    private IEnumerator FadeOutAnimation()
    {
        float counter = 0;
        Color spriteColor = spriteRenderer.material.color;
        while(counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, counter);
            spriteRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            yield return null;
        }       
    }
}
