using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Plant _targetPlant;
    private Movement _movement;


    bool Interact => Input.GetKeyDown(KeyCode.E);
    private bool _allowInput = true;

    public static Player Current;

    private void Awake()
    {
        Current = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Interact)
        {
            if (_targetPlant) ProcessPlant(_targetPlant);
        }
    }

    public void SetInputState(bool value)
    {
        _allowInput = value;
        _movement.enabled = value;
    }

    private void ProcessPlant(Plant plant)
    {
        plant.Float();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!_allowInput) return;
        if (Interact)
        {
            print("Try To interact with something");
        }
        if (other.gameObject.CompareTag("Bubble"))
        {

            var bubble = other.GetComponent<Bubble>();
            bubble.Pick();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Plant"))
        {
            var plant = other.GetComponent<Plant>();
            _targetPlant = plant;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Plant") && other.gameObject == _targetPlant.gameObject)
        {
            _targetPlant = null;
        }
    }
}
