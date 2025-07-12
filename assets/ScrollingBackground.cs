using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed = 2f;
    private Vector3 startPosition;
    public float tileWidth = 35.56f ; // Set this to the width of your background image in world units

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileWidth);
        transform.position = startPosition + Vector3.left * newPosition;
    }
}
