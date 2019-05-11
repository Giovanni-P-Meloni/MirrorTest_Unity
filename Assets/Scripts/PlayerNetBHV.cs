using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerNetBHV : NetworkBehaviour
{
    public GameObject canvas;
    private TurnManager turnManager;
    // Start is called before the first frame update
    private void Awake()
    {
        turnManager = FindObjectOfType<TurnManager>();
    }

    void Start()
    {
        if (isLocalPlayer)
        {
            GameObject obj = Instantiate(canvas);
            obj.transform.GetChild(0).position += new Vector3(Random.Range(-100f, 100f), 0, 0);
            obj.GetComponent<PlayerController>().myPlayer = this;
        }
        
    }

    [Command]
    public void CmdCommand1()
    {
        turnManager.Command1();
    }
  
    public void Command2()
    {
        
        foreach (PlayerNetBHV p in FindObjectsOfType<PlayerNetBHV>())
        {
            Debug.Log("O player " + p + "é local:" + p.isLocalPlayer);
            p.CmdCommand1();
        }
        turnManager.Command2();
    }
}
