﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TurnManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Command1()
    {
        Debug.Log("Command1");
        FindObjectOfType<NetworkAnimator>().SetTrigger("Pula");//pq so tem um
    }


    public void Command2()
    {
        Debug.Log("Command2");
        FindObjectOfType<NetworkAnimator>().SetTrigger("Pula");//pq so tem um
    }
}
