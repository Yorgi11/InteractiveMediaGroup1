using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thermostat : MonoBehaviour
{
    public Manager manager;

    public float tempIncreaseAmount = 10;

    public bool on;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!on && collision.tag == "Player")
        {
            print("player");
            if (Input.GetKey(KeyCode.E))
            {
                on = true;
                manager.IncreaseTemp(tempIncreaseAmount);
            }
        }
    }
}
