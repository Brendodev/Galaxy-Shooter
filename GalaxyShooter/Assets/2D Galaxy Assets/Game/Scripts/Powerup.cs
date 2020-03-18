using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.name);

        if(other.tag == "Player")
        {
            //acess the player
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                if(powerupID == 0)
                {
                    //enable triple shot
                    player.TripleShotPowerupOn();
                }
                else if (powerupID == 1)
                {
                    //enable speed boost
                    player.SpeedBoostOn();
                }
                else if (powerupID == 2)
                {
                    player.EnableShield();
                    //enable shield boost
                    
                }


            }

            //destroy the powerup
            Destroy(this.gameObject);
        }

    }
}
