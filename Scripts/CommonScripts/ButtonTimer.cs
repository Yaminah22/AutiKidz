using UnityEngine;
using System.Collections;

public class ButtonTimer : MonoBehaviour
{

	public float inputTimer; //the timer begins at 0
	public float frequency; //the frequency the timer is checked, if you need to check the timer more frequently then try 0.5f or 0.25f or even 0.1f, but 1f should be sufficient.
	public float timeLimit; //the limit of the timer 
	public Animator helpHand;
	public bool timing;
	public bool noInput;

	void Start()
	{ //none of these things need to be set at start, they can be set elsewhere if need be

		inputTimer = 0f;
		frequency = 2f;
		timeLimit = 10f;

	}

	void Update()
	{

		if (!noInput && !timing)
		{ //first, the script makes sure the timer hasn't already begun or elapsed
			if (!Input.anyKey)
			{ //then, if there is no input
				timing = true; //the timing begins
				StartCoroutine(InputTimer()); //and the coroutine is called
			}
		}

		if (Input.anyKey)
		{ //if input is ever detected
			timing = false; //the timing ends
			noInput = false; //the "noInput" status is reset (if it had been activated in the first place)
			inputTimer = 0f; //the timer is also reset to 0, this is useful if the timer needs to be accessed by another portion of the script but this can be removed because the timer also sets itself to 0 at the start of the coroutine
			helpHand.SetBool("idle", false);
			StopCoroutine(InputTimer()); //and the coroutine is ended
			
		}
	}

	IEnumerator InputTimer()
	{

		inputTimer = 0f; //timer resets at the start of the coroutine

		while (timing)
		{ //only workins while timing
			while (inputTimer < timeLimit)
			{ //while the timer is below the timeLimit
				inputTimer += frequency; //the timer counts upward at the same rate it is checked
				yield return new WaitForSeconds(frequency); // 1 second is sufficient but it can check every half or quarter or whatever you need
			}

			if (inputTimer >= timeLimit)
			{ // once the timer has exceeded the timeLimit
				noInput = true; //it tells the script that there has been no input
				timing = false; //stops timing (because there's no need once it has determined there's no input)
				Debug.Log("No input!"); //I just left this here while testing
				helpHand.SetBool("idle", true);
				yield return noInput;
			}
		}
	}
}