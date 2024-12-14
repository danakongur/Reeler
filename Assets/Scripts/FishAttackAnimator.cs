using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FishAttackAnimator : MonoBehaviour
{
    public Vector3 struggleEnd = new Vector3(-5, 0, 0);
    public float struggleDuration = 1;
    public Vector3 healEnd = new Vector3(0, 0, 0);
    public float healDuration = 1;
    public Vector3 cleanseEnd = new Vector3(0, 0, 0);
    public float cleanseHeight = 0.5f;
    public float cleanseDuration = 1;
    public Vector3 retreatEnd = new Vector3(5, 0, 0);
    public float retreatDuration = 1;
    Color healColor = Color.green;
    Color cleanseColor = Color.magenta;

	public AudioClip damageSound;
    public AudioClip healingSound;
    public AudioSource audioSource;
    /*void OnEnable()
    {
        StartCoroutine(StruggleAnimation());
        
    }*/

	void Start() {
		if (audioSource == null) 
			audioSource = gameObject.AddComponent<AudioSource>();
	}

    public IEnumerator StruggleAnimation()
    {
        float timer = 0f;
        while (timer < struggleDuration)
        {
            timer += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(Vector3.zero, struggleEnd, timer / struggleDuration);
            yield return null;

        }

		Debug.Log("struggle damage sound");
		audioSource.PlayOneShot(damageSound);

        timer = 0f;
        while (timer < struggleDuration/2)
        {
            timer += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(struggleEnd, Vector3.zero, timer / (struggleDuration / 2));
            yield return null;

        }

        yield return null;
    }
    public IEnumerator HealAnimation()
    {
        float timer = 0f;
        SpriteRenderer fishSprite = transform.GetComponent<SpriteRenderer>();
        Color defaultColor = fishSprite.color; // respects gameobject chosen color
        audioSource.PlayOneShot(healingSound);
        while (timer < 1)
        {
            timer += Time.deltaTime / healDuration;

            if (timer < 0.5f)
            {
                float lerpValue = (timer) * 2;
                transform.localPosition = Vector3.Lerp(Vector3.zero, healEnd, lerpValue);
            }
            else
            {
                float lerpValue = (timer - 0.5f) * 2;
                transform.localPosition = Vector3.Lerp(healEnd, Vector3.zero, lerpValue);
            }


            fishSprite.color = Color.Lerp(defaultColor, healColor, timer);
            yield return null;

        }
        timer = 0f;
        while (timer < healDuration)
        {
            timer += Time.deltaTime;
            fishSprite.color = Color.Lerp(healColor, defaultColor, timer / healDuration);
            //transform.localPosition = Vector3.Lerp(Vector3.zero, healEnd, timer / healDuration);
            yield return null;

        }
        fishSprite.color = defaultColor;
        yield return null;
    }
    public IEnumerator CleanseAnimation()
    {
        float timer = 0f;
        SpriteRenderer fishSprite = transform.GetComponent<SpriteRenderer>();
        Color defaultColor = fishSprite.color; // respects gameobject chosen color
        while (timer < cleanseDuration)
        {
            timer += Time.deltaTime;
            fishSprite.color = Color.Lerp(defaultColor, cleanseColor, timer / cleanseDuration);
            //transform.localPosition = Vector3.Lerp(Vector3.zero, healEnd, timer / healDuration);
            yield return null;

        }
        timer = 0f;
        while (timer < cleanseDuration)
        {
            timer += Time.deltaTime;
            fishSprite.color = Color.Lerp(cleanseColor, defaultColor, timer / cleanseDuration);
            //transform.localPosition = Vector3.Lerp(Vector3.zero, healEnd, timer / healDuration);
            yield return null;

        }
        fishSprite.color = defaultColor;
        yield return null;
    }

public IEnumerator RetreatAnimation()
    {
        float timer = 0f;
        while (timer < retreatDuration)
        {
            timer += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(Vector3.zero, retreatEnd, timer / retreatDuration);
            yield return null;

        }
        yield return null;
    }


}
