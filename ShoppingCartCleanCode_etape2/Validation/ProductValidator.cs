namespace ShoppingCartCleanCode.Validation
{
    public static class ProductValidator
    {
        public static void Validate(string name, decimal price, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidProductException("Le nom du produit ne peut pas être vide.");

            if (price < 0)
                throw new InvalidProductException("Le prix ne peut pas être négatif.");

            if (quantity <= 0)
                throw new InvalidProductException("La quantité doit être supérieure à 0.");
        }
    }

    public class InvalidProductException : Exception
    {
        public InvalidProductException(string message) : base(message) { }
    }
}
