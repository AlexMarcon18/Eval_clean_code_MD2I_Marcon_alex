namespace ShoppingCartProject.UnitTests
{
    [TestClass]
    public sealed class PanierTests
    {
        private Panier _panier;

        [TestInitialize]
        public void Init()
        {
            _panier = new Panier();
        }

        // RÈGLE 1 : l'appel à la méthode GetTotal() sur un panier vide lève une exception custom à la classe panier
        [TestMethod]
        public void GetTotal_WithEmptyCart_ShouldThrowEmptyCartException()
        {
            Assert.ThrowsException<EmptyCartException>(() => _panier.GetTotal());
        }

        // RÈGLE 2 : un panier avec un seul produit retourne prix unitaire multiplie par quantité
        [TestMethod]
        public void GetTotal_WithOneProduct_ReturnsPriceTimesQuantity()
        {
            _panier.AddProduct("Pomme", 2.5m, 3);
            var result = _panier.GetTotal();
            Assert.AreEqual(7.5m, result);
        }

        // RÈGLE 3 : un panier avec plusieurs produits retourne la somme des sous totaux
        [TestMethod]
        public void GetTotal_WithMultipleProducts_ReturnsSumOfSubtotals()
        {
            _panier.AddProduct("Pomme", 2.5m, 3);    // 7.5
            _panier.AddProduct("Banane", 1.5m, 2);   // 3.0
            _panier.AddProduct("Orange", 3.0m, 1);   // 3.0
            var result = _panier.GetTotal();
            Assert.AreEqual(13.5m, result);
        }

        // RÈGLE 4 : si le total depasse 100 euros, appliquer une reduction de 10 pourcent sur le total
        [TestMethod]
        public void GetTotal_WithTotalOver100_AppliesTenPercentDiscount()
        {
            _panier.AddProduct("Ordinateur", 120m, 1);  // 120
            var result = _panier.GetTotal();
            Assert.AreEqual(108m, result);  // 120 - 10% = 108
        }

        [TestMethod]
        public void GetTotal_WithTotalExactly100_DoesNotApplyDiscount()
        {
            _panier.AddProduct("Produit", 100m, 1);  // 100
            var result = _panier.GetTotal();
            Assert.AreEqual(100m, result);  // Pas de réduction à 100
        }

        // RÈGLE 5 : si un produit a une quantité superieure ou egale a 10, appliquer une reduction de 5 pourcent sur ce produit uniquement
        [TestMethod]
        public void GetTotal_WithProductQuantityGreaterOrEqual10_AppliesFivePercentDiscountOnProduct()
        {
            _panier.AddProduct("Stylo", 2m, 10);  // 20 - 5% = 19
            var result = _panier.GetTotal();
            Assert.AreEqual(19m, result);
        }

        [TestMethod]
        public void GetTotal_WithMultipleProductsIncludingOneWithQuantity10_AppliesDiscountOnlyOnThatProduct()
        {
            _panier.AddProduct("Stylo", 2m, 10);     // 20 - 5% = 19
            _panier.AddProduct("Cahier", 3m, 5);     // 15 (pas de réduction)
            var result = _panier.GetTotal();
            Assert.AreEqual(34m, result);  // 19 + 15
        }

        [TestMethod]
        public void GetTotal_WithProductQuantity9_DoesNotApplyDiscount()
        {
            _panier.AddProduct("Stylo", 2m, 9);  // 18 (pas de réduction)
            var result = _panier.GetTotal();
            Assert.AreEqual(18m, result);
        }

        // RÈGLE 6 : le total ne peut jamais être négatif
        // Je gère ça en empêchant l'ajout de prix ou quantités négatives
        [TestMethod]
        public void AddProduct_WithNegativePrice_ShouldThrowInvalidProductException()
        {
            Assert.ThrowsException<InvalidProductException>(() => _panier.AddProduct("Produit", -10m, 2));
        }

        [TestMethod]
        public void AddProduct_WithNegativeQuantity_ShouldThrowInvalidProductException()
        {
            Assert.ThrowsException<InvalidProductException>(() => _panier.AddProduct("Produit", 10m, -2));
        }

        [TestMethod]
        public void AddProduct_WithZeroPrice_ShouldBeValid()
        {
            _panier.AddProduct("Cadeau", 0m, 1);
            var result = _panier.GetTotal();
            Assert.AreEqual(0m, result);
        }

        [TestMethod]
        public void AddProduct_WithZeroQuantity_ShouldThrowInvalidProductException()
        {
            Assert.ThrowsException<InvalidProductException>(() => _panier.AddProduct("Produit", 10m, 0));
        }

        // TESTS SUPPLÉMENTAIRES : Combinaison de règles
        [TestMethod]
        public void GetTotal_WithBothDiscounts_AppliesBothCorrectly()
        {
            // Produit avec quantité >= 10 (réduction 5%) ET total > 100 (réduction 10%)
            _panier.AddProduct("Livre", 11m, 10);  // 110 - 5% = 104.5, puis 104.5 - 10% = 94.05
            var result = _panier.GetTotal();
            Assert.AreEqual(94.05m, result);
        }

        [TestMethod]
        public void GetTotal_WithMultipleProductsIncludingQuantityDiscountAndTotalDiscount_AppliesAllDiscounts()
        {
            _panier.AddProduct("Stylo", 2m, 10);      // 20 - 5% = 19
            _panier.AddProduct("Cahier", 5m, 20);     // 100 - 5% = 95
            // Total = 19 + 95 = 114, puis 114 - 10% = 102.6
            var result = _panier.GetTotal();
            Assert.AreEqual(102.6m, result);
        }
    }
}
