using Game1.ModelsAndAnimation;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class CustomContentManager
    {
        private struct AssetNameAndType
        {
            public AssetNameAndType(string AssetName, Type type)
            {
                this.AssetName = AssetName;
                this.type = type;
            }
            public string AssetName;
            public Type type;
        }

        Dictionary<AssetNameAndType, object> LoadedContent = new Dictionary<AssetNameAndType, object>();
        ContentManager MonogameManager;
        GraphicsDevice Graphdevice;

        string ModelFolderPath = "Content/Models/";
        string PlaceHolderName = "Placeholder";

        public CustomContentManager(ContentManager Manager, GraphicsDevice Device)
        {
            MonogameManager = Manager;
            Graphdevice = Device;
            var placeHolderImageKey = new AssetNameAndType(PlaceHolderName, typeof(Texture2D));
            var placeholderimage = PlaceHolderFiles.GetPlaceholderTexture(500, 10, Device);
            LoadedContent.Add(placeHolderImageKey, placeholderimage);
            var placeHolderModelKey = new AssetNameAndType(PlaceHolderName, typeof(ModelClass));
            var placeholdermodel = PlaceHolderFiles.GetPlaceholderModel(Device);
            LoadedContent.Add(placeHolderModelKey, placeholdermodel);
        }

        public T Load<T>(string AssetName)
        {
            var Key = new AssetNameAndType(AssetName, typeof(T));
            if (LoadedContent.ContainsKey(Key))
            {
                return (T)LoadedContent[Key];
            }
            else
            {
                try {
                    object ReturnVariable;


                    if (typeof(T) == typeof(ModelClass))
                    {
                        ReturnVariable = ModelClass.LoadJson(AssetName, ModelFolderPath, Graphdevice);
                    }
                    else
                    {
                        try
                        {
                            ReturnVariable = LoadMonogameManager<T>(AssetName);
                        }
                        catch (NotSupportedException e)
                        {
                            throw new NotSupportedException("Format not supported");
                        }
                    }

                    LoadedContent.Add(Key, ReturnVariable);
                    return (T)ReturnVariable;
                }
                catch(ContentLoadException e)
                {
                    Key.AssetName = PlaceHolderName;
                    if (LoadedContent.ContainsKey(Key))
                    {
                        return (T) LoadedContent[Key];
                    }
                    else
                    {
                        throw new ContentLoadException("Content could not be loaded and no placeholder file exists for the specified type of content");
                    }
                }
            }
        }

        private T LoadMonogameManager<T>(string AssetName)
        {
            return MonogameManager.Load<T>(AssetName);
        }
    }
}
