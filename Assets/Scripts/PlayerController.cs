using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerNetBHV myPlayer;
    
    public void Command1()
    {
        myPlayer.CmdCommand1();
    }

    public void Command2()
    {
        myPlayer.Command2();
    }
}
