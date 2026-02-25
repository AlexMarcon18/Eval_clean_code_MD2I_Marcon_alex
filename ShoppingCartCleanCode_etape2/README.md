# Étape 2 - Refactorisation avec SOLID

## Architecture

```
ShoppingCartCleanCode/
├── Models/Product.cs
├── Validation/ProductValidator.cs
├── Discounts/
│   ├── IDiscountStrategy.cs
│   ├── QuantityDiscountStrategy.cs
│   └── TotalDiscountStrategy.cs
└── ShoppingCart.cs
```

## Principes SOLID appliqués

Chaque classe a une seule responsabilité:
- Product = données
- ProductValidator = validation
- Les stratégies = calcul des réductions
- ShoppingCart = orchestration

On peut ajouter de nouvelles réductions sans modifier le code existant
toutes les stratégies de réduction sont interchangeables
L'interface IDiscountStrategy est petite (2 membres seulement)
ShoppingCart dépend de l'interface IDiscountStrategy

## Différence avec l'étape 1

Étape 1 : Tout dans une seule classe Panier

Étape 2 : Séparé en plusieurs classes avec chacune sa responsabilité

L'avantage c'est qu'on peut maintenant ajouter de nouvelles règles de réduction sans toucher au code existant.
