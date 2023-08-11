using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlueController : MonoBehaviour
{
    public float moveSpeed = 10f; // Velocidade de movimentação do jogador
    public float jumpForce = 10f; // Força aplicada para impulsionar o pulo
    public float gravityScale = 1f; // Fator de escala da gravidade

    public bool isGrounded = false; // Verifica se o jogador está no chão
    public Transform groundCheck; // Objeto vazio que será usado para verificar se o jogador está no chão
    public LayerMask groundLayer; // Layer que representa o chão
    public float groundCheckRadius = 0.2f; // Raio para verificar se o jogador está no chão

    // Referência para o componente Rigidbody do jogador
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundCheck = transform.Find("GroundCheck");
    }

    private void Update()
    {
        // Captura a entrada do jogador nos eixos horizontal e vertical usando as teclas de seta
        float moveHorizontal = Input.GetKey(KeyCode.LeftArrow) ? -1f : (Input.GetKey(KeyCode.RightArrow) ? 1f : 0f);
        float moveVertical = Input.GetKey(KeyCode.DownArrow) ? -1f : (Input.GetKey(KeyCode.UpArrow) ? 1f : 0f);

        // Calcula o vetor de movimentação
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        movement = movement.normalized * moveSpeed * Time.deltaTime;

        // Move o jogador na direção horizontal e vertical
        rb.MovePosition(transform.position + movement);

        // Verifica se o jogador está no chão
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Aplica a gravidade ao jogador
        if (!isGrounded)
        {
            Vector3 gravity = new Vector3(0f, Physics.gravity.y * gravityScale * Time.deltaTime, 0f);
            rb.AddForce(gravity, ForceMode.VelocityChange);
        }

        // Verifica se o jogador pressionou a tecla de pulo (tecla 0 do teclado numérico)
        if (isGrounded && Input.GetKeyDown(KeyCode.Keypad0))
        {
            // Aplica uma força para impulsionar o pulo
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se colidiu com um objeto que possui a tag "Botão"
        if (collision.gameObject.CompareTag("Botão"))
        {
            // Obtém o componente "Elevador" do objeto "Elevador 2" e o ativa
            Elevador elevador = GameObject.Find("Elevador 2").GetComponent<Elevador>();
            elevador.enabled = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Verifica se deixou de colidir com um objeto que possui a tag "Botão"
        if (collision.gameObject.CompareTag("Botão"))
        {
            // Obtém o componente "Elevador" do objeto "Elevador 2" e o desativa
            Elevador elevador = GameObject.Find("Elevador 2").GetComponent<Elevador>();
            elevador.enabled = false;
        }
    }
}
