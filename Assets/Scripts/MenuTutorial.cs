using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If the player clicks anywhere while active, the Tutorial panel will be disabled
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
        }
    }
}
