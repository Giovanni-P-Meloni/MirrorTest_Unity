using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerNetBHV : NetworkBehaviour
{
    public GameObject canvas;
    public GameObject battler;
    public List<GameObject> battlers = new List<GameObject>();
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


            CmdSpawnObject();
            //CmdSpawnObject(battler);
            

        }
        
    }

    [Command]
    //private void CmdSpawnObject(GameObject obj)
    private void CmdSpawnObject()
    {
        
        //turnManager.SpawnObj(battler, this);

        GameObject obj2 = Instantiate(battler);
        battlers.Add(obj2);
        //pNet.battlers.Add(obj2);
        NetworkServer.Spawn(obj2);
    }

    private void Update()
    {
        
        foreach (GameObject obj in battlers)
        {
            obj.transform.position += new Vector3(Random.Range(-1f, 1f), 0, 0);
        }
    }

    [Command]
    public void CmdCommand1()
    {
        Debug.Log("Command1 from player");
        turnManager.Command1();
    }
  
    public void Command2()
    {
        
        foreach (PlayerNetBHV p in FindObjectsOfType<PlayerNetBHV>())
        {
            Debug.Log("O player " + p + "é local:" + p.isLocalPlayer);
            //p.CmdCommand1();
        }
        turnManager.Command2();
    }
}
