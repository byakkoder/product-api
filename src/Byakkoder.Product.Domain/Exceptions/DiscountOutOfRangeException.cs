namespace Byakkoder.Product.Domain.Exceptions
{
    public class DiscountOutOfRangeException : Exception
    {
        public DiscountOutOfRangeException(string message) : base(message) { }
    }
}
