using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUI : MonoBehaviour
{

    public Text scoreText;
    public Text lostText;

    void Start()
    {
//        Dot dot = GetComponent<Dot>();
//        dot.OnDestroy += new Dot.DestroyedHandler(HandleDotDestroyed);
    }

    void Update()
    {

    }

    void HandleDotDestroyed(GameObject gameObject, bool playerDidDestroy)
    {
        if (playerDidDestroy) {
            UpdateScore(1);
        }
        else {
            UpdateLost();
        }
    }

    public void UpdateScore(int newScoreText)
    {
        scoreText.text += newScoreText;
    }

    public void UpdateLost()
    {

    }

}
