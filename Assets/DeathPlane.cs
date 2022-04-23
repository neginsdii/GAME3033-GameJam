using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
	public Transform SpawnPoint;
	public AudioSource audioSource;
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			other.gameObject.transform.position = SpawnPoint.position;
			other.gameObject.transform.rotation = SpawnPoint.rotation;
			if (!audioSource.isPlaying)
				audioSource.Play();
		}
	}
}
