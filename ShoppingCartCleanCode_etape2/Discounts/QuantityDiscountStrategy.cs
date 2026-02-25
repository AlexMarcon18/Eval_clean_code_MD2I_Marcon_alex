using ShoppingCartCleanCode.Models;

namespace ShoppingCartCleanCode.Discounts
{
    public class QuantityDiscountStrategy : IDiscountStrategy
    {
        private const int QuantityThreshold = 10;
        private const decimal DiscountRate = 0.05m;

        public string Description => $"Réduction de {DiscountRate * 100}% pour les produits avec quantité >= {QuantityThreshold}";

        public decimal ApplyDiscount(IReadOnlyList<Product> products, decimal currentTotal)
        {
            decimal total = 0;

            foreach (var product in products)
            {
                decimal subtotal = product.Price * product.Quantity;

                if (product.Quantity >= QuantityThreshold)
                    subtotal *= (1 - DiscountRate);

                total += subtotal;
            }

            return total;
        }
    }
}
