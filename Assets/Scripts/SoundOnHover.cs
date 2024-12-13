//code from https://gamedev.stackexchange.com/questions/150382/play-a-sound-when-moving-cursor-on-a-button
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundOnHover : MonoBehaviour, IPointerEnterHandler // required interface when using the OnPointerEnter method.
{
    public AudioClip AudioClip ;
    private AudioSource audioSource ;

    //Do this when the cursor enters the rect area of this selectable UI object.
    public void OnPointerEnter(PointerEventData eventData)
    {
        if( audioSource == null )
            audioSource = GetComponent<AudioSource>();
        if( audioSource == null )
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.PlayOneShot( AudioClip ) ;
    }
}