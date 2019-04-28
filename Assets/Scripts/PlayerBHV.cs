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
    
    private void Start()
    {
        text = transform.GetComponent<TextMesh>();
        text.text = "Player " + playerID.ToString();
    }

    private void Update()
    {
        if (!isLocalPlayer || !isMyTurn) return;

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        transform.position += new Vector3(horizontal, vertical, 0f) * speed;
    }

    [ClientRpc]
    public void RpcChangeMyTurn()
    {
        isMyTurn = !isMyTurn;        
    }
    


}
