using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TurnFinishedDelegate(PlayerBHV player);

public class TurnMngr : NetworkBehaviour
{
    [SerializeField]
    private List<PlayerBHV> playersOnServer;

    public bool waiting;
    private TextMesh text;
    private bool playerIsActing;

    [SyncVar]
    public int currentTurn;


    private void Start()
    {
        text = transform.GetComponent<TextMesh>();
        playersOnServer = new List<PlayerBHV>();
        currentTurn = 0;
    }
    
    public void AddPlayer(PlayerBHV player)
    {
        playersOnServer.Add(player);
        if(playersOnServer.Count > 0)
            TurnWorks();
    }

    public void RemovePlayer(PlayerBHV player){
        if(currentTurn >= playersOnServer.IndexOf(player))
            currentTurn--;
        playersOnServer.Remove(player);
    }

    void TurnWorks()
    {
        foreach(PlayerBHV player in playersOnServer){
            player.RpcOnChangeTurn(currentTurn);
        }
        print(playersOnServer[currentTurn]);
        playersOnServer[currentTurn].RpcChangeMyTurn(EndCurrentTurn);
        StartCoroutine(WaitForTurnEnd());
    }

    public void EndCurrentTurn(PlayerBHV player){
        if(player.isMyTurn){
            playerIsActing = false;
            playersOnServer[currentTurn].RpcChangeMyTurn(null);
            currentTurn = ((currentTurn + 1) % (playersOnServer.Count));
            TurnWorks();
        }
    }

    IEnumerator WaitForTurnEnd()
    {
        if(playerIsActing){
            yield return new WaitForSeconds(5);
            EndCurrentTurn(playersOnServer[currentTurn]);
        }
    }

}
