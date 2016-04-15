using UnityEngine;

public class Dot : MonoBehaviour
{
    // Events
    public delegate void OnDestroyedHandler(GameObject gameObject, bool playerDidDestroy);
    public event OnDestroyedHandler OnDestroy;

    public float width = 2.5f;

    float speed = 10f;
    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if(!gameManager.isPaused) {
            float distanceToMove = speed * Time.deltaTime;
            Vector3 currentPos = transform.position;
            Vector3 newPosition = currentPos + (Vector3.down * distanceToMove);

            if (Input.GetMouseButtonDown(0)) {
                Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (clickHappenedInDot(clickPos)) {
                    Destroy(gameObject);
                    // OnDestroy(gameObject, true);
                }
            }

            if (newPosition.y < gameManager.camBounds.minY) {
                Destroy(gameObject);
                // OnDestroy(gameObject, false);
            }
            else {
                transform.Translate(Vector3.down * distanceToMove);
            }
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
