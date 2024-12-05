using UnityEngine;
using System.Collections;

//found online in a unity forum by user Stardog
public class shaker : MonoBehaviour
{
    public float Inbetweenshakes;
    public float movementDistance;
    private void Start()
    {
        StartCoroutine(Tremble());
    }
    IEnumerator Tremble()
    {
        while (true) { 
        {
            transform.position += new Vector3(0, movementDistance, 0);
            yield return new WaitForSeconds(Inbetweenshakes);
            transform.position -= new Vector3(0, movementDistance, 0);
            yield return new WaitForSeconds(Inbetweenshakes);
        }
        }
    }
}