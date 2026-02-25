using ShoppingCartCleanCode.Models;
using ShoppingCartCleanCode.Validation;
using ShoppingCartCleanCode.Discounts;

namespace ShoppingCartCleanCode
{
    public class ShoppingCart
    {
        private readonly List<Product> _products = new();
        private readonly List<IDiscountStrategy> _discountStrategies = new();

        public IReadOnlyList<Product> Products => _products.AsReadOnly();

        public ShoppingCart()
        {
            _discountStrategies.Add(new QuantityDiscountStrategy());
            _discountStrategies.Add(new TotalDiscountStrategy());
        }

        public ShoppingCart(params IDiscountStrategy[] strategies)
        {
            _discountStrategies.AddRange(strategies);
        }

        public void AddProduct(string name, decimal price, int quantity)
        {
            ProductValidator.Validate(name, price, quantity);
            _products.Add(new Product(name, price, quantity));
        }

        public decimal GetTotal()
        {
            if (_products.Count == 0)
                throw new EmptyCartException("Le panier est vide.");

            decimal total = CalculateBaseTotal();

            foreach (var strategy in _discountStrategies)
            {
                total = strategy.ApplyDiscount(_products, total);
            }

            return total;
        }

        private decimal CalculateBaseTotal()
        {
            decimal total = 0;
            foreach (var product in _products)
            {
                total += product.Price * product.Quantity;
            }
            return total;
        }

        public void AddDiscountStrategy(IDiscountStrategy strategy)
        {
            _discountStrategies.Add(strategy);
        }
    }

    public class EmptyCartException : Exception
    {
        public EmptyCartException(string message) : base(message) { }
    }
}
