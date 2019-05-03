using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIMngr : MonoBehaviour
{
    public PlayerBHV pla = null;
    private TextMesh text;

    private void Start()
    {
        text = transform.GetComponent<TextMesh>();
    }

    private void Update()
    {
        if (!pla) Debug.Log("Player is null");
        else{
            text.text = "It is Player " + (pla.currentTurn + 1) + " Turn";
            Debug.Log("UI updated");
        }
    }
}
