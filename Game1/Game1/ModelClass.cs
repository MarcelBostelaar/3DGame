using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Game1
{
    class ModelClass
    {
        private class ModelClassJson
        {
            public string VertexType { get; set; }
            public List<VertexPositionTexture> Vertixes { get; set; }
            public List<int[]> Indexes { get; set; }
        }
        private enum VertexTypes{
            VertexPositionTexture
        }

        public void LoadJson(string JsonName, string path)
        {

        }

        public void WriteToJson()
        {
            string Json;
            var NewJsonFile = new ModelClassJson();
            NewJsonFile.VertexType = VertexTypes.VertexPositionTexture.ToString();
            NewJsonFile.Vertixes = new List<VertexPositionTexture>();

            for (int i = 0; i < testArray.Length; i++)
            {
                NewJsonFile.Vertixes.Add(testArray[i]);
            }
            NewJsonFile.Indexes = new List<int[]>();
            for (int i = 0; i < Indextestarray.Length/3; i++)
            {
                int[] tinyarray = new int[3];
                tinyarray[0] = (int)Indextestarray[0 + i * 3];
                tinyarray[1] = (int)Indextestarray[1 + i * 3];
                tinyarray[2] = (int)Indextestarray[2 + i * 3];
                NewJsonFile.Indexes.Add(tinyarray);
            }
            Json = JsonConvert.SerializeObject(NewJsonFile);
        }
        VertexPositionTexture[] testArray = { new VertexPositionTexture(new Vector3(0), new Vector2(0)), new VertexPositionTexture(new Vector3(1), new Vector2(1)), new VertexPositionTexture(new Vector3(2), new Vector2(2)) };
        short[] Indextestarray = { 0, 1, 2 };


        VertexBuffer Vertices;
        IndexBuffer Indexes;
        string Name;
    }
}
