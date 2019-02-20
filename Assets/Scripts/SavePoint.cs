using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : Interactable
{
	
	
	private const string _playerSaveCoordinate = "PlayerLastCoordinate";

	private void Start()
	{
		GameManager.Instance.onPlayerDieCallback += SpawnPlayer;
	}

	public override void Interact()
	{
		print("Saving player progress");
	    GameManager.Instance.CreatePlayerSaveSpot();
    }

	public void SpawnPlayer()
	{
		Debug.Log("spawning player");
		string encryptedCoordinate = PlayerPrefs.GetString(_playerSaveCoordinate);
		string[] encryptedConponent = encryptedCoordinate.Split(',');
		float x = float.Parse(encryptedConponent[0]);
		float y = float.Parse(encryptedConponent[1]);
		float z = float.Parse(encryptedConponent[2]);
		Vector3 playerLastCoordinate = new Vector3(x,y,z);
		GameManager.Instance.player.transform.position = playerLastCoordinate;
	}
}
