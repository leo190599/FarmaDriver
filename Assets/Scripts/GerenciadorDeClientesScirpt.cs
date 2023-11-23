using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GerenciadorDeClientesScirpt : MonoBehaviour
{
    private static GerenciadorDeClientesScirpt gerenciadorDeClientesSingleton=null;
    [SerializeField]
    private float intervaloDeSpawnClientes = 10;
    [SerializeField]
    private float decrementoDeTempoSpawnClientes = .25f;
    [SerializeField]
    private float minimoIntervaloSpawnClientes = 1;
    private IEnumerator corrotinaDeInstancia;
    [SerializeField]
    private int contagemDeFilhos;
    [SerializeField]
    private int quantidadesDeCaixasAPerder = 10;
    [SerializeField]
    private GerenciadorEstadiDeCena gerenciadorEstadosDeCena;

    public UnityAction eventosAlteracaoDeCaixasAPerder;


    private List<InstanciadorDeCliente> instanciadoresDeClientes;
    private List<InstanciadorDeCliente> instanciadoresDeClientesLivres;
    private List<InstanciadorDeCliente> instanciadorDeClientesOcupados;
    private bool houveAPrimeiraEntrega = false;

    private void Awake()
    {
        if(gerenciadorDeClientesSingleton==null)
        {
            gerenciadorDeClientesSingleton = this;
        }
        else
        {
            Debug.LogWarning("Ha mais de uma instancia de gerenciadores de clientes");
            Destroy(gameObject);
        }
    }

    public void PerderCaixaAPerder()
    {
        quantidadesDeCaixasAPerder--;
        if(quantidadesDeCaixasAPerder<=0)
        {
            gerenciadorEstadosDeCena.TrocarEstadoCena(GerenciadorEstadiDeCena.EstadoCena.perdeu);
        }
        else
        {
            if(eventosAlteracaoDeCaixasAPerder!=null)
            {
                eventosAlteracaoDeCaixasAPerder.Invoke();
            }
        }
    }

    public void SetHouveAPrimeiraEntrega(bool novoValor)
    {
        houveAPrimeiraEntrega=novoValor;
    }

    // Start is called before the first frame update
    void Start()
    {
        instanciadoresDeClientes=new List<InstanciadorDeCliente> ();
        instanciadoresDeClientesLivres = new List<InstanciadorDeCliente>();
        instanciadorDeClientesOcupados = new List<InstanciadorDeCliente>();
        contagemDeFilhos = transform.childCount;
        InstanciadorDeCliente novoFilho;
        for(int i=0;i<contagemDeFilhos;i++)
        {
            novoFilho = transform.GetChild(i).GetComponent<InstanciadorDeCliente>();
            instanciadoresDeClientes.Add(novoFilho);
            instanciadoresDeClientesLivres.Add(novoFilho);
            novoFilho.SetGerenciadorDeClientePai(this);
        }
        corrotinaDeInstancia = CorrotinaInstanciaCliente();
        if (instanciadoresDeClientesLivres.Count > 0)
        {
            instanciadoresDeClientesLivres[Random.Range(0, instanciadoresDeClientesLivres.Count - 1)].InstancarCliente();
        }
        StartCoroutine(corrotinaDeInstancia);
    }
    public void OcuparInstanciadorDeClientes(InstanciadorDeCliente instanciadorASerOcupado)
    {
        if(instanciadoresDeClientesLivres.Contains(instanciadorASerOcupado))
        {
            instanciadorDeClientesOcupados.Add(instanciadorASerOcupado);
            instanciadoresDeClientesLivres.Remove(instanciadorASerOcupado);
        }
    }
    public void LiberarInstanciadorDeCliente(InstanciadorDeCliente instanciadorASerLiberado)
    {
       // Debug.Log(instanciadorASerLiberado);
        instanciadorDeClientesOcupados.Remove(instanciadorASerLiberado);
        instanciadoresDeClientesLivres.Add(instanciadorASerLiberado);
    }

    //ImplementarInstanciacaoDeClientesPorTempo
    IEnumerator CorrotinaInstanciaCliente()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervaloDeSpawnClientes);
            if (instanciadoresDeClientesLivres.Count > 0 && houveAPrimeiraEntrega)
            {
                instanciadoresDeClientesLivres[Random.Range(0, instanciadoresDeClientesLivres.Count - 1)].InstancarCliente();
                intervaloDeSpawnClientes -= decrementoDeTempoSpawnClientes;
                if(intervaloDeSpawnClientes<minimoIntervaloSpawnClientes)
                {
                    intervaloDeSpawnClientes = minimoIntervaloSpawnClientes;
                }
            }
        }
    }

    private void OnDisable()
    {
        if(gerenciadorDeClientesSingleton==this)
        {
            gerenciadorDeClientesSingleton = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(instanciadoresDeClientesLivres.Count);
    }

    public static GerenciadorDeClientesScirpt GetGerenciadorDeClientesSingleton=>
        gerenciadorDeClientesSingleton;
    public static bool ExisteUmGerenciadorDeClientesSingleton=>gerenciadorDeClientesSingleton!=null;
    public int GetQuantidadeDeCaixasAPerder => quantidadesDeCaixasAPerder;
    public bool GetHouveAPrimeiraEntrega => houveAPrimeiraEntrega;
}
