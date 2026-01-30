using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // für neues Input System

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public Text timerText;
    private float movementX;
    private float movementY;
    private Rigidbody rb;

    private int count;
    public float timeLeft = 10.0f;

    // Neue Input System Variable für Bewegung
    private Vector2 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }

    // Neue Input System Callback
    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>(); // Convert the input value into a Vector2 for movement.
        movementX = movementVector.x;  // Store the X and Y components of the movement.
        movementY = movementVector.y;
    }


    void FixedUpdate()
    {
        // Bewegung jetzt über movementInput statt Input.GetAxis
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            SizeGrowOnPickup();
            ChangeColor();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 10)
        {
            winText.text = "You Win!";
        }
    }

    void SetTimerText()
    {
        timerText.text = "Time Left: " + Mathf.Round(timeLeft).ToString();
        if (timeLeft < 0)
        {
            winText.text = "You Lose!";
        }
    }

    void Timer()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            winText.text = "You Lose!";
            Destroy(this);
        }
    }

    void Update()
    {
        Timer();
        print("Move Y: " + movementY);
    }

    void ChangeColor()
    {
        GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
    }

    void SizeGrowOnPickup()
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }
}
