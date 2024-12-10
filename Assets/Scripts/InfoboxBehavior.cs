using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoboxBehavior : MonoBehaviour
{
    public TMPro.TextMeshProUGUI fishNameTextComponent; // The fish name text component
    public TMPro.TextMeshProUGUI fishHabitatTextComponent; // The fish habitat text component
    public TMPro.TextMeshProUGUI fishDietTextComponent; // The fish diet text component
    public TMPro.TextMeshProUGUI fishBehaviorTextComponent; // The fish behavior text component
    public TMPro.TextMeshProUGUI fishConservationStatusTextComponent; // The fish conservation status text component
    public TMPro.TextMeshProUGUI fishSellValueTextComponent; // The fish sell value text component

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If the player clicks anywhere while active, the InfoBoxPanel will be disabled
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
        }
    }
}
