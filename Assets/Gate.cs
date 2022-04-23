using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
	public bool isCrossed;
	public TimerCountDown timer;

	private void Start()
	{
		timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerCountDown>();
	}
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			if(!isCrossed)
			{
				isCrossed = true;
				timer.AddTimer(10);
			}
		}
	}
}
