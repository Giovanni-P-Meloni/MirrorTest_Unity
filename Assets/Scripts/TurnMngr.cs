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


    void OnChangeTurn()
    {
        foreach (GameObject GO in playersOnServer)
        {
            GO.GetComponent<PlayerBHV>().currentTurn = this.currentTurn;
        }
    }

    private void Start()
    {
        text = transform.GetComponent<TextMesh>();
        playersOnServer = new GameObject[2];
        plyr_nbr = 0;
        currentTurn = 0;
        
        //delay to start the game
        Invoke("ChangingTurns", 3);
    }
    
    public void AddPlayer(GameObject playerGO)
    {
        playersOnServer[plyr_nbr] = playerGO;
        plyr_nbr++;
        
    }

    //When a player leaves the server, its reference must be removed
    public void RemovePlayer()
    {
        
    }

    private void ChangingTurns()
    {
        if (currentTurn == -1) return;

        OnChangeTurn();
        TurnWorks(); 
    }

    void TurnWorks()
    {

        print(playersOnServer[currentTurn]);//Debug
        if (!playersOnServer[currentTurn]) return;//doesn't allow null
        playersOnServer[currentTurn].GetComponent<PlayerBHV>().RpcChangeMyTurn();//Start player turn
        StartCoroutine(WaitForTurnEnd());      
        
    }

    //
    IEnumerator WaitForTurnEnd()
    {
        
        yield return new WaitForSeconds(5);//turn time
        playersOnServer[currentTurn].GetComponent<PlayerBHV>().RpcChangeMyTurn();//End player turn
        currentTurn = ((currentTurn + 1) % (plyr_nbr));//change turn
        ChangingTurns();
    }

}
