using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
	public bool playerInRange;

    private void Update()
    {
	    if (playerInRange)
	    {
		    if (Input.GetKeyDown(KeyCode.E))
		    {
			    Interact();
		    }
	    }
    }

    public abstract void Interact();

    private void OnTriggerEnter(Collider other)
    {
	    if (other.gameObject == GameManager.Instance.player)
	    {
		    print("NPC: player is in range, press e to interact");
		    playerInRange = true;
	    }
    }

    private void OnTriggerExit(Collider other)
    {
	    if (other.gameObject == GameManager.Instance.player)
	    {
		    playerInRange = false;
	    }
    }
}
