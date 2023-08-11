using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRedHealth : MonoBehaviour
{
    public int maxHealth = 100; // Vida máxima do personagem
    private int currentHealth; // Vida atual do personagem

    private void Start()
    {
        currentHealth = maxHealth; // Configura a vida inicial para o valor máximo
    }

    // Usamos OnTriggerEnter para detectar colisões com objetos que possuem Collider marcado como Is Trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se colidiu com um objeto que possui a tag "PoçaRed" ou "PoçaGreen"
        if (other.CompareTag("PoçaRed") || other.CompareTag("PoçaGreen"))
        {
            // Reduz a vida do personagem em 10
            currentHealth -= 10;

            // Verifica se a vida chegou a 0 ou menos
            if (currentHealth <= 0)
            {
                // Se a vida for menor ou igual a 0, destrói o personagem
                Destroy(gameObject);

                // Mostra uma mensagem no console indicando que o personagem morreu
                Debug.Log("Personagem morreu!");
            }
        }
    }
}
