using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScriptCarrinho : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    private RaycastHit hit;
    [SerializeField]
    private LayerMask mascaraDeRaio;

    [SerializeField]
        public GameObject alvoDebug;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IrAteObjetivo(GameObject alvo)
    {

            alvoDebug = alvo;

        Physics.Raycast(new Vector3(alvo.transform.position.x, 1000, alvo.transform.position.z), Vector3.down, out hit, Mathf.Infinity, mascaraDeRaio);
        Debug.Log(hit.collider);
        if (hit.collider != null)
        {
            agent.SetDestination(hit.point);
        }
    }

    private void OnDrawGizmos()
    {
        if (alvoDebug != null)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(new Vector3(alvoDebug.transform.position.x, 1000, alvoDebug.transform.position.z),
            new Vector3(alvoDebug.transform.position.x, -1000, alvoDebug.transform.position.z));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Cliente")
        {
            Destroy(other.gameObject);
            if(GerenciadorDeCarrinhos.ExisteUmGerenciadorDeCarrinhos)
            {
                agent.SetDestination(GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.GetHospital.GetCoordenadaDeRetornoNavmesh);
            }
        }
        else if(other.tag=="Hospital")
        {
            if(GerenciadorDeCarrinhos.ExisteUmGerenciadorDeCarrinhos)
            {
                GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.adicionarCarrinhoLivre();
                GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.ReceberPagamento();
                Destroy(gameObject);
            }
        }
    }

}
