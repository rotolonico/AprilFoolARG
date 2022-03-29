using Objects;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Handlers
{
    public class PixelHandler : MonoBehaviour
    {
        [SerializeField] private Image pixel;
        
        private Color type = Color.clear;
        private Color data = Color.clear;
    
        public void SetType(Color c) => type = c;

        public void SetData(Color c) => data = c;
    
        public Color GetPixelType() => type;

        public void SetTypeColor() => pixel.color = type;
        
        public void SetDataColor() => pixel.color = data;
        public void SetRandomColor() => pixel.color = Randomness.RandomBlackOrWhiteColor();
    }
}
