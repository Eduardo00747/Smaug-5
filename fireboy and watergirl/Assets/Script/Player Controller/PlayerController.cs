using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f; // Velocidade de movimenta��o do jogador
    public float jumpForce = 10f; // For�a aplicada para impulsionar o pulo
    public float gravityScale = 1f; // Fator de escala da gravidade

    public bool isGrounded = false; // Verifica se o jogador est� no ch�o
    public Transform groundCheck; // Objeto vazio que ser� usado para verificar se o jogador est� no ch�o
    public LayerMask groundLayer; // Layer que representa o ch�o
    public float groundCheckRadius = 0.2f; // Raio para verificar se o jogador est� no ch�o

    // Refer�ncia para o componente Rigidbody do jogador
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundCheck = transform.Find("GroundCheck");
    }

    private void Update()
    {
        // Captura a entrada do jogador nos eixos horizontal e vertical usando as teclas W, A, S e D
        float moveHorizontal = Input.GetKey("a") ? -1f : (Input.GetKey("d") ? 1f : 0f);
        float moveVertical = Input.GetKey("s") ? -1f : (Input.GetKey("w") ? 1f : 0f);

        // Calcula o vetor de movimenta��o
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        movement = movement.normalized * moveSpeed * Time.deltaTime;

        // Move o jogador na dire��o horizontal e vertical
        rb.MovePosition(transform.position + movement);

        // Verifica se o jogador est� no ch�o
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Aplica a gravidade ao jogador
        if (!isGrounded)
        {
            Vector3 gravity = new Vector3(0f, Physics.gravity.y * gravityScale * Time.deltaTime, 0f);
            rb.AddForce(gravity, ForceMode.VelocityChange);
        }

        // Verifica se o jogador pressionou a tecla de pulo (normalmente a barra de espa�o)
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // Aplica uma for�a para impulsionar o pulo
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se colidiu com um objeto que possui a tag "Bot�o"
        if (collision.gameObject.CompareTag("Bot�o"))
        {
            // Obt�m o componente "Elevador" do objeto "Elevador 2" e o ativa
            Elevador elevador = GameObject.Find("Elevador 2").GetComponent<Elevador>();
            elevador.enabled = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Verifica se deixou de colidir com um objeto que possui a tag "Bot�o"
        if (collision.gameObject.CompareTag("Bot�o"))
        {
            // Obt�m o componente "Elevador" do objeto "Elevador 2" e o desativa
            Elevador elevador = GameObject.Find("Elevador 2").GetComponent<Elevador>();
            elevador.enabled = false;
        }
    }
}