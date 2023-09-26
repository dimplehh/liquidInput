//#define DEBUG_RENDER

using UnityEngine;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;

namespace AkilliMum.Standard.D2WeatherEffects
{
    [ExecuteInEditMode]
    public class D2SnowsSpriteExt : MonoBehaviour
    {
        public Transform CamTransform;
        private Vector3 _firstPosition;
        private Vector3 _difference;
        public float CameraSpeedMultiplier = 1f;

        public Color Color = new Color(1f, 1f, 1f, 1f);
        public Texture2D Mask;
        [Space]
        public bool TopFade = false;
        public bool RightFade = false;
        public bool BottomFade = false;
        public bool LeftFade = false;
        [Range(0.0f, 1f)]
        public float FadeMultiplier = 0.1f;
        [Space]
        [MinMaxSlider(0, 200)]
        public Vector2 Layers = new Vector2(0, 50.0f);
        [Range(0, 10)]
        public float Speed = .1f;
        [Range(0, 20)]
        public float Direction = 0f;
        [Range(0, 20)]
        public float Luminance = 1f;
        [Range(0.01f, 20f)]
        public float Depth = 1f;
        //public bool DarkMode = false;
        //[Range(0f, 0.1f)]
        //public float LuminanceAdder = 0.002f;
        public Material[] EffectedMaterials;

        private void Awake()
        {
            _firstPosition = CamTransform.position;
        }

        private void Update()
        {
            _difference = Vector3.Lerp(_firstPosition, CamTransform.position, Time.time);
            _firstPosition = CamTransform.position;

            foreach (var mat in EffectedMaterials)
            {
                if (mat.HasProperty("_TopFade"))
                {
                    mat.SetFloat("_TopFade", TopFade ? 1 : 0);
                }
                if (mat.HasProperty("_RightFade"))
                {
                    mat.SetFloat("_RightFade", RightFade ? 1 : 0);
                }
                if (mat.HasProperty("_BottomFade"))
                {
                    mat.SetFloat("_BottomFade", BottomFade ? 1 : 0);
                }
                if (mat.HasProperty("_LeftFade"))
                {
                    mat.SetFloat("_LeftFade", LeftFade ? 1 : 0);
                }
                if (mat.HasProperty("_FadeMultiplier"))
                {
                    mat.SetFloat("_FadeMultiplier", FadeMultiplier);
                }
                if (mat.HasProperty("_Color"))
                {
                    mat.SetColor("_Color", Color);
                }
                if (mat.HasProperty("_MaskTex"))
                {
                    mat.SetTexture("_MaskTex", Mask);
                }
                if (mat.HasProperty("SPEED"))
                {
                    mat.SetFloat("SPEED", Speed);
                }
                if (mat.HasProperty("WIDTH"))
                {
                    mat.SetFloat("WIDTH", Direction);
                }
                if (mat.HasProperty("DEPTH"))
                {
                    mat.SetFloat("DEPTH", Depth);
                }
                if (mat.HasProperty("_Luminance"))
                {
                    mat.SetFloat("_Luminance", Luminance);
                }
                if (mat.HasProperty("LAYERSSTART"))
                {
                    mat.SetFloat("LAYERSSTART", Layers[0]);
                }
                if (mat.HasProperty("LAYERSEND"))
                {
                    mat.SetFloat("LAYERSEND", Layers[1]);
                }
                if (mat.HasProperty("_CameraSpeedMultiplier"))
                {
                    mat.SetFloat("_CameraSpeedMultiplier", CameraSpeedMultiplier);
                }
                if (mat.HasProperty("_UVChangeX"))
                {
                    mat.SetFloat("_UVChangeX", _difference.x);
                }
                if (mat.HasProperty("_UVChangeY"))
                {
                    mat.SetFloat("_UVChangeY", _difference.y);
                }
            }
        }
    }
}