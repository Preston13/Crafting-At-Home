using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseTrigger : MonoBehaviour
{
    public GameObject player;

    private Survivor survivor;

    // Start is called before the first frame update
    void Start()
    {
        survivor = player.GetComponent<Survivor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {
            if (survivor.curHealth < survivor.maxHealth)
            {
                survivor.inHome = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {
            survivor.inHome = false;
        }
    }
}
