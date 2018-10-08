using UnityEngine;
using System.Collections;

public class DroppingPlatform : MonoBehaviour 
{
	Rigidbody2D rb2D;
	public GameObject particleFX;
	private Vector3 originalPosition;
	public float resetPositionAfterSeconds = 10f;

	private AudioSource auds;
	public AudioClip fallingSound;

	// Use this for initialization
	void Start () 
	{
		rb2D = gameObject.GetComponent<Rigidbody2D>();
		originalPosition = this.gameObject.transform.position;
		auds = this.gameObject.GetComponent<AudioSource>();

        rb2D.gravityScale = 0;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			//rb2D.useGravity = true;
			particleFX.SetActive(true);
			//Make falling sound
			auds.PlayOneShot(fallingSound);
			StartCoroutine(Delay(resetPositionAfterSeconds));
		}
	}

	void ResetPlatform()
	{
		//Turn Gravity Off
		//rb2D.useGravity = false;
		//Move Platform back to original position
		this.gameObject.transform.position = originalPosition;
		//Turn off "On" Particle Effect
		particleFX.SetActive(false);
	}

	IEnumerator Delay(float waitTime) 
	{
		yield return new WaitForSeconds(waitTime);
		ResetPlatform();
	}
}
