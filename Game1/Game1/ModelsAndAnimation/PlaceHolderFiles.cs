using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.ModelsAndAnimation
{
    public static class PlaceHolderFiles
    {
        public static Texture2D GetPlaceholderTexture(int dimension, int CheckerBoardSize, GraphicsDevice graphDevice)
        {
            Color Purple, Black;
            Purple = new Color(255,0,255);
            Black = new Color(0,0,0);
            Texture2D newone = new Texture2D(graphDevice, dimension, dimension);
            var TextureData = new Color[dimension, dimension];
            for (int x = 0; x < dimension; x++)
            {
                for (int y = 0; y < dimension; y++)
                {
                    if(x/CheckerBoardSize%2 == 0)
                    {
                        if (y / CheckerBoardSize % 2 == 0)
                        {
                            TextureData[x, y] = Purple;
                        }
                        else
                        {
                            TextureData[x, y] = Black;
                        }
                    }
                    else
                    {
                        if (y / CheckerBoardSize % 2 != 0)
                        {
                            TextureData[x, y] = Purple;
                        }
                        else
                        {
                            TextureData[x, y] = Black;
                        }
                    }
                }
            }
            var oneDimensionalTextureData = new Color[dimension * dimension];
            int counter = 0;
            for (int x = 0; x < dimension; x++)
            {
                for (int y = 0; y < dimension; y++)
                {
                    oneDimensionalTextureData[counter] = TextureData[x, y];
                    counter++;
                }
            }
            newone.SetData<Color>(oneDimensionalTextureData);
            newone.Name = "Placeholder image";
            return newone;
        }

        private const string PlaceHolderJson = "{\r\n  \"name\": \"ExclamationPoint\",\r\n  \"Vertices\": [\r\n    {//0\r\n      \"Position\": \"-2, 14, -2\",\r\n      \"TextureCoordinate\": \"0, 0\"\r\n    },\r\n    {//1\r\n      \"Position\": \"2, 14, -2\",\r\n      \"TextureCoordinate\": \"0.25, 0\"\r\n    },\r\n    {//2\r\n      \"Position\": \"-1.5, 6, -1.5\",\r\n      \"TextureCoordinate\": \"0, 0.6\"\r\n    },\r\n    {//3\r\n      \"Position\": \"1.5, 6, -1.5\",\r\n      \"TextureCoordinate\": \"0.25, 0.6\"\r\n    },\r\n\t\r\n\t\r\n    {//4\r\n      \"Position\": \"2, 14, 2\",\r\n      \"TextureCoordinate\": \"0.5, 0\"\r\n    },\r\n    {//5\r\n      \"Position\": \"1.5, 6, 1.5\",\r\n      \"TextureCoordinate\": \"0.5, 0.6\"\r\n    },\r\n\t\r\n\t\r\n    {//6\r\n      \"Position\": \"-2, 14, 2\",\r\n      \"TextureCoordinate\": \"0.75, 0\"\r\n    },\r\n    {//7\r\n      \"Position\": \"-1.5, 6, 1.5\",\r\n      \"TextureCoordinate\": \"0.75, 0.6\"\r\n    },\r\n\t\r\n\t\r\n    { //8\r\n      \"Position\": \"-2, 14, -2\",\r\n      \"TextureCoordinate\": \"1, 0\"\r\n    },\r\n    { //9\r\n      \"Position\": \"-1.5, 6, -1.5\",\r\n      \"TextureCoordinate\": \"1, 0.6\"\r\n    },\r\n\t\r\n\t\r\n\t//bottom half\r\n    {//10\r\n      \"Position\": \"-1.5, 4, -1.5\",\r\n      \"TextureCoordinate\": \"0, 0.4\"\r\n    },\r\n    {//11\r\n      \"Position\": \"1.5, 4, -1.5\",\r\n      \"TextureCoordinate\": \"0.25, 0.4\"\r\n    },\r\n    {//12\r\n      \"Position\": \"-1.5, 0, -1.5\",\r\n      \"TextureCoordinate\": \"0, 1\"\r\n    },\r\n    {//13\r\n      \"Position\": \"1.5, 0, -1.5\",\r\n      \"TextureCoordinate\": \"0.25, 1\"\r\n    },\r\n\t\r\n\t\r\n    {//14\r\n      \"Position\": \"1.5, 4, 1.5\",\r\n      \"TextureCoordinate\": \"0.5, 0.4\"\r\n    },\r\n    {//15\r\n      \"Position\": \"1.5, 0, 1.5\",\r\n      \"TextureCoordinate\": \"0.5, 1\"\r\n    },\r\n\t\r\n\t\r\n    {//16\r\n      \"Position\": \"-1.5, 4, 1.5\",\r\n      \"TextureCoordinate\": \"0.75, 0.4\"\r\n    },\r\n    {//17\r\n      \"Position\": \"-1.5, 0, 1.5\",\r\n      \"TextureCoordinate\": \"0.75, 1\"\r\n    },\r\n\t\r\n\t\r\n    { //18\r\n      \"Position\": \"-1.5, 4, -1.5\",\r\n      \"TextureCoordinate\": \"1, 0.4\"\r\n    },\r\n    { //19\r\n      \"Position\": \"-1.5, 0, -1.5\",\r\n      \"TextureCoordinate\": \"1, 1\"\r\n    }\r\n  ],\r\n  \"Indices\": [\t\r\n\t2,1,0,\r\n\t2,3,1,\r\n\t\r\n\t3,4,1,\r\n\t3,5,4,\r\n\t\r\n\t7,6,4,\r\n\t5,7,4,\r\n\t\r\n\t9,8,6,\r\n\t7,9,6,\r\n\t\r\n\t1,4,0,\r\n\t4,6,0,\r\n\t\r\n\t3,2,5,\r\n\t7,5,2,\r\n\t\r\n\t\r\n\t//bottom half\r\n\t12,11,10,\r\n\t12,13,11,\r\n\t\r\n\t13,14,11,\r\n\t13,15,14,\r\n\t\r\n\t17,16,14,\r\n\t15,17,14,\r\n\t\r\n\t19,18,16,\r\n\t17,19,16,\r\n\t\r\n\t11,14,10,\r\n\t14,16,10,\r\n\t\r\n\t13,12,15,\r\n\t17,15,12\r\n  ]\r\n}";

        public static ModelClass GetPlaceholderModel(GraphicsDevice graphDevice)
        {
            return ModelClass.LoadJson(PlaceHolderJson, graphDevice);
        }
    }
}
