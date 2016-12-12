﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class BetterQuad
    {
        public BetterQuad(Vector3 TopLefCorner, Vector3 HorizontalSide, Vector3 VerticalSide)
        {
            UpperLeft = TopLefCorner;
            UpperRight = TopLefCorner + HorizontalSide;
            LowerLeft = TopLefCorner + VerticalSide;
            LowerRight = TopLefCorner + HorizontalSide + VerticalSide;

            Vertices = new VertexPositionNormalTexture[4];
            Indexes = new short[6];
            Normal = Vector3.Cross(VerticalSide, HorizontalSide);

            FillVertices();
        }

        public BetterQuad(Vector3 origin, Vector3 normal, Vector3 up,
        float width, float height)
        {
            Vertices = new VertexPositionNormalTexture[4];
            Indexes = new short[6];
            Origin = origin;
            Normal = normal;
            Up = up;

            // Calculate the quad corners
            Left = Vector3.Cross(normal, Up);
            Vector3 uppercenter = (Up * height / 2) + origin;
            UpperLeft = uppercenter + (Left * width / 2);
            UpperRight = uppercenter - (Left * width / 2);
            LowerLeft = UpperLeft - (Up * height);
            LowerRight = UpperRight - (Up * height);

            FillVertices();
        }

        private void FillVertices()
        {
            // Fill in texture coordinates to display full texture
            // on quad
            Vector2 textureUpperLeft = new Vector2(0.0f, 0.0f);
            Vector2 textureUpperRight = new Vector2(1.0f, 0.0f);
            Vector2 textureLowerLeft = new Vector2(0.0f, 1.0f);
            Vector2 textureLowerRight = new Vector2(1.0f, 1.0f);

            // Provide a normal for each vertex
            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i].Normal = Normal;
            }

            // Set the position and texture coordinate for each
            // vertex
            Vertices[0].Position = LowerLeft;
            Vertices[0].TextureCoordinate = textureLowerLeft;
            Vertices[1].Position = UpperLeft;
            Vertices[1].TextureCoordinate = textureUpperLeft;
            Vertices[2].Position = LowerRight;
            Vertices[2].TextureCoordinate = textureLowerRight;
            Vertices[3].Position = UpperRight;
            Vertices[3].TextureCoordinate = textureUpperRight;


            // Set the index buffer for each vertex, using
            // clockwise winding
            Indexes[0] = 0;
            Indexes[1] = 1;
            Indexes[2] = 2;
            Indexes[3] = 2;
            Indexes[4] = 1;
            Indexes[5] = 3;
        }

        public short[] Indexes { get; private set; }
        public Vector3 Left { get; private set; }
        public Vector3 Normal { get; private set; }
        public Vector3 Origin { get; private set; }
        public Vector3 Up { get; private set; }
        public Vector3 UpperLeft { get; private set; }
        public Vector3 UpperRight { get; private set; }
        public Vector3 LowerLeft { get; private set; }
        public Vector3 LowerRight { get; private set; }
        public VertexPositionNormalTexture[] Vertices { get; private set; }
    }
}