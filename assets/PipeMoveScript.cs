using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    public float moveSpeed = 8;
    public float deadZone = -45;
    private logicScript logic;
    private float currentSpeed;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<logicScript>();
        currentSpeed = moveSpeed;
    }

    void Update()
    {
        // Don't move pipes before the game starts
        if (!bird_script.gameStarted)
            return;

        // Calculate speed multiplier based on score thresholds: 10, 20, 40, 80, ...
        float speedMultiplier = 1f;
        if (logic != null)
        {
            int threshold = 10;
            int multiplier = 0;
            int score = logic.playerScore;
            while (score >= threshold)
            {
                multiplier++;
                threshold *= 2;
            }
            speedMultiplier = Mathf.Pow(1.2f, multiplier); // 20% faster at each threshold
        }

        // If the bird is dead, gradually slow down the pipes
        if (logic != null && logic.bird != null && !logic.bird.birdIsAlive)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, Time.deltaTime * 0.5f);
        }
        else
        {
            currentSpeed = moveSpeed * speedMultiplier;
        }

        transform.position += Vector3.left * currentSpeed * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}