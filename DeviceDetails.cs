namespace DataGifting
{
    public class DeviceDetails
    {

        private string _manufacturer;

        public string Manufacturer
        {
            get { return _manufacturer; }
            set { _manufacturer = value; }
        }

        private string _deviceName;

        public string DeviceName
        {
            get { return _deviceName; }
            set { _deviceName = value; }
        }

        private string _thumbnailUrl;

        public string ThumbnailUrl
        {
            get { return _thumbnailUrl; }
            set { _thumbnailUrl = value; }
        }

        private string _imageUrl;

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; }
        }
    }
}