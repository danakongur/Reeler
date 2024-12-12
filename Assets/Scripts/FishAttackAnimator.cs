using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FishAttackAnimator : MonoBehaviour
{
    public Vector3 struggleEnd = new Vector3(-5, 0, 0);
    public float struggleDuration = 1;
    void OnEnable()
    {
        StartCoroutine(StruggleAnimation());
        
    }

    IEnumerator StruggleAnimation()
    {
        float timer = 0f;
        while (timer < struggleDuration)
        {
            timer += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(Vector3.zero, struggleEnd,timer/struggleDuration);
            yield return null;

        }
        yield return null;
    }
}
