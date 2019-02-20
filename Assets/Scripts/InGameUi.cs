using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUi : MonoBehaviour
{
    public Text hpLossText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void DisplayFloatingDamage(Transform floatingHpPlace,float damage)
    {

        Vector3 displayPlace = Camera.main.WorldToScreenPoint(floatingHpPlace.transform.position);

        hpLossText.text = "-" + damage.ToString("0.0");
        Instantiate(hpLossText, displayPlace, Quaternion.identity,transform);
    }
}
