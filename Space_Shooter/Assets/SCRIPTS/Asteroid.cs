using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private GameObject Explosion;
    private SpawnObject spawnObject;
    // Start is called before the first frame update
    private void Start()
    {
        spawnObject = GameObject.Find("Spawn_Manager").GetComponent<SpawnObject>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward*  _speed* Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            spawnObject.StartSpawning();
            Destroy(this.gameObject, 0.25f);

        }
    }
}
