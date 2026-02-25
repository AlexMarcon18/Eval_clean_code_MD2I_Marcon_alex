namespace ShoppingCartProject
{
    public class Panier
    {
        private readonly List<(string name, decimal price, int quantity)> _products = new();
        private const decimal QuantityDiscountThreshold = 10;
        private const decimal QuantityDiscountRate = 0.05m;
        private const decimal TotalDiscountThreshold = 100m;
        private const decimal TotalDiscountRate = 0.10m;

        /* ========== VERSION 1 ==========
         * Test: GetTotal_WithEmptyCart_ShouldThrowEmptyCartException
         * Implémentation initiale (bête et méchante) - Lance toujours l'exception
         * 
         * public decimal GetTotal()
         * {
         *     throw new EmptyCartException("Le panier est vide.");
         * }
         */

        /* ========== VERSION 2 ==========
         * Test: GetTotal_WithOneProduct_ReturnsPriceTimesQuantity
         * Ajout de AddProduct et calcul pour un seul produit (code naïf)
         * 
         * public void AddProduct(string name, decimal price, int quantity)
         * {
         *     _products.Add((name, price, quantity));
         * }
         * 
         * public decimal GetTotal()
         * {
         *     if (_products.Count == 0)
         *         throw new EmptyCartException("Le panier est vide.");
         *     
         *     var product = _products[0];  // Bête et méchant : juste le premier
         *     return product.price * product.quantity;
         * }
         */

        /* ========== VERSION 3 - REFACTOR ==========
         * Test: GetTotal_WithMultipleProducts_ReturnsSumOfSubtotals
         * Refactorisation : Boucle pour gérer N produits au lieu d'un seul
         * 
         * public decimal GetTotal()
         * {
         *     if (_products.Count == 0)
         *         throw new EmptyCartException("Le panier est vide.");
         *     
         *     decimal total = 0;
         *     foreach (var product in _products)
         *     {
         *         total += product.price * product.quantity;
         *     }
         *     return total;
         * }
         */

        /* ========== VERSION 4 ==========
         * Tests: GetTotal_WithTotalOver100_AppliesTenPercentDiscount
         *        GetTotal_WithTotalExactly100_DoesNotApplyDiscount
         * Ajout de la réduction de 10% si total > 100
         * 
         * public decimal GetTotal()
         * {
         *     if (_products.Count == 0)
         *         throw new EmptyCartException("Le panier est vide.");
         *     
         *     decimal total = 0;
         *     foreach (var product in _products)
         *     {
         *         total += product.price * product.quantity;
         *     }
         *     
         *     if (total > 100m)
         *         total = total * 0.9m;  // Réduction de 10%
         *     
         *     return total;
         * }
         */

        /* ========== VERSION 5 ==========
         * Tests: GetTotal_WithProductQuantityGreaterOrEqual10_AppliesFivePercentDiscountOnProduct
         *        GetTotal_WithMultipleProductsIncludingOneWithQuantity10_AppliesDiscountOnlyOnThatProduct
         *        GetTotal_WithProductQuantity9_DoesNotApplyDiscount
         * Ajout de la réduction de 5% par produit si quantité >= 10
         * 
         * public decimal GetTotal()
         * {
         *     if (_products.Count == 0)
         *         throw new EmptyCartException("Le panier est vide.");
         *     
         *     decimal total = 0;
         *     foreach (var product in _products)
         *     {
         *         decimal subtotal = product.price * product.quantity;
         *         
         *         if (product.quantity >= 10)
         *             subtotal = subtotal * 0.95m;  // Réduction de 5%
         *         
         *         total += subtotal;
         *     }
         *     
         *     if (total > 100m)
         *         total = total * 0.9m;
         *     
         *     return total;
         * }
         */

        /* ========== VERSION 6 ==========
         * Tests: AddProduct_WithNegativePrice_ShouldThrowInvalidProductException
         *        AddProduct_WithNegativeQuantity_ShouldThrowInvalidProductException
         *        AddProduct_WithZeroPrice_ShouldBeValid
         *        AddProduct_WithZeroQuantity_ShouldThrowInvalidProductException
         * Ajout de la validation dans AddProduct pour garantir un total non négatif
         */
        public void AddProduct(string name, decimal price, int quantity)
        {
            if (price < 0)
                throw new InvalidProductException("Le prix ne peut pas être négatif.");

            if (quantity <= 0)
                throw new InvalidProductException("La quantité doit être supérieure à 0.");

            _products.Add((name, price, quantity));
        }

        /* ========== VERSION 7 - REFACTOR FINAL ==========
         * Tests: GetTotal_WithBothDiscounts_AppliesBothCorrectly
         *        GetTotal_WithMultipleProductsIncludingQuantityDiscountAndTotalDiscount_AppliesAllDiscounts
         * Refactorisation finale après tous les tests :
         * - Extraction de méthodes privées (CalculateSubtotalsWithQuantityDiscount, ApplyTotalDiscount)
         * - Ajout de constantes pour remplacer les nombres magiques
         * - Amélioration de la lisibilité avec (1 - taux) au lieu de 0.95 / 0.9
         */
        public decimal GetTotal()
        {
            if (_products.Count == 0)
                throw new EmptyCartException("Le panier est vide.");

            decimal total = CalculateSubtotalsWithQuantityDiscount();
            total = ApplyTotalDiscount(total);
            return total;
        }

        private decimal CalculateSubtotalsWithQuantityDiscount()
        {
            decimal total = 0;
            foreach (var product in _products)
            {
                decimal subtotal = product.price * product.quantity;

                if (product.quantity >= QuantityDiscountThreshold)
                    subtotal = subtotal * (1 - QuantityDiscountRate);

                total += subtotal;
            }
            return total;
        }

        private decimal ApplyTotalDiscount(decimal total)
        {
            if (total > TotalDiscountThreshold)
                total = total * (1 - TotalDiscountRate);
            return total;
        }
    }

    public class EmptyCartException : Exception
    {
        public EmptyCartException(string? message) : base(message) { }
    }

    public class InvalidProductException : Exception
    {
        public InvalidProductException(string? message) : base(message) { }
    }
}
