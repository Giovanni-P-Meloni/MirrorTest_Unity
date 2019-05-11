using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{

    private PlayerBHV _myPlayer;
    private TextMesh text;

    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetComponent<TextMesh>();
        _myPlayer = transform.parent.GetComponent<PlayerBHV>(); 
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "It is Player " + (_myPlayer.currentTurn + 1).ToString() + " Turn";
    }
}
