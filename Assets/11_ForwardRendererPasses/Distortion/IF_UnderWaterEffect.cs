using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class IF_UnderWaterEffect : ScriptableRendererFeature
{
    [SerializeField]
    private RenderPassEvent renderPassEvent;
    [SerializeField]
    private Material _material;
    [SerializeField]
    [Range(0.001f, 0.1f)]
    private float _pixelOffset;
    [SerializeField]
    [Range(0.1f, 20f)]
    private float _noiseScale;
    [SerializeField]
    [Range(0.1f, 20f)]
    private float _noiseFrequency;
    [SerializeField]
    [Range(0.1f, 30f)]
    private float _noiseSpeed;
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
        renderPass = new RenderPass(_material,_pixelOffset,_noiseScale,_noiseFrequency,_noiseSpeed, _depthStart, _depthDistance);
        if (renderPass != null)
            renderPass.renderPassEvent = renderPassEvent;
    }

    class RenderPass : ScriptableRenderPass
    {
        private Material _material;
        private float _pixelOffset;
        private float _noiseScale;
        private float _noiseFrequency;
        private float _noiseSpeed;
        private float _depthDistance;
        private float _depthStart;
        private RenderTargetIdentifier source;
        private RenderTargetHandle tempTexture;

        public RenderPass(Material pMaterial, float pPixelOffset, float pNoiseScale, float pNoiseFrequency, float pNoiseSpeed, float pDepthStart, float pDepthdist ) : base()
        {
            _material = pMaterial;
            _pixelOffset = pPixelOffset;
            _noiseScale = pNoiseScale;
            _noiseFrequency = pNoiseFrequency;
            _noiseSpeed = pNoiseSpeed;
            _depthStart = pDepthStart;
            _depthDistance = pDepthdist;
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
            _material.SetFloat("_NoiseFrequency", _noiseFrequency);
            _material.SetFloat("_NoiseScale", _noiseScale);
            _material.SetFloat("_NoiseSpeed", _noiseSpeed);
            _material.SetFloat("_PixelOffset", _pixelOffset);
            _material.SetFloat("_DepthStart", _depthStart);
            _material.SetFloat("_DepthDistance", _depthDistance);
        }
    }

}