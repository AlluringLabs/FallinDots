using UnityEngine;
using System.Collections;

public class Dot : MonoBehaviour
{
	
	public ParticleSystem deathParticles;
    public float width = 2.5f;

    float speed = 5f;
    GameManager gameManager;

    void Start() {
        gameManager = FindObjectOfType<GameManager>();
		deathParticles = GetComponent<ParticleSystem>();
    }

    void Update() {
        float distanceToMove = speed * Time.deltaTime;
        Vector3 currentPos = transform.position;
        Vector3 newPosition = currentPos + (Vector3.down * distanceToMove);

        if (Input.GetMouseButtonDown(0)) {
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (clickHappenedInDot(clickPos)) {
                Destroy(gameObject);
            }
        }

        if (newPosition.y < gameManager.camBounds.minY) {
            Destroy(gameObject);
        }
        else {
            transform.Translate(Vector3.down * distanceToMove);
        }
    }

    bool clickHappenedInDot(Vector3 clickPos) {
        Vector3 currentPos = transform.position;

        return clickPos.x > currentPos.x - width/2 &&
            clickPos.x < currentPos.x + width/2 &&
            clickPos.y > currentPos.y - width/2 &&
            clickPos.y < currentPos.y + width/2;
    }
}
