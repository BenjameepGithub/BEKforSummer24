namespace Utilities.Serializers {
    public interface ISerializer {
        /// <summary>
        /// Example Value: ".json" or ".xml"
        /// </summary>
        public string FileExtension { get; }
        string Serialize<T>(T obj);
        T Deserialize<T>(string fileText);
    }
    
    /// <summary>
    /// An Encrypted Version Of A ISerializer.
    /// </summary>
    public sealed class EncryptedSerializer : ISerializer {
        public string FileExtension { get; }
        
        private readonly ISerializer _baseSerializer;
        
        public EncryptedSerializer(ISerializer basBaseSerializer = null) {
            _baseSerializer = basBaseSerializer ?? new JSONSerializer();
            FileExtension = _baseSerializer.FileExtension;
        }
        
        public string Serialize<T>(T obj) {
            return _baseSerializer.Serialize(obj).Encrypted();
        }
        
        public T Deserialize<T>(string fileText) {
            return _baseSerializer.Deserialize<T>(fileText.Decrypted());
        }
    }
}