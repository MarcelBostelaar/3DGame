using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Text;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;

namespace Game1.ModelsAndAnimation
{
    interface IVertexAndIndexBuffers
    {
        VertexBuffer Vertex_Buffer { get; }
        IndexBuffer Index_Buffer { get; }
        int PrimitiveCount { get; }
        string Name { get; }
    }

    public class ModelClass : IVertexAndIndexBuffers
    {
        [DebuggerDisplay("Name = {_name}")]

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public int PrimitiveCount
        {
            get
            {
                return _primitiveCount;
            }
            private set
            {
                _primitiveCount = value;
            }
        }

        string _name;
        int _primitiveCount;

        public VertexPositionTexture[] Vertices { get; set; }
        public short[] Indices { get { return indices; } set {
                indices = value;
                PrimitiveCount = Indices.Length / 3;
            } }

        public VertexBuffer Vertex_Buffer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IndexBuffer Index_Buffer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        private short[] indices;

        public static ModelClass LoadJson(string JsonName, string Path, GraphicsDevice GraphDevice)
        {
            try {
                var filestream = new System.IO.StreamReader(Path + JsonName + GlobalConstants.ModelClassFileExtention, Encoding.ASCII);
                var jsontoconvert = filestream.ReadToEnd();
                return LoadJson(jsontoconvert, GraphDevice);
            }
            catch(Exception e)
            {
                try {
                    var filestream = new System.IO.StreamReader(Path + JsonName + GlobalConstants.CakeLayerModelFileExtention, Encoding.ASCII);
                    var jsontoconvert = filestream.ReadToEnd();
                    var CakelayerModel = CakeLayerModel.LoadJSON(jsontoconvert);
                    return CakelayerModel.toModelClass();
                }
                catch(Exception ex)
                {
                    throw new ContentLoadException();
                }
            }
        }

        public static ModelClass LoadJson(string JSON, GraphicsDevice GraphDevice)
        {
            var ModelJSON = JsonConvert.DeserializeObject<ModelClassJson>(JSON);
            var newModel = new ModelClass();
            //newModel._indexBuffer = new IndexBuffer(GraphDevice, IndexElementSize.ThirtyTwoBits, ModelJSON.Indices.Length, BufferUsage.None);
            //newModel._indexBuffer.SetData(ModelJSON.Indices);
            //newModel._vertexBuffer = new VertexBuffer(GraphDevice, typeof(VertexPositionTexture), ModelJSON.Vertices.Length, BufferUsage.None);
            //newModel._vertexBuffer.SetData(ModelJSON.Vertices);
            newModel._name = ModelJSON.name;
            newModel._primitiveCount = ModelJSON.Indices.Length / 3;

            //Temporary until I found out why "DrawUserIndexedPrimitives" doesnt work.
            newModel.Vertices = ModelJSON.Vertices;
            newModel.Indices = ModelJSON.Indices;
            //END TEMP

            return newModel;
        }

        public void SaveJson(string Path)
        {
            var modeljson = new ModelClassJson();
            modeljson.name = "DUMMY_DATA_FOR_MODEL";
            modeljson.Indices = new short[] { 1, 2, 3, 4, 5, 6 };
            modeljson.Vertices = new VertexPositionTexture[6];
            for (int i = 0; i < 6; i++)
            {
                modeljson.Vertices[i] = new VertexPositionTexture(new Vector3(1, 2, 3), new Vector2(4, 5));
            }

            var json = JsonConvert.SerializeObject(modeljson, Formatting.Indented);
            System.IO.StreamWriter filestream;
            try {
                filestream = new System.IO.StreamWriter(Path + modeljson.name + ".json"); }
            catch(System.IO.DirectoryNotFoundException e)
            {
                System.IO.Directory.CreateDirectory(Path);
                filestream = new System.IO.StreamWriter(Path + modeljson.name);
            }
            filestream.Write(json);
            filestream.Close();
        }

        private class ModelClassJson
        {
            public string name;
            public VertexPositionTexture[] Vertices;
            public short[] Indices;
        }
    }
}
