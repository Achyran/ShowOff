using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashGenerator : MonoBehaviour
{
    public float cooldown;
    public float speed;
    private float _cd;
    public Trash TrashPrefab;


    // Start is called before the first frame update
    private void Start()
    {
        EventManager.current.event_TrashCreated += CreatTrash;
        _cd = cooldown;
    }
    private void OnDestroy()
    {
        EventManager.current.event_TrashCreated -= CreatTrash;
    }

    // Update is called once per frame
    void Update()
    {
        if(_cd <= 0)
        {
            EventManager.current.TrashCreated();
            _cd = cooldown;
        }
        else
        {
            _cd -= Time.deltaTime;
        }
    }
    private void CreatTrash()
    {
        Trash _trash = Instantiate(TrashPrefab);
    }
    
}
