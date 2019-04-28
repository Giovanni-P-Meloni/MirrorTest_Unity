using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnMngr : NetworkBehaviour
{
    [SerializeField]
    private GameObject[] playersOnServer;

    public bool waiting;
    private TextMesh text;
    private int plyr_nbr;

    [SyncVar]
    public int currentTurn;

    void OnChangeTurn(int newTurn)
    {
        foreach (GameObject GO in playersOnServer)
        {
            GO.GetComponent<PlayerBHV>().currentTurn = newTurn;
        }
    }

    private void Start()
    {
        text = transform.GetComponent<TextMesh>();
        playersOnServer = new GameObject[2];
        plyr_nbr = 0;
        currentTurn = 0;
        

        Invoke("ChangingTurns", 3);
    }
    
    /*[ClientCallback]
    private void Update()
    {
        text.text = "It is Player " + (currentTurn + 1).ToString() + " Turn";
    }
    */
    public void AddPlayer(GameObject playerGO)
    {
        playersOnServer[plyr_nbr] = playerGO;
        plyr_nbr++;
        
    }

    private void ChangingTurns()
    {
        if (currentTurn == -1) return;
 
        TurnWorks(); 
    }

    void TurnWorks()
    {
        print(playersOnServer[currentTurn]);
        if (!playersOnServer[currentTurn]) return;//nao permite null
        playersOnServer[currentTurn].GetComponent<PlayerBHV>().RpcChangeMyTurn();
        StartCoroutine(WaitForTurnEnd());       
        
    }

    IEnumerator WaitForTurnEnd()
    {
        
        yield return new WaitForSeconds(5);
        playersOnServer[currentTurn].GetComponent<PlayerBHV>().RpcChangeMyTurn();
        currentTurn = ((currentTurn + 1) % (plyr_nbr));
        ChangingTurns();
    }

}
