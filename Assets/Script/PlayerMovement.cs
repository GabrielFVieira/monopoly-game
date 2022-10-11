using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject[] tiles;
    public GameObject jogador;
    protected int actualPosition = 0;


    public void StartMoveJogador(int dado) {
        StartCoroutine(MoveJogador(dado));
    }

    public IEnumerator MoveJogador(int dado) {
        for(int i = 1; i <= dado; i++) {
            if(actualPosition >= tiles.Length - 1) {
                actualPosition = 0;
                jogador.transform.position = tiles[0].transform.position;
            } else { 
            jogador.transform.position = tiles[actualPosition +1].transform.position;
            actualPosition++;
              }

            yield return new WaitForSeconds(0.5f);

        }
    }
  
}
