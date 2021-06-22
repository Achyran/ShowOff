using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerController : MonoBehaviour
{
    private ContainerGrab[] containers;
    private List<int> notDone;
    private int currenIndex;

    private void Start()
    {
        containers = FindObjectsOfType<ContainerGrab>();
        notDone = new List<int>();
        int index = 0;
        foreach (ContainerGrab c in containers)
        {
            notDone.Add(index);
            index++;
        }
        StartNext();
    }

    private void Update()
    {
        switch (containers[currenIndex].state)
        {
            case ContainerGrab.State.ready:
                StartNext();
                break;
            case ContainerGrab.State.running:
                break;
            case ContainerGrab.State.done:
                notDone.Remove(currenIndex);
                StartNext();
                break;
            default:
                break;
        }
        if(notDone.Count == 0)
        {
            Destroy(gameObject);
        }
    }

    private void StartNext()
    { 
        int index = RandomIndex();
        if (index == -1) return;
        currenIndex = notDone[index];
        containers[currenIndex].StartAnimation();
    }

    private int RandomIndex()
    {
        if (notDone.Count == 0) return -1;
        return Random.Range(0, notDone.Count);
    }
}
