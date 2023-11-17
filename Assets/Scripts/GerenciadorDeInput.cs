using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDeInput : MonoBehaviour
{
    [SerializeField]
    private LayerMask mascaraDeInteratividade;
    private Ray raio;
    private RaycastHit hit;
    [SerializeField]
    private GerenciadorEstadiDeCena GerenciadorEstadiDeCena;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && GerenciadorEstadiDeCena.GetEstadoCena==GerenciadorEstadiDeCena.EstadoCena.jogando)
        {
            raio=Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(raio,out hit,Mathf.Infinity,mascaraDeInteratividade);
            if(hit.collider!=null)
            {
                if(GerenciadorDeCarrinhos.ExisteUmGerenciadorDeCarrinhos)
                {
                    GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.EnviarCarrinhoParaEntrega(hit.collider.gameObject);
                }
            }
        }
    }
}
