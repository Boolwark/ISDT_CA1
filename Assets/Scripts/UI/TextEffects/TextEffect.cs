using System;
using TMPro;
using UnityEngine;

namespace UI.TextEffects
{
    public class TextEffect : MonoBehaviour
    {
        public TMP_Text textComponent;

        public enum Effects
        {
            WOBBLY,
            SHAKING
            
        }

        public float shakeScale = 0.01f;

        public Effects currentEffect = Effects.WOBBLY;
        private void Update()
        {
            textComponent.ForceMeshUpdate();
            var textInfo = textComponent.textInfo;
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                var charInfo = textInfo.characterInfo[i];
                if (!charInfo.isVisible)
                {
                    continue;
                }

                var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
                for (int j = 0; j < 4; j++)
                {
                    var orig = verts[charInfo.vertexIndex + j];
                    switch (currentEffect)
                    {
                        case(Effects.WOBBLY):
                            verts[charInfo.vertexIndex + j] =
                                orig + new Vector3(0, Mathf.Sin(Time.time * 2f + orig.x * 0.01f) * 10f, 0)*shakeScale;
                            break;
                        case(Effects.SHAKING):
                            verts[charInfo.vertexIndex + j] =
                                orig + new Vector3(UnityEngine.Random.Range(0,1f) * shakeScale, UnityEngine.Random.Range(0,1f)* shakeScale, 0);
                            break;
                    }
                    
                    
                }
            }
            for(int i = 0; i < textInfo.meshInfo.Length;i++)
            {
                var meshInfo = textInfo.meshInfo[i];
                meshInfo.mesh.vertices = meshInfo.vertices;
                textComponent.UpdateGeometry(meshInfo.mesh,i);
            }
        }
    }
}