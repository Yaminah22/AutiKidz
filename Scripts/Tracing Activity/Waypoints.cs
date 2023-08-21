using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Waypoints : MonoBehaviour
{

	[SerializeField]
	Transform[] waypoints;

	[SerializeField]
	float moveSpeed = 2f;
	public int waypointIndex;
	public GameObject destroy_tracing;
	public GameObject activate_tracing;
	public GameObject Waypoint_object;
	public GameObject changeText;
	public bool tracing_two = false;
	public GameObject tracing_script;
	public GameObject prefab_ball;
	Vector3 ballposition;
	void Start()
	{

		if (Waypoint_object.name == "clicking") { 
			waypointIndex = 0;
		transform.position = waypoints[waypointIndex].transform.position; }
		else
			waypointIndex = -1;
		if (Waypoint_object.name == "SoccerBall") { ballposition = Waypoint_object.transform.position; };
	}

	void Update()
	{
		
			Move();
	}

	public void Move()
	{
		/*try
        {
			transform.position = Vector3.MoveTowards(transform.position,
												waypoints[waypointIndex].transform.position,
												moveSpeed * Time.deltaTime);
		}
		catch(IndexOutOfRangeException e)
        {
			Debug.Log("Index Out Of Range Exception has occurred: "+e);
        }
*/
		transform.position = Vector3.MoveTowards(transform.position,
												waypoints[waypointIndex].transform.position,
												moveSpeed * Time.deltaTime);

		if (transform.position == waypoints[waypointIndex].transform.position)
			waypointIndex += 1;
		{
		}

		if (waypointIndex == waypoints.Length && Waypoint_object.name=="clicking")
			waypointIndex = 0;

		if (waypointIndex == waypoints.Length && Waypoint_object.name == "SoccerBall")
		{
			Destroy(destroy_tracing);
			Waypoint_object.transform.parent.gameObject.SetActive(false);
			activate_tracing.SetActive(true);
			changeText.GetComponent<TextMeshProUGUI>().text = "TWO";
			tracing_script.GetComponent<Audio>().tracing_two = true;
			tracing_script.GetComponent<Audio>().tracefollowing();
			Instantiate(prefab_ball, ballposition, Quaternion.identity);
			Instantiate(prefab_ball, transform.position - Vector3.up+Vector3.left, Quaternion.identity);

		}


	}
	

}


