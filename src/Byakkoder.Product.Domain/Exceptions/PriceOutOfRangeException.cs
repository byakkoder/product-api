namespace Byakkoder.Product.Domain.Exceptions
{
    public class PriceOutOfRangeException : Exception
    {
        public PriceOutOfRangeException(string message) : base(message) { }
    }
}
