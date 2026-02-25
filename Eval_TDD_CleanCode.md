# Evaluation TDD / Clean Code
## PREMIERE ETAPE ! (TDD)
### Objectif
- La première étape vous incite à faire du Clean Code, mais à cette étape, vous n'avez pas besoin d'implémenter tous les principes du clean code que l'on a vu ensemble. Le but étant de vous focaliser ici sur le TDD (impliquant du clean code, mais pour l'instant que sur une seule classe).
- Implementer en TDD **UNE SEULE** classe permettant de calculer le prix total d'un panier d'achat avec des règles de reduction progressives.
- Vous devez suivre les règles du TDD au maximum (red, green, refactor + demarrer simple + coder betement ce qui fait passer le test)

### Contexte
- J'insiste, pour l'instant, vous n'avez pas besoin d'appliquer tous les principes SOLID, l'idée étant d'éviter de devoir utiliser des mocks etc... Donc juste une classe panier, pas de classe produit à cette étape.
- Un client peut ajouter des produits dans un panier.
- Chaque produit possède un nom, un prix unitaire et une quantité (PAS DE CLASSE PRODUIT ICI A CETTE ETAPE)
- La méthode GetTotal() doit calculer le prix total en appliquant des règles simples, puis evolutives.
- L'objectif est de construire le calcul pas à pas en ajoutant une règle a la fois.

### Règles
- Règle 1 : l'appel à la méthode GetTotal() sur un panier vide lève une exception custom à la classe panier.
- Règle 2 : un panier avec un seul produit retourne prix unitaire multiplie par quantité.
- Règle 3 : un panier avec plusieurs produits retourne la somme des sous totaux.
- Règle 4 : si le total depasse 100 euros, appliquer une reduction de 10 pourcent sur le total.
- Règle 5 : si un produit a une quantité superieure ou egale a 10, appliquer une reduction de 5 pourcent sur ce produit uniquement.
- Règle 6 : le total ne peut jamais être négatif (Ici, y'a plusieurs moyen de gérer ça, je suis intentionnellement un peu "flou" pour voir comment vous estimez que c'est le plus propice de gérer cet règle).

### Contraintes
- Vous ne devez pas écrire tout le code d'un coup.
- Vous devez écrire un test avant chaque modification du code.
- Chaque test doit être simple et precis.
- Vous devez factoriser après chaque test (ça va être difficile pour moi de bien le voir ça, alors hésitez pas à mettre des versions de votre code en commentaire avec l'intitulé du test correspondant au moment de la refacto)
- Ajoutez les règles dans l'ordre.

### Attendu
- Une classe Panier.
- Une methode AddProduct(string name, decimal price, int quantity).
- Une methode GetTotal() qui retourne un decimal.
- Une suite de tests montrant la progression du developpement en TDD (un test par regle, puis des tests supplementaires si besoin).

## DEUXIEME ETAPE ! (Clean Code)
- Ici, vous pouvez créer un autre projet dans la solution (pas besoin de tester le code de cette étape)
- L'objectif est de repartir de votre classe panier précédente, normalement déjà assez propre car vous avez refacto à chaque tests
- MAIS, il faut respecter l'intégralité des principes du Clean Code et les principes SOLID que l'on a vu durant la formation au maximum !
- Donc, vous n'aurez plus qu'une seule classe... 
- A vous de me rendre le truc le plus clean possible

