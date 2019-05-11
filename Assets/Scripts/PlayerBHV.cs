using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerBHV : NetworkBehaviour
{
    public GameObject _myUI; 
    private TextMesh text;
    public float speed = 1.0f;
    

    [Space]
    [Header("Network properties")]
    [SyncVar]
    public bool isMyTurn = false;
    [SyncVar]
    public int playerID;
    public int currentTurn;

    private void Start()
    {
        text = transform.GetComponent<TextMesh>();
        text.text = "Player " + playerID.ToString();
        Initialize();
    }
    
    private void Initialize()
    {
        Instantiate(_myUI, transform);
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
