using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using Objects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Handlers
{
    public class BoardHandler : MonoBehaviour
    {
        [SerializeField] private int frameDelayInSeconds;
        [SerializeField] private Color[] types;
        [SerializeField] private char[] frames;

        [SerializeField] private GridLayoutGroup boardGrid;
        [SerializeField] private GameObject pixelPrefab;

        [SerializeField] private Image frameTypeImage;
        [SerializeField] private TextMeshProUGUI frameIdText;
        
        [SerializeField] private bool useSeed;
        [SerializeField] private int seed;

        private PixelHandler[][] board;
        private Code data;

        private void Start()
        {
            if (useSeed) Randomness.SetSeed(seed);
            
            data = InputProcessor.GetData("qr");
            InitBoard();

            SetTypeColors();
            
            StartCoroutine(ShowFrames());
        }

        private void InitBoard()
        {
            board = new PixelHandler[data.width][];
            boardGrid.constraintCount = data.width;

            for (var x = 0; x < data.width; x++)
            {
                board[x] = new PixelHandler[data.height];

                for (var y = 0; y < data.height; y++)
                {
                    var newPixel = Instantiate(pixelPrefab, transform.position, Quaternion.identity)
                        .GetComponent<PixelHandler>();
                    newPixel.transform.SetParent(boardGrid.transform, false);
                    newPixel.SetType(types.RandomElement());
                    newPixel.SetData(data.data[x + y * data.width]);

                    board[x][y] = newPixel;
                }
            }
        }

        private void SetTypeColors()
        {
            foreach (var column in board)
            foreach (var pixel in column)
                pixel.SetTypeColor();
        }

        private void SetRandomColors()
        {
            foreach (var column in board)
            foreach (var pixel in column)
                pixel.SetRandomColor();
        }

        private void SetDataColorsForType(Color type)
        {
            foreach (var column in board)
            foreach (var pixel in column)
            {
                if (pixel.GetPixelType() == type) pixel.SetDataColor();
                else pixel.SetRandomColor();
            }
        }

        private IEnumerator ShowFrames()
        {
            var frameData = GenerateFrameData();

            foreach (var frame in frameData)
            {
                yield return new WaitForSecondsRealtime(frameDelayInSeconds);
                ShowFrame(frame);
            }
        }

        private IEnumerable<FrameInfo> GenerateFrameData()
        {
            var distinctFrames = frames.Distinct().ToArray();
            var correctFrames = distinctFrames.RandomElements(types.Length);
            var frameInfo = new FrameInfo[distinctFrames.Length];

            for (var i = 0; i < distinctFrames.Length; i++)
            {
                var typeIndex = Array.IndexOf(correctFrames, distinctFrames[i]);

                frameInfo[i] = new FrameInfo
                {
                    id = distinctFrames[i],
                    type = typeIndex != -1 ? types[typeIndex] : Color.clear,
                    isMeaningful = typeIndex != -1
                };
            }

            Debug.Log("Correct frames are: " +  string.Join(" ", correctFrames));
            return frameInfo;
        }

        private void ShowFrame(FrameInfo f)
        {
            frameIdText.text = f.id.ToString();
            frameTypeImage.color = f.isMeaningful ? f.type : types.RandomElement();

            if (!f.isMeaningful) SetRandomColors();
            else SetDataColorsForType(f.type);
        }
    }
}