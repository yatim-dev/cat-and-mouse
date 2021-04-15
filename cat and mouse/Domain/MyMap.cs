﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;


namespace cat_and_mouse.Domain
{
    public enum ChunkType
    {
        None,
        Wall
    }

    public class MapChunk
    {
        public ChunkType Type;
        public Bitmap Texture;

        public MapChunk(ChunkType type, Bitmap texture)
        {
            Type = type;
            Texture = texture;
        }
    }

    public class MyMap
    {
        public int ChunkSize;
        public int Width { get; private set; }
        public int Height { get; private set; }
        public MapChunk[,] Chunks;
        public List<Square> Walls;

        public MyMap(string fileName)
        {
            ReadFromFile(fileName);
        }

        public void ReadFromFile(string fileName)
        {
            Walls = new List<Square>();
            var path = "Levels\\" + fileName + '\\';
            var file = new StreamReader(path + fileName + ".txt");
            var textureCount = int.Parse(file.ReadLine());
            var textureDictionary = new Dictionary<char, MapChunk>();

            for (var i = 0; i < textureCount; i++)
            {
                var textureInfo = file.ReadLine().Split();
                var type = ChunkType.None;
                if (!Enum.TryParse(textureInfo[1], out type))
                    throw new Exception();
                textureDictionary[textureInfo[0][0]] = new MapChunk(type, new Bitmap(path + textureInfo[2]));
                if (textureDictionary[textureInfo[0][0]].Texture.Width > ChunkSize)
                    ChunkSize = textureDictionary[textureInfo[0][0]].Texture.Width;
            }

            var size = file.ReadLine().Split().Select(x => int.Parse(x)).ToArray();
            Width = size[0];
            Height = size[1];
            Chunks = new MapChunk[Width, Height];

            for (var i = 0; i < Height; i++)
            {
                var line = file.ReadLine();
                for (var j = 0; j < Width; j++)
                {
                    Chunks[j, i] = textureDictionary[line[j]];
                    if (Chunks[j, i].Type == ChunkType.Wall)
                        Walls.Add(new Square(new Vector(j, i), 1));
                }
            }
        }

        public Bitmap GetMapImage()
        {
            var image = new Bitmap(Width * ChunkSize, Height * ChunkSize);
            var graphics = Graphics.FromImage(image);
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    graphics.DrawImage(Chunks[j, i].Texture, j * ChunkSize, i * ChunkSize);
                }
            }

            graphics.Dispose();
            return image;
        }
    }
}