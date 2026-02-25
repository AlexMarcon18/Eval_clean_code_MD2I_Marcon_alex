using ShoppingCartCleanCode.Models;

namespace ShoppingCartCleanCode.Discounts
{
    public class TotalDiscountStrategy : IDiscountStrategy
    {
        private const decimal TotalThreshold = 100m;
        private const decimal DiscountRate = 0.10m;

        public string Description => $"Réduction de {DiscountRate * 100}% si le total dépasse {TotalThreshold}€";

        public decimal ApplyDiscount(IReadOnlyList<Product> products, decimal currentTotal)
        {
            if (currentTotal > TotalThreshold)
                return currentTotal * (1 - DiscountRate);

            return currentTotal;
        }
    }
}
