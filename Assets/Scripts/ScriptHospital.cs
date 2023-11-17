using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptHospital : MonoBehaviour
{
    [SerializeField]
    private GameObject carrinhoPrefab;
    [SerializeField]
    private Transform coordenadaDeInstancia;
    [SerializeField]
    private GameObject coordenadaDeRetorno;
    [SerializeField]
    private Vector3 coorDenadaDeRetornoNaNavmesh;
    [SerializeField]
    private RaycastHit hit;
    [SerializeField]
    private LayerMask mascaraDeColisao;
    // Start is called before the first frame update
    void Start()
    {
        if(GerenciadorDeCarrinhos.ExisteUmGerenciadorDeCarrinhos)
        {
            GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.SetHospital(this);
        }
        Physics.Raycast(coordenadaDeRetorno.transform.position, Vector3.down, out hit, Mathf.Infinity, mascaraDeColisao);
        if(hit.collider!=null)
        {
            coorDenadaDeRetornoNaNavmesh = hit.point;
        }
    }

    public void instanciarCarrinho(GameObject alvo)
    {
        Debug.Log(coordenadaDeRetorno);
        GameObject instanciaCarrinho= Instantiate(carrinhoPrefab, coordenadaDeInstancia.position, Quaternion.identity);
        instanciaCarrinho.GetComponent<ScriptCarrinho>().IrAteObjetivo(alvo);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawLine(coordenadaDeRetorno.transform.position, (coordenadaDeRetorno.transform.position + Vector3.down * 1000));
    }
    public Vector3 GetCoordenadaDeRetornoNavmesh => coorDenadaDeRetornoNaNavmesh;
}
