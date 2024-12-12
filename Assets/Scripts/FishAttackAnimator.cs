using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FishAttackAnimator : MonoBehaviour
{
    public Vector3 struggleEnd = new Vector3(-5, 0, 0);
    public float struggleDuration = 1;
    public Vector3 healEnd = new Vector3(-5, 0, 0);
    public float healDuration = 1;
    public Vector3 cleanseEnd = new Vector3(-5, 0, 0);
    public float cleanseDuration = 1;
    public Vector3 retreatEnd = new Vector3(-5, 0, 0);
    public float retreatDuration = 1;
    /*void OnEnable()
    {
        StartCoroutine(StruggleAnimation());
        
    }*/

    public IEnumerator StruggleAnimation()
    {
        float timer = 0f;
        while (timer < struggleDuration)
        {
            timer += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(Vector3.zero, struggleEnd, timer / struggleDuration);
            yield return null;

        }
        yield return null;
    }
    public IEnumerator HealAnimation()
    {
        float timer = 0f;
        while (timer < healDuration)
        {
            timer += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(Vector3.zero, healEnd, timer / healDuration);
            yield return null;

        }
        yield return null;
    }
    public IEnumerator CleanseAnimation()
    {
        float timer = 0f;
        while (timer < cleanseDuration)
        {
            timer += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(Vector3.zero, cleanseEnd, timer / cleanseDuration);
            yield return null;

        }
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
