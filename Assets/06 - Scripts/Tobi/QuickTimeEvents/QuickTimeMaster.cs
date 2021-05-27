using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTimeMaster : MonoBehaviour
{
    [SerializeField]
    private List<QT_TimedButton> quickTimeEvents;
    [SerializeField]
    private List<Collider> colliders;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(QT_TimedButton q in quickTimeEvents)
        {
            q.Run();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision Detected");
    }


}
