using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerBHV : NetworkBehaviour
{
    [SyncVar]
    public int playerID;
    private TextMesh text;
    [SyncVar]
    public bool isMyTurn = false;
    public float speed = 1.0f;
    public int currentTurn;
    [SerializeField]
    private GameObject objUI;
    public TurnFinishedDelegate SendEndTurn;
    
    private void Start()
    {
        text = transform.GetComponent<TextMesh>();
        text.text = "Player " + playerID.ToString();
        GameObject obj = GameObject.Instantiate(objUI, GameObject.Find("Canvas").transform);
        obj.GetComponent<UIMngr>().pla = this;
        SendEndTurn = null;
    }

    private void Update()
    {
        if (!isLocalPlayer || !isMyTurn) return;

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        transform.position += new Vector3(horizontal, vertical, 0f) * speed;

        if(Input.GetKeyDown(KeyCode.Space))
            CmdEndTurn();
    }

    [ClientRpc]
    public void RpcChangeMyTurn(TurnFinishedDelegate del)
    {
        isMyTurn = !isMyTurn;
        SendEndTurn = del;
    }

    [Command]
    private void CmdEndTurn(){
        if(SendEndTurn != null)
            SendEndTurn(this);
    }
    

    [ClientRpc]
    public void RpcOnChangeTurn(int newTurn)
    {
        if(isLocalPlayer)
            currentTurn = newTurn;
    }
}
