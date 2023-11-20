using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDeClientesScirpt : MonoBehaviour
{
    [SerializeField]
    private float intervaloDeSpawnClientes = 10;
    [SerializeField]
    private float decrementoDeTempoSpawnClientes = .25f;
    [SerializeField]
    private float minimoIntervaloSpawnClientes = 1;
    private IEnumerator corrotinaDeInstancia;
    [SerializeField]
    private int contagemDeFilhos;
    private List<InstanciadorDeCliente> instanciadoresDeClientes;
    private List<InstanciadorDeCliente> instanciadoresDeClientesLivres;
    private List<InstanciadorDeCliente> instanciadorDeClientesOcupados;
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
            if (instanciadoresDeClientesLivres.Count > 0)
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

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(instanciadoresDeClientesLivres.Count);
    }
}
