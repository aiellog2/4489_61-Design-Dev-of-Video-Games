using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatorStart : MonoBehaviour
{

    [SerializeField] float speed = 2f;
    public GameObject floor;


    private void OnTriggerEnter(Collider other)
    {
      Debug.Log("We have collided!!!");
      floor.transform.position = Vector3.up * Time.deltaTime;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
