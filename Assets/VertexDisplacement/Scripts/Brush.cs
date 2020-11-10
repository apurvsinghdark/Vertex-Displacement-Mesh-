using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{
    public GameObject _terrain;
    public Transform _brush;
    public Shader drawShader;
    public ParticleSystem particle;

    private Material drawMaterial;
    private Material myMaterial;
    private RenderTexture splatmap;

    [Range(0, 2)]
    public float _brushSize;
    [Range(0, 10)]
    public float _brushStrength;

    RaycastHit _groundHit;
    int _layerMask;

    private void Start()
    {
        particle.Stop();

        _layerMask = LayerMask.GetMask("Ground");
        drawMaterial = new Material(drawShader);
        myMaterial = _terrain.GetComponent<MeshRenderer>().material;
        myMaterial.SetTexture("_Splat", splatmap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat));

    }

    private void Update()
    {
        if (Physics.Raycast(_brush.position, -transform.right, out _groundHit, 1f, _layerMask))
        {
            drawMaterial.SetVector("_Coordinate", new Vector4(_groundHit.textureCoord.x, _groundHit.textureCoord.y, 0, 0));
            drawMaterial.SetFloat("_Strength", _brushStrength);
            drawMaterial.SetFloat("_Size", _brushSize);

            RenderTexture temp = RenderTexture.GetTemporary(splatmap.width, splatmap.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(splatmap, temp);
            Graphics.Blit(temp, splatmap, drawMaterial);
            RenderTexture.ReleaseTemporary(temp);

            particle.Play();
        }
        else
        {
            particle.Stop();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_brush.position, -transform.right * 1f);
    }
}
