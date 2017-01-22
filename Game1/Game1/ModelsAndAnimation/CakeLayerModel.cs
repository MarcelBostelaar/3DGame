using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.ModelsAndAnimation
{
    class CakeLayerModel
    {
        public CakeLayerModel()
        {
        }

        public struct Layer
        {
            public Layer(Point textureBlock, float CustomHeight = float.NaN)
            {
                TextureBlock = textureBlock;
                if (!float.IsNaN(CustomHeight))
                {
                    this.CustomHeight = new GenWrapp<float>(CustomHeight);
                }
                else
                {
                    this.CustomHeight = null;
                }
            }
            public Point TextureBlock;
            public GenWrapp<float> CustomHeight;
        }

        public float DefaultHeightDifference { get; set; }
        public int HorizontalTextures { get; set; }
        public int VerticalTextures { get; set; }
        public int Width { get;  set; }
        public int Height { get;  set; }
        public List<Layer> Layers { get; set; }

        //public static CakeLayerModel LoadJSON(string JsonName, string Path)
        //{
        //    try {
        //        var filestream = new System.IO.StreamReader(Path + JsonName + GlobalConstants.CakeLayerModelFileExtention, Encoding.ASCII);
        //        var Jsontoconvert = filestream.ReadToEnd();
        //        return LoadJSON(Jsontoconvert);
        //    }
        //    catch(Exception e)
        //    {
        //        throw new ContentLoadException();
        //    }
        //}

        public static CakeLayerModel LoadJSON(string JSON)
        {
            return JsonConvert.DeserializeObject<CakeLayerModel>(JSON);
        }


        private static readonly short[] Indices = { 0, 1, 3, 1, 2, 3 };

        public ModelClass toModelClass()
        {
            var toreturn = new ModelClass();
            var newIndices = new short[Layers.Count * 6];
            var newVertices = new VertexPositionTexture[Layers.Count * 4];
            float previousheight = 0;
            for (int i = 0; i < Layers.Count; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    newIndices[i * 6 + j] = (short)(Indices[j] + 4 * i);
                }

                float zheight;
                if (Layers[i].CustomHeight == null)
                {
                    zheight = DefaultHeightDifference + previousheight;
                }
                else
                {
                    zheight = Layers[i].CustomHeight.Value + previousheight;
                }
                previousheight = zheight;

                newVertices[i * 4 + 0] = new VertexPositionTexture(
                    new Vector3(0, zheight, 0),
                    new Vector2((float)Layers[i].TextureBlock.X / HorizontalTextures,
                        (float)Layers[i].TextureBlock.Y / VerticalTextures)
                    );
                newVertices[i * 4 + 1] = new VertexPositionTexture(
                    new Vector3(Height, zheight, 0),
                    new Vector2((float)(Layers[i].TextureBlock.X + 1) / HorizontalTextures,
                        (float)Layers[i].TextureBlock.Y / VerticalTextures)
                    );
                newVertices[i * 4 + 2] = new VertexPositionTexture(
                    new Vector3(Height, zheight, Width),
                    new Vector2((float)(Layers[i].TextureBlock.X + 1) / HorizontalTextures,
                        (float)(Layers[i].TextureBlock.Y + 1) / VerticalTextures)
                    );
                newVertices[i * 4 + 3] = new VertexPositionTexture(
                    new Vector3(0, zheight, Width),
                    new Vector2((float)Layers[i].TextureBlock.X / HorizontalTextures,
                        (float)(Layers[i].TextureBlock.Y + 1) / VerticalTextures)
                    );
            }
            toreturn.Indices = newIndices;
            toreturn.Vertices = newVertices;
            return toreturn;
        }

        public void SaveJson(string Path, string name)
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            System.IO.StreamWriter filestream;
            try
            {
                filestream = new System.IO.StreamWriter(Path + name + ".json");
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                System.IO.Directory.CreateDirectory(Path);
                filestream = new System.IO.StreamWriter(Path + name);
            }
            filestream.Write(json);
            filestream.Close();
        }
    }
}
