using UnityEngine;
using System.Collections;

public class bird_script : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public logicScript logic;
    public bool birdIsAlive = true;
    public Animator animator;
    public GameObject welcomePanel;
    public WelcomeFader welcomeFader;

    public static bool gameStarted = false; // Now static and public
    private static bool welcomeShown = false;
    public AudioSource audioSource;
    public AudioClip flapClip;

    void Start()
    {
        gameObject.name = "Flappy";
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<logicScript>();
        animator = GetComponent<Animator>();
        myRigidbody.simulated = false;

        // Load flap.mp3 from Resources if not assigned in Inspector
        if (flapClip == null)
            flapClip = Resources.Load<AudioClip>("flap");

        // Show welcome panel only once per app launch
        if (welcomePanel != null)
            welcomePanel.SetActive(!welcomeShown);

        // If welcome already shown (i.e., after restart), start game immediately
        if (welcomeShown)
        {
            StartGameImmediate();
        }
    }

    void Update()
    {
        if (!gameStarted)
        {
            if (!welcomeShown)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (welcomePanel != null && welcomeFader != null)
                    {
                        welcomeFader.FadeAndDestroy(() =>
                        {
                            welcomeShown = true;
                            StartGameImmediate();
                            myRigidbody.linearVelocity = Vector2.up * flapStrength;
                            animator.SetTrigger("Flap");
                            if (audioSource != null && flapClip != null)
                                audioSource.PlayOneShot(flapClip);
                        });
                    }
                    else
                    {
                        welcomeShown = true;
                        StartGameImmediate();
                        myRigidbody.linearVelocity = Vector2.up * flapStrength;
                        animator.SetTrigger("Flap");
                        if (audioSource != null && flapClip != null)
                            audioSource.PlayOneShot(flapClip);
                    }
                }
                return;
            }
            else
            {
                // If welcome already shown (i.e., restart), start immediately
                StartGameImmediate();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
        {
            myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocity.x, flapStrength);
            animator.SetTrigger("Flap");
            if (audioSource != null && flapClip != null)
                audioSource.PlayOneShot(flapClip);
        }

        float bottomEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        if (transform.position.y < bottomEdge)
        {
            logic.gameOver();
        }
    }

    private void StartGameImmediate()
    {
        gameStarted = true;
        myRigidbody.simulated = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.gameOver();
        birdIsAlive = false;
    }
}