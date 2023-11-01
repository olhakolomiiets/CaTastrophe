using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNightCityBehaviour : MonoBehaviour 
{
    [SerializeField]
    Vector2 forceDir;
    [SerializeField]
    int spin;
    Rigidbody2D rb;
    Collider2D collider2d;
    public float fadeDuration = 1.0f;
    [SerializeField] bool isCatDestroy;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
        rb.AddForce(forceDir*8);
        rb.AddTorque(spin);
        if (isCatDestroy)
        {
            StartCoroutine(FadeBall(10));
        }
        else 
        {
            StartCoroutine(FadeBall(10));
        }
    }

    private IEnumerator FadeBall(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();

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
        }
    }

    public void MakeBallPuff()
    {
        StartCoroutine("BallPuff");
    }
        
    public IEnumerator BallPuff()
    {
        yield return new WaitForSeconds(3);
        GameObject ballPuff = ObjectPooler.SharedInstance.GetPooledObject("EnemySteam");
        if (ballPuff != null)
        {
            ballPuff.transform.position = this.transform.position;
            ballPuff.SetActive(true);
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }
}

