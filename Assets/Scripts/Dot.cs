using UnityEngine;

public class Dot : MonoBehaviour
{

    public float width = 2.5f;
    float speed = 10f;

    void Update()
    {
        float distanceToMove = speed * Time.deltaTime;
        transform.Translate(Vector3.down * distanceToMove);
    }
}
