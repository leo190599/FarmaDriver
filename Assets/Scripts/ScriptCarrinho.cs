using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScriptCarrinho : MonoBehaviour
{
    [Header("Parametros de design")]
    private static float velBoost = 30;
    private static int custoBoost = 25;
    [Header("Parametros de debug")]
    [SerializeField]
    private NavMeshAgent agent;
    private RaycastHit hit;
    [SerializeField]
    private LayerMask mascaraDeRaio;
    private bool boostAtivo;
    [SerializeField]
    private GameObject particulasEntregaConcluida;
    [SerializeField]
    private GameObject particulasFalhaNaEntrega;
    [SerializeField]
    private GameObject particulasBoost;

    private bool fezAEntrega = false;

    [SerializeField]
    public GameObject alvo;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AtivarBoost()
    {
        if (agent != null && !boostAtivo)
        {
            if (GerenciadorDeCarrinhos.ExisteUmGerenciadorDeCarrinhos)
            {
                if (GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.GetQuantidadeDeDinheiro 
                    >= custoBoost)
                {
                    GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.SubtrairDinheiro(custoBoost);
                    agent.speed = velBoost;
                    particulasBoost.transform.parent = null;
                    particulasBoost.SetActive(true);
                    boostAtivo = true;
                }
            }
        }
    }

    public void IrAteObjetivo(GameObject alvo)
    {

        this.alvo = alvo;
        ScriptCliente clienteAlvo=alvo.GetComponent<ScriptCliente>();
        if(clienteAlvo!=null)
        {
            clienteAlvo.SetCarrinhoParaEntrega(this);
        }
        Physics.Raycast(new Vector3(alvo.transform.position.x, 1000, alvo.transform.position.z), Vector3.down, out hit, Mathf.Infinity, mascaraDeRaio);
        if (hit.collider != null)
        {
            agent.SetDestination(hit.point);
        }
    }

    public void FracassarEntrega()
    {
        GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.adicionarCarrinhoLivre();
        particulasFalhaNaEntrega.transform.parent = null;
        particulasFalhaNaEntrega.SetActive(true);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (alvo != null)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(new Vector3(alvo.transform.position.x, 1000, alvo.transform.position.z),
            new Vector3(alvo.transform.position.x, -1000, alvo.transform.position.z));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Cliente" && other.gameObject==alvo)
        {
            Destroy(other.gameObject);
            fezAEntrega = true;
            if(GerenciadorDeCarrinhos.ExisteUmGerenciadorDeCarrinhos)
            {
                agent.SetDestination(GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.GetHospital.GetCoordenadaDeRetornoNavmesh);
            }
        }
        else if(other.tag=="Hospital" && fezAEntrega)
        {
            if(GerenciadorDeClientesScirpt.ExisteUmGerenciadorDeClientesSingleton)
            {
                GerenciadorDeClientesScirpt.GetGerenciadorDeClientesSingleton.SetHouveAPrimeiraEntrega(true);
            }
            if(GerenciadorDeCarrinhos.ExisteUmGerenciadorDeCarrinhos)
            {
                GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.adicionarCarrinhoLivre();
                GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.ReceberPagamento();
                particulasEntregaConcluida.transform.parent = null;
                particulasEntregaConcluida.SetActive(true);
                Destroy(gameObject);
            }
        }
    }

}
