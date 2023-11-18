using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciadorDeCliente : MonoBehaviour
{
    [SerializeField]
    private GerenciadorDeClientesScirpt gerenciadorDeClientesPai;
    [SerializeField]
    private ScriptCliente clientePrefab;
    public void SetGerenciadorDeClientePai(GerenciadorDeClientesScirpt gerenciadorDeClientes)
    {
        gerenciadorDeClientesPai= gerenciadorDeClientes;
    }
    public void InstancarCliente()
    {
        if (clientePrefab != null)
        {
            ScriptCliente novoCliente=Instantiate(clientePrefab,transform.position,transform.rotation);
            gerenciadorDeClientesPai.OcuparInstanciadorDeClientes(this);
            novoCliente.SetInstanciadorDeClientePai(this);
        }
    }
    public void LiberarInstanciador()
    {
        gerenciadorDeClientesPai.LiberarInstanciadorDeCliente(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
