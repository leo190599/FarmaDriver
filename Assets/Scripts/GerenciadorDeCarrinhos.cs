using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GerenciadorDeCarrinhos : MonoBehaviour
{
    [Header("Singleton")]
    [SerializeField]
    private static GerenciadorDeCarrinhos gerenciadorDeCarrinhosSingleton=null;
    [Header("ParametrosDeDesign")]
    [SerializeField]
    private int[] precosDosCarrinhos;
    [Header("ParametrosDeDebug")]
    [SerializeField]
    private int indexPrecosDosCarrinhos=0;
    [SerializeField]
    private int numeroTotalDeCarrinhos = 0;
    [SerializeField]
    private int numeroDeCarrinhosLivres=0;
    [SerializeField]
    private int quantidadeDeDinheiro = 100;
    [SerializeField]
    private int PrecoCarrinho=100;
    [Header("Eventos")]
    public UnityAction eventosAtualizacaoDeCarrinhosLivres;
    public UnityAction eventosAtualizacaoDeDinheiro;
    public UnityAction eventosCompraDeCarrinhos;
    public UnityAction eventosCompraFinalDeCarrinhos;
    public UnityAction eventosFalhaCompraDeCarrinhos;

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

    public void comprarCarrinho()
    {
        if(ChecarPossibilidadeDeCompra(PrecoCarrinho))
        {
            SubtrairDinheiro(PrecoCarrinho);
            indexPrecosDosCarrinhos++;
            if(indexPrecosDosCarrinhos<precosDosCarrinhos.Length)
            {
                PrecoCarrinho = precosDosCarrinhos[indexPrecosDosCarrinhos];

            }
            else
            {
                if(eventosCompraFinalDeCarrinhos!=null)
                {
                    eventosCompraFinalDeCarrinhos.Invoke();
                }
            }
            numeroTotalDeCarrinhos++;
            if(eventosCompraDeCarrinhos!=null)
            {
                eventosCompraDeCarrinhos.Invoke();
            }
            adicionarCarrinhoLivre();
        }
        else
        {
            if(eventosFalhaCompraDeCarrinhos!=null)
            {
                eventosFalhaCompraDeCarrinhos.Invoke();
            }
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
        if (eventosCompraDeCarrinhos != null)
        {
            eventosCompraDeCarrinhos.Invoke();
        }
        if (precosDosCarrinhos.Length>=indexPrecosDosCarrinhos-1 && indexPrecosDosCarrinhos>=0)
        {
            PrecoCarrinho = precosDosCarrinhos[indexPrecosDosCarrinhos];
        }
        else
        {
            Debug.LogError("Insira um index valido");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool ChecarPossibilidadeDeCompra(int valorAGastar) => (valorAGastar <= quantidadeDeDinheiro);
    public int GetQuantidadeDeDinheiro => quantidadeDeDinheiro;
    public int GetQuantidadeTotalDeCarrinhos => numeroTotalDeCarrinhos;
    public int GetQuantidadeDeCarrinhosLivres => numeroDeCarrinhosLivres;
    public int GetPrecoCarrinho => PrecoCarrinho;
    public static GerenciadorDeCarrinhos GetGerenciadorDeCarrinhosSingleton => gerenciadorDeCarrinhosSingleton;
    public static bool ExisteUmGerenciadorDeCarrinhos => gerenciadorDeCarrinhosSingleton != null;
}
