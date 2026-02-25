using ShoppingCartCleanCode.Models;

namespace ShoppingCartCleanCode.Discounts
{
    public interface IDiscountStrategy
    {
        decimal ApplyDiscount(IReadOnlyList<Product> products, decimal currentTotal);
        string Description { get; }
    }
}
