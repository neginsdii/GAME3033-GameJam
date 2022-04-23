using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	public Vector3[] points;
	public int pointNumber = 0;
	private Vector3 currentTarget;

	public float tolerance;
	public float speed;
	public float delayTime;

	private float delay_Start;

	public bool auto;

	private void Start()
	{
		if(points.Length>0)
		{
			currentTarget = points[0];
		}
		tolerance = speed * Time.deltaTime;
	}

	private void Update()
	{
		if(transform.position != currentTarget)
		{
			MoveEnemy();
		}
		else
		{
			updateTarget();
		}
	}

	private void MoveEnemy()
	{
		Vector3 dir = currentTarget - transform.position;
		transform.position += (dir.normalized) * speed * Time.deltaTime;
		if(dir.magnitude<tolerance)
		{
			transform.position = currentTarget;
			delay_Start = Time.time;
		}
	}

	 private void updateTarget()
	{
		if(auto)
		{
			if(Time.time - delay_Start> delayTime)
			{
				NextPlatform();
			}
		}
	}

	private void NextPlatform()
	{
		pointNumber++;
		if (pointNumber >= points.Length)
			pointNumber = 0;
		currentTarget = points[pointNumber];
	}
}
