using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevador : MonoBehaviour
{
    public Transform startPosition; // Posi��o inicial do elevador (Y 7.12 e X 16.4)
    public Transform endPosition;   // Posi��o final do elevador (Y -0.35 e X -16.4)
    public float moveDuration = 1.5f; // Dura��o total do movimento (ida e volta)

    private float moveTime = 0f;     // Tempo decorrido do movimento
    private bool movingToEnd = true; // Controla a dire��o do movimento

    private void Start()
    {
        // Define a posi��o inicial do elevador
        transform.position = startPosition.position;
    }

    private void Update()
    {
        // Move o elevador em dire��o � posi��o final ou inicial, dependendo do valor de 'movingToEnd'
        if (movingToEnd)
        {
            moveTime += Time.deltaTime;
            float t = moveTime / moveDuration;
            transform.position = Vector3.Lerp(startPosition.position, endPosition.position, t);

            // Verifica se chegou � posi��o final e troca a dire��o do movimento
            if (t >= 1f)
            {
                moveTime = 0f;
                movingToEnd = false;
            }
        }
        else
        {
            moveTime += Time.deltaTime;
            float t = moveTime / moveDuration;
            transform.position = Vector3.Lerp(endPosition.position, startPosition.position, t);

            // Verifica se chegou � posi��o inicial e troca a dire��o do movimento
            if (t >= 1f)
            {
                moveTime = 0f;
                movingToEnd = true;
            }
        }
    }
}