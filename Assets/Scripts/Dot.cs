using UnityEngine;
using TouchScript.Gestures;
using TouchScript.Hit;
using System;

public class Dot : MonoBehaviour
{

	public float speed = 10f;
	public float width = 2.5f;

	GameManager gameManager;

	void Start()
	{
		gameManager = FindObjectOfType<GameManager> ();
	}

	private void OnEnable() {
		Debug.Log ("ENABLE");
		GetComponent<TapGesture> ().Tapped += tappedHandler;
	}

	private void OnDisable() {
		GetComponent<TapGesture> ().Tapped -= tappedHandler;
		Debug.Log ("DISABLE");
	}

	private void tappedHandler(object sender, EventArgs e)
	{
		Debug.Log ("tapped handler");
		Destroy (gameObject);
//		Debug.Log ("TAP HANDLER");
//		Destroy (gameObject);
	}

	void Update()
	{
		// Move the object
		float distanceToMove = speed * Time.deltaTime;
        Vector3 currentPos = transform.position;
        Vector3 newPosition = currentPos + (Vector3.down * distanceToMove);
		transform.Translate(Vector3.down * distanceToMove);

        if (newPosition.y < gameManager.camBounds.minY) {
            Destroy(gameObject);
        }
	}
}
