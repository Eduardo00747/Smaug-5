using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsController : MonoBehaviour
{
    public MonoBehaviour scriptToActivate; // Referência ao script que deseja ativar

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se colidiu com um objeto que possui a tag "Botão"
        if (collision.gameObject.CompareTag("Alavanca On"))
        {
            // Obtém o componente "Elevador" do objeto "Elevador 1" e o ativa
            Elevador elevador = GameObject.Find("Elevador 1").GetComponent<Elevador>();
            elevador.enabled = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Verifica se deixou de colidir com um objeto que possui a tag "Botão"
        if (collision.gameObject.CompareTag("Alavanca On"))
        {
            // Obtém o componente "Elevador" do objeto "Elevador 1" e o desativa
            Elevador elevador = GameObject.Find("Elevador 1").GetComponent<Elevador>();
            elevador.enabled = false;
        }
    }
}
