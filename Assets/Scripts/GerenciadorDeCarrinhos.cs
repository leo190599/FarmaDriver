using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GerenciadorDeCarrinhos : MonoBehaviour
{
    [SerializeField]
    private static GerenciadorDeCarrinhos gerenciadorDeCarrinhosSingleton=null;
    [SerializeField]
    private int numeroDeCarrinhosLivres=0;
    [SerializeField]
    private int quantidadeDeDinheiro = 100;

    public UnityAction eventosAtualizacaoDeCarrinhosLivres;
    public UnityAction eventosAtualizacaoDeDinheiro;

    private void Awake()
    {
        //iniciação do singleton
        if(gerenciadorDeCarrinhosSingleton==null)
        {
            gerenciadorDeCarrinhosSingleton = this;
        }
        else
        {
            Debug.LogWarning("Ha mais de uma instancia de gerenciadores de carrinhos");
            Destroy(gameObject);
        }
    }
    private void OnDisable()
    {
        //Forçando o singleton se tornar nulo no momento que o objeto do singleton ativo é desativado
        if(gerenciadorDeCarrinhosSingleton==this)
        {
            gerenciadorDeCarrinhosSingleton=null;
        }
    }

    public void adicionarCarrinhoLivre()
    {
        numeroDeCarrinhosLivres++;
        if(eventosAtualizacaoDeCarrinhosLivres!=null)
        {
            eventosAtualizacaoDeCarrinhosLivres.Invoke();
        }
    }
    public void retirarCarrinhoLivre()
    {
        numeroDeCarrinhosLivres--;
        if (eventosAtualizacaoDeCarrinhosLivres != null)
        {
            eventosAtualizacaoDeCarrinhosLivres.Invoke();
        }
    }
    public void SubtrairDinheiro(int quantidade)
    {
        if(quantidade<=quantidadeDeDinheiro)
        {
            quantidadeDeDinheiro-= quantidade;
        }
        else
        {
            quantidadeDeDinheiro = 0;
        }
        if (eventosAtualizacaoDeDinheiro != null)
        {
            eventosAtualizacaoDeDinheiro.Invoke();
        }
    }
    public void somarDinheiro(int quantidade)
    {
        quantidadeDeDinheiro += quantidade;
        if(eventosAtualizacaoDeDinheiro!=null)
        {
            eventosAtualizacaoDeDinheiro.Invoke();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool ChecarPossibilidadeDeCompra(int valorAGastar) => (valorAGastar <= quantidadeDeDinheiro);
    public int GetQuantidadeDeDinheiro => quantidadeDeDinheiro;
    public int GetQuantidadeDeCarrinhosLivres => numeroDeCarrinhosLivres;
    public static GerenciadorDeCarrinhos GetGerenciadorDeCarrinhosSingleton => gerenciadorDeCarrinhosSingleton;
    public static bool ExisteUmGerenciadorDeCarrinhos => gerenciadorDeCarrinhosSingleton != null;
}
