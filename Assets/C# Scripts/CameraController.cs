using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //serialize field for the camera controller to access from the editor
    [SerializeField] private GameObject player;

    //private fields for the camera controller
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }



    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
