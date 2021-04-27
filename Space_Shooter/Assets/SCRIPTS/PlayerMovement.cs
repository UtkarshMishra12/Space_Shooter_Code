using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{    
    [SerializeField]
    private float speed = 3.5f;
    private float speedMultiplier = 2;
    [SerializeField]
    private GameObject Laser;
    private float canFire = -1f;
    [SerializeField]
    private float fireRate = 0.5f;
    [SerializeField]
    private int life = 3;
    private SpawnObject spawnObject;
    [SerializeField]
    private GameObject TripleShoot;

    private bool isShieldActive = false;
    private bool isTripleShootActive = false;
    private bool isSpeedBoostActive = false;
    [SerializeField]
    private GameObject ShieldVisualizer;
    [SerializeField]
    private int Score ;
    private UIManager _uimanager;

    [SerializeField]
    private GameObject _rightengine, _leftengine;

    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioSource _audiosource;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        spawnObject = GameObject.Find("Spawn_Manager").GetComponent<SpawnObject>();
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();

        _audiosource = GetComponent<AudioSource>();
        if(spawnObject == null)
        {
            Debug.LogError("The Spawn Manager Is NuLL");
        }
        if (_uimanager == null)
        {
            Debug.Log("Null");
        }
        if(_audiosource == null)
        {
            Debug.Log("Audio Source is null");
        }
        else
        {
            _audiosource.clip = _laserSoundClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        FiringLaser();
    }

    void Movement()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");

        Vector3 Direction = new Vector3(HorizontalInput, VerticalInput, 0);

        transform.Translate(Direction * speed * Time.deltaTime);
        

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -3.9f)
        {

            transform.position = new Vector3(transform.position.x, -3.9f, 0);
        }

        if (transform.position.x > 11.1f)
        {
            transform.position = new Vector3(-11.1f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.1f)
        {
            transform.position = new Vector3(11.1f, transform.position.y, 0);
        }
    }

    void FiringLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space)  && Time.time > canFire)
        {
            canFire = Time.time + fireRate;
            if (isTripleShootActive == true)
            {
                Instantiate(TripleShoot, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(Laser, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
            }
            _audiosource.Play();

        }
    }

    public void Damage()
    {
        if( isShieldActive == true)
        {
            isShieldActive = false;
            ShieldVisualizer.SetActive(false);
            return;
        }
        life--;
        if (life == 2)
        {
            _leftengine.SetActive(true);
        }
        else if (life == 1)
        {
            _rightengine.SetActive(true);
        }
        _uimanager.UpdateLives(life);

        if (life < 1)
        {
            spawnObject.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        isTripleShootActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(4.0f);
        isTripleShootActive = false;
    }

    public void SpeedBoostActive()
    {
        isSpeedBoostActive = true;
        speed *= speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
        speed /= speedMultiplier;
    }

    public void ShieldIsActive()
    {
        isShieldActive = true;
        ShieldVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        Score += points;
        _uimanager.UpdateScore(Score);
    }
}
