using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class SimpleBox
    {
        public SimpleBox(Vector3 BottomCentre, Vector3 widthVector, Vector3 lenghtVector, Vector3 HeightVector)
        {
            BottomTopLeft = BottomCentre - widthVector / 2 - lenghtVector / 2;
            BottomTopRight = BottomCentre + widthVector / 2 - lenghtVector / 2;
            BottomBottomLeft = BottomCentre - widthVector / 2 + lenghtVector / 2;
            BottomBottomRight = BottomCentre + widthVector / 2 + lenghtVector / 2;

            TopTopLeft = BottomCentre - widthVector / 2 - lenghtVector / 2 + HeightVector;
            TopTopRight = BottomCentre + widthVector / 2 - lenghtVector / 2 + HeightVector;
            TopBottomLeft = BottomCentre - widthVector / 2 + lenghtVector / 2 + HeightVector;
            TopBottomRight = BottomCentre + widthVector / 2 + lenghtVector / 2 + HeightVector;
            FillVertices();
        }
        Vector3 BottomTopLeft, BottomTopRight, BottomBottomRight, BottomBottomLeft, TopTopLeft, TopTopRight, TopBottomRight, TopBottomLeft;

        public VertexPositionTexture[] Vertices;
        public short[] Indexes;

        private void FillVertices()
        {
            Vertices = new VertexPositionTexture[8];
            Vertices[0] = new VertexPositionTexture(BottomTopLeft, new Vector2(0, 0));
            Vertices[1] = new VertexPositionTexture(BottomTopRight, new Vector2(1, 0));
            Vertices[2] = new VertexPositionTexture(BottomBottomLeft, new Vector2(0, 1));
            Vertices[3] = new VertexPositionTexture(BottomBottomLeft, new Vector2(1, 1));

            Vertices[4] = new VertexPositionTexture(TopTopLeft, new Vector2(0, 0));
            Vertices[5] = new VertexPositionTexture(TopTopRight, new Vector2(1, 0));
            Vertices[6] = new VertexPositionTexture(TopBottomLeft, new Vector2(0, 1));
            Vertices[7] = new VertexPositionTexture(TopBottomLeft, new Vector2(1, 1));

            Indexes = new short[]
            {
                0,2,3, //buttom
                0,3,1,
                0,1,4, //back
                1,5,4,
                4,6,2, //left
                0,4,2,
                3,7,1, //right
                7,5,1, 
                4,5,7, //top
                4,7,6,
                6,3,2, //front
                6,7,3
            };
        }
    }
}
