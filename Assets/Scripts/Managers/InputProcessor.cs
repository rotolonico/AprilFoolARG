using Objects;
using UnityEngine;

namespace Managers
{
    public static class InputProcessor
    {
        public static Code GetData(string inputPath)
        {
            var input = Resources.Load (inputPath) as Texture2D;
            return new Code
            {
                width = input.width,
                height = input.height,
                data = input.GetPixels()
            };

        }
    }
}
