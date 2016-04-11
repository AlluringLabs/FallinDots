using UnityEngine;

public class Dot : MonoBehaviour
{

    public static int width = 15;
    float speed = 10;

    void Update()
    {
        float distanceToMove = speed * Time.deltaTime;
        transform.Translate(Vector3.down * distanceToMove);
    }
}
