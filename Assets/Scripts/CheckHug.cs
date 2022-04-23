using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHug : MonoBehaviour
{
	public bool collided = false;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			collided = true;
		}
	}
	

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			collided = false;
		}
	}
}
