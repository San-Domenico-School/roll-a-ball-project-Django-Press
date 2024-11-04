using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private float speed;       //how hard the ball is pushed
    private float xDirection;  //move the ball left and right
    private float zDirection;  //move the ball forwards and backwards
    private int count;         //Count the pickup



    // Start is called before the first frame update
    void Start()
    {
        speed = 2;
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
        MoveBall();
    }

    private void MoveBall()
    {
        Vector3 direction = new Vector3(xDirection, 0, zDirection);
        GetComponent<Rigidbody>().AddForce(direction * speed);
    }

    //listen for player pressing arrow or WASD keys
    private void GetPlayerInput()
    {
        xDirection = Input.GetAxis("Horizontal");
        zDirection = Input.GetAxis("Vertical");
    }

    private void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
        count = count + 1;
        SetCountText();
    }




}