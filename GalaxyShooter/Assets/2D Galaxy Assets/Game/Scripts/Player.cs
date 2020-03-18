using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool canTripleShot = false;
    public bool canSpeedBoost = false;
    public bool shieldsActive = false;
    public int lives = 3;

    [SerializeField]
    private GameObject _explosionPlayer;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private GameObject _shieldGameObject;


    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if(_uiManager != null)
        {
            _uiManager.UpdateLives(lives);
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("Spane_Manager").GetComponent<SpawnManager>();
        if (_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutines();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
   
 
    }
    private void Shoot()
    {
        if (canTripleShot == true)
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
            {
                if (Time.time > _canFire)
                {
                    Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
                    _canFire = Time.time + _fireRate;
                }
            }
       
        }    
        else if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
           {
                if (Time.time > _canFire)
                {
                    Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
                    _canFire = Time.time + _fireRate;
                }

           }
        
    }


    private void Movement()
    {
        float horizontaInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if(canSpeedBoost == true)
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed * 3.0f *horizontaInput);
            transform.Translate(Vector3.up * Time.deltaTime * _speed * 3.0f * verticalInput);
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontaInput);
            transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalInput);
        }
        
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.25f)
        {
            transform.position = new Vector3(transform.position.x, -4.25f, 0);
        }
        else if (transform.position.x > 9.40f)
        {
            transform.position = new Vector3(-9.40f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.40f)
        {
            transform.position = new Vector3(9.40f, transform.position.y, 0);
        }

    }
    public void EnableShield()
    {
        shieldsActive = true;
        _shieldGameObject.SetActive(true);
    }
    public void Damage()
    {
        if(shieldsActive == true)
        {
            shieldsActive = false;
            _shieldGameObject.SetActive(false);
            return;
        }

        lives -= 1;
        _uiManager.UpdateLives(lives);
        if( lives < 1)
        {
            Instantiate(_explosionPlayer, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uiManager.ShowTitleScreen();
            Destroy(this.gameObject);
        }


    }

    public void SpeedBoostOn()
    {
        canSpeedBoost = true;
        StartCoroutine(SpeedBoostOf());
    }
    public IEnumerator SpeedBoostOf()
    {
        yield return new WaitForSeconds(2.0f);

        canSpeedBoost = false;
    }


    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);

        canTripleShot = false;
    }
}
