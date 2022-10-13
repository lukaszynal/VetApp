namespace VetApp.Classes
{
    public class QueryParameters
    {
        const int maxSize = 100;
        private int size = 50;

        public int Page { get; set; } = 1;
        public int Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = Math.Min(maxSize, value);
            }
        }
    }
}