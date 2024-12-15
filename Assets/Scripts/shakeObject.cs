using UnityEngine;
using System.Collections;

//found online in a unity forum by user Stardog
public class shaker : MonoBehaviour
{
    public float Inbetweenshakes;
    public float movementDistance;
	IEnumerator coroutine;
    void Start()
    {
		coroutine = Tremble();
        StartCoroutine(coroutine);
    }
	void OnEnable(){
		if (coroutine != null)
			StopCoroutine(coroutine);
		coroutine = Tremble();
		StartCoroutine(coroutine);
	}

    IEnumerator Tremble()
    {
        while (true) { 
            transform.position += new Vector3(0, movementDistance, 0);
			Debug.Log($"changing position from {transform.position}");
            yield return new WaitForSeconds(Inbetweenshakes);
            transform.position -= new Vector3(0, movementDistance, 0);
			Debug.Log($"changing position from {transform.position}");
            yield return new WaitForSeconds(Inbetweenshakes);
        }
    }
}


