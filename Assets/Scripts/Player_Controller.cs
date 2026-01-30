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
   public void OnMove(InputAction.CallbackContext context)
{
    movementInput = context.ReadValue<Vector2>();
    Debug.Log("Move Input: " + movementInput);
}


    void FixedUpdate()
    {
        // Bewegung jetzt über movementInput statt Input.GetAxis
        Vector3 movement = new Vector3(movementInput.x, 0.0f, movementInput.y);
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
