using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyObjectWall : MonoBehaviour
{
    private bool isDestroyed = false;
    public Rigidbody2D rb;
    public Collider2D collider2d;
    public SpriteRenderer sprite;
    public int magnitude = 12;
    private CowController controller;
    public float fadeDuration;
    public Sprite[] spriteList;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CowController>();

        if (spriteList.Length > 0)
        {
            // Get a random index from the spriteList
            int randomIndex = Random.Range(0, spriteList.Length);

            // Set the sprite to the random one from the list
            sprite.sprite = spriteList[randomIndex];
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Floor"))
        {
            isDestroyed = true;
            rb.bodyType = RigidbodyType2D.Static;
            collider2d.enabled = false;
            StartCoroutine(FadeWall(0));
        }
        else if (collision.gameObject.tag.Equals("Player"))
        {
            collision.GetComponent<CowController>().LooseLife();
            StartCoroutine(FadeWall(0));
            //SoundManager.snd.PlayGlassDidntDestroySounds();
        }
    }

    private IEnumerator FadeWall(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SpriteRenderer spriteRenderer = sprite;

        if (spriteRenderer != null)
        {
            // Store the initial color
            Color initialColor = spriteRenderer.color;

            float elapsedTime = 0;
            yield return new WaitForSeconds(3f);
            while (elapsedTime < fadeDuration)
            {
                // Calculate the current alpha value based on the elapsed time
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

                // Set the new color with adjusted alpha
                Color newColor = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);

                // Apply the new color to the SpriteRenderer
                spriteRenderer.color = newColor;

                // Increment the elapsed time
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            // Ensure the final color is fully transparent
            spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
            this.gameObject.SetActive(false);
        }
    }
}