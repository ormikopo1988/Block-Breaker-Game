using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name) {
		Debug.Log("Level load requested for " + name);
		Brick.breakableCount = 0;
		Application.LoadLevel(name);
	}
	
	public void QuitRequest() {
		Debug.Log("Quit game reguested!");
		Application.Quit();
	}
	
	public void LoadNextLevel() {
		Brick.breakableCount = 0;
		//catch the next level in the sequence in which we have put our levels in the build settings and load it automatically
		Application.LoadLevel(Application.loadedLevel + 1);
	}
	
	public void BrickDestroyed() {
		//if last brick destroyed
		if(Brick.breakableCount <= 0) {
			LoadNextLevel();
		}
	}
}
