using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
 
    float dist;

    bool DoorOpened;
    bool playerOpens;
    bool playerCloses;

    bool PlayerInRange = false;

    bool UIEnabled = false;
 

    public Transform player;
    public GameObject buttonui;
       public KeyCode interactkey;

    public Animator animatorLeft;







    private void Start()
    {
        player = player.GetComponent<Transform>();
        buttonui = GameObject.Find(buttonui.name);
        buttonui.GetComponent<Canvas>().enabled = false;
        animatorLeft = animatorLeft.GetComponent<Animator>();
        DoorOpened = animatorLeft.GetBool("DoorOpened");
        playerOpens = animatorLeft.GetBool("playerOpens");
        playerCloses = animatorLeft.GetBool("playerCloses");
    }

    private void Update()
    {
        PlayerRange();
        AnimationController();
        UIController();
    }

    void AnimationController()
    {

        
        
        //Debug.Log("DoorOpened = " + DoorOpened);
        //Debug.Log("playerOpens = " + playerOpens);
        //Debug.Log("playerCloses = " + playerCloses);
        //Debug.Log("PlayerInRange = " + PlayerInRange);

        if (Input.GetKeyDown(interactkey) && PlayerInRange && DoorOpened)
        {
            playerOpens = false;
            playerCloses = true;
            DoorOpened = false;
            Debug.Log("this one doesnt though");   
        } else if (Input.GetKeyDown(interactkey) && PlayerInRange && !DoorOpened)
        {
            playerOpens = true;
            playerCloses = false;
            DoorOpened = true;

            Debug.Log("Thisworks");
        }
        animatorLeft.SetBool("playerOpens", playerOpens);
        animatorLeft.SetBool("playerCloses", playerCloses);
        animatorLeft.SetBool("DoorOpened", DoorOpened);

    }

    private void PlayerRange()
    {
        dist = Vector3.Distance(this.transform.position, player.transform.position);

        //Debug.Log("dist = " + dist);
        if (dist < 10)
        {
            PlayerInRange = true;

        }
        else
        {
            PlayerInRange = false;
        }

        //Debug.Log("PlayerInRange =  " + PlayerInRange);
        if (PlayerInRange)
        {
            buttonui.GetComponent<Canvas>().enabled = true;
            if(buttonui.GetComponent<Canvas>().enabled == true)
            {
                UIEnabled = true;
            }
        }
    }
    private void UIController()
    {
        
        if (!PlayerInRange)
        {
            UIEnabled = false;
        } else if (PlayerInRange)
        {

            UIEnabled = true;
        }
        

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


}
