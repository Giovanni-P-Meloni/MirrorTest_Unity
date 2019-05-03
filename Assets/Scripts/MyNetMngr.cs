using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetMngr : NetworkManager
{
    [Space]
    [Header("Additional")]
    public TurnMngr turnMngr;

    private int playersInServer;

    public override void Start()
    {
        base.Start();
        playersInServer = 0;
    }

    public override void OnServerAddPlayer(NetworkConnection conn, AddPlayerMessage extraMessage)
    {
        base.OnServerAddPlayer(conn, extraMessage);
        playersInServer++;//aumenta o numero de jogadores conectados
        turnMngr.AddPlayer(conn.playerController.gameObject.GetComponent<PlayerBHV>());//passa a referencia do jogador para o TurnManager
        conn.playerController.gameObject.GetComponent<PlayerBHV>().playerID = playersInServer;//Seta o ID do player
    }

    public override void OnServerRemovePlayer(NetworkConnection conn, NetworkIdentity player)
    {
        base.OnServerRemovePlayer(conn, player);
        playersInServer--;
        turnMngr.RemovePlayer(conn.playerController.gameObject.GetComponent<PlayerBHV>());
    }
}
