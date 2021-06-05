using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class IF_FogEffect : ScriptableRendererFeature
{
    [SerializeField]
    private RenderPassEvent renderPassEvent;
    [SerializeField]
    private Material _material;
    [SerializeField]
    private Color _fogColor;
    [SerializeField]
    private float _depthStart = 1;
    [SerializeField]
    private float _depthDistance = 500;

    private RenderPass renderPass;
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderPass.SetSource(renderer.cameraColorTarget);
        renderer.EnqueuePass(renderPass);
    }

    public override void Create()
    {
        renderPass = new RenderPass(_material,_fogColor,_depthStart, _depthDistance);
        if(renderPass != null)
        renderPass.renderPassEvent = renderPassEvent;
    }

    class RenderPass : ScriptableRenderPass
    {
        private Material _material;
        private Color _fogColor;
        private float _depthStart;
        private float _depthDistance;
        private RenderTargetIdentifier source;
        private RenderTargetHandle tempTexture;

        public RenderPass(Material pMaterial, Color pFogColor, float pDepthStart, float pDepthDistance) : base()
        {
            _material = pMaterial;
            _fogColor = pFogColor;
            _depthStart = pDepthStart;
            _depthDistance = pDepthDistance;
            tempTexture.Init("_TempFogTexture");
        }

        public void SetSource(RenderTargetIdentifier pSource)
        {
            source = pSource;
        }


        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (_material != null)
            {
                CommandBuffer cmd = CommandBufferPool.Get("FogEffect");

                SetMaterialVariables();
                RenderTextureDescriptor cameraTectureFog = renderingData.cameraData.cameraTargetDescriptor;
                cameraTectureFog.depthBufferBits = 0;
                cmd.GetTemporaryRT(tempTexture.id, cameraTectureFog, FilterMode.Bilinear);

                Blit(cmd, source, tempTexture.Identifier(), _material, 0);
                Blit(cmd, tempTexture.Identifier(), source);

                context.ExecuteCommandBuffer(cmd);
                CommandBufferPool.Release(cmd);
            }
        }

        private void SetMaterialVariables()
        {
            _material.SetColor("_FoggColor", _fogColor);
            _material.SetFloat("_DepthStart", _depthStart);
            _material.SetFloat("_DepthDistance", _depthDistance);
        }
    }

}
