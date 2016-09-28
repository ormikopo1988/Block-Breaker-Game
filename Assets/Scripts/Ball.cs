using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	//here we try to link it with the ball programmatically - to use it for the prefabs
	private Paddle paddle;
	
	private Vector3 paddleToBallVector;
	
	private bool hasGameStarted = false;

	// Use this for initialization
	void Start () {
		//now we do not have to associate the ball with the paddle every time from the editor we ask the GameObject to find an object of type Paddle
		paddle = GameObject.FindObjectOfType<Paddle>();
		paddleToBallVector = this.transform.position - paddle.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(!hasGameStarted) {
			//Lock the ball relative to the paddle
			this.transform.position = paddle.transform.position + paddleToBallVector;
			
			//Wait for a left mouse click to launch the ball
			if(Input.GetMouseButtonDown(0)) {
				hasGameStarted = true;
				this.rigidbody2D.velocity = new Vector2(2f, 10f);
			}
		}
	}
	
	void OnCollisionEnter2D (Collision2D collision) {
		//Ball does not trigger sound when brick is destroyed.
		//Not 100% sure why, possibly because brick is not there
		
		//Change ball's velocity randomly
		Vector2 tweak = new Vector2(Random.Range(0f, 0.2f), Random.Range(0f, 0.2f));
		
		if(hasGameStarted) {
			audio.Play();
			this.rigidbody2D.velocity += tweak;
		}
	}
}
