using System.IO;
using System.Xml.Serialization;
using UnityModManagerNet;

namespace ProgressBar
{
    public class Setting : UnityModManager.ModSettings
    {
        public int R = 255;
        public int G = 255;
        public int B = 255;
        public float x = 50f;
        public float y = 100f;
        public float size = 100;
        public int height = 10;
        
        public override void Save(UnityModManager.ModEntry modEntry) {
            var filepath = GetPath(modEntry);
            try {
                using (var writer = new StreamWriter(filepath)) {
                    var serializer = new XmlSerializer(GetType());
                    serializer.Serialize(writer, this);
                }
            } catch {
            }
        }
       
        public override string GetPath(UnityModManager.ModEntry modEntry) {
            return Path.Combine(modEntry.Path, GetType().Name + ".xml");
        }
    }
}