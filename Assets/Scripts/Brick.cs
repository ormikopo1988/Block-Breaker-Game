using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	public GameObject smoke;

	public static int breakableCount = 0;
	
	public AudioClip crack;

	public Sprite[] hitSprites;
	
	private int timesHit;

	private LevelManager levelManager;
	
	private bool isBreakable;
	
	// Use this for initialization
	void Start () {
		timesHit = 0;
		isBreakable = (this.tag == "Breakable");
		
		//keep track of breakable bricks
		if(isBreakable) {
			breakableCount++;
		}
		
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter2D (Collision2D collision) {
		AudioSource.PlayClipAtPoint(crack, transform.position);
		if(isBreakable) {
			HandleHit();
		}
	}
	
	void  HandleHit() {
		timesHit++;
		int maxHits = hitSprites.Length + 1;
		
		if(timesHit >= maxHits) {
			breakableCount--;
			//send message to the levelManager that a brick has been destroyed and he decides if we go to a next level or not
			levelManager.BrickDestroyed();
			
			PuffSmoke();
			
			Destroy(gameObject);
		} else {
			LoadSprites();	
		}
	}
	
	void PuffSmoke() {
		//instantiate gameobject smoke - make smoke appear
		GameObject smokePuff = Instantiate(smoke, gameObject.transform.position, Quaternion.identity) as GameObject;
		smokePuff.particleSystem.startColor = gameObject.GetComponent<SpriteRenderer>().color;
	}
	
	void LoadSprites() {
		int spriteIndex = timesHit - 1;
		
		//load the correct sprite now
		if(hitSprites[spriteIndex]) {
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		}
		
	}
	
	// TODO - Remove this method once we can actually win
	void SimulateWin() {
		levelManager.LoadNextLevel();
	}
}
