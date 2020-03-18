
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyExplosion;
    [SerializeField]
    private float _speedEnemy = 3.0f;
    private UIManager _uiManager;


    // Update is called once per frame
    void Update()
    {
        float  x = Random.Range(-7.62f, 7.62f);

        transform.Translate(Vector3.down * _speedEnemy * Time.deltaTime);
        if (transform.position.y <= -6.5)
        {
            transform.position = new Vector3(x, 6.5f, 0);
        }
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
  
        if (other.tag == "Laser")
        {

            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }
            Destroy(other.gameObject);
            Instantiate(_enemyExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            _uiManager.UpdateScore();
        }

        else if(other.tag == "Player") 
        { 
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            Instantiate(_enemyExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            _uiManager.UpdateScore();
        }

    }
}
