using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController _characterController;
    Rigidbody2D _rigidBody;
    Door _door;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float xAxis = 0f;
        float yAxis = 0f;

        if (Input.GetKey(KeyCode.A))
            xAxis--;
        if (Input.GetKey(KeyCode.D))
            xAxis++;

        if (Input.GetKey(KeyCode.W))
            yAxis++;
        if (Input.GetKey(KeyCode.S))
            yAxis--;

        _rigidBody.velocity = new Vector2(xAxis, yAxis).normalized * 5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
            _door = collision.GetComponent<Door>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
            _door = null;
    }
}
