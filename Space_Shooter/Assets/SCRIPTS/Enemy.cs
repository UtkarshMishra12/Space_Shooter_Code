using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 4f;
    private PlayerMovement _player;
    private Animator _anim;
    private AudioSource _audiosource;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        _audiosource = GetComponent<AudioSource>();
        if(_player== null)
        {
            Debug.Log("Player is Null");
        }
        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.Log("Anim is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
 
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(transform.position.y < -4.5f)
        {
            float DirectionX = Random.Range(-8f, 8f);
            transform.position = new Vector3(DirectionX, 10.5f, 0);
        }
     
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement player = other.transform.GetComponent<PlayerMovement>();
            
            if(player != null)
            {
                player.Damage();
            }
            _anim.SetTrigger("OnEnemyDeath");
            speed = 0;
            _audiosource.Play();
            Destroy(this.gameObject, 2.8f);
            
        }

        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.AddScore(10);
            }
            _anim.SetTrigger("OnEnemyDeath");
            speed = 0;
            _audiosource.Play();
            Destroy(this.gameObject, 2.8f);
        }

    }
}
