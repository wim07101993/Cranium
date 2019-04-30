namespace Cranium.Data.Models
{
    public class Attachment : AWithId
    {
        #region FIELDS

        private string _name;

        private byte[] _file;
        private bool _fetchedFile;

        private string _value;
        private EAttachmentType _attachmentType;

        #endregion FIELDS


        #region PROPERTIES

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public byte[] File
        {
            get => _file;
            set => SetProperty(ref _file, value);
        }

        public bool FetchedFile
        {
            get => _fetchedFile;
            internal set => SetProperty(ref _fetchedFile, value);
        }

        public string Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public EAttachmentType AttachmentType
        {
            get => _attachmentType;
            set => SetProperty(ref _attachmentType, value);
        }

        #endregion PROPERTIES
    }
}
