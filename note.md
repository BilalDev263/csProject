conception d'une application mvc

priorisation de la version paire pour les sdk dotnet pour la maintenabilité

espade de nom virtuel contenant qui permet de faciliter export et limport de code source

création d'un CRUD complet

les méthodes de Controller ont les appels les actions

utilisation d'Entity Framework Core comme ORM

connexion à une base de données MySQL

gestion des erreurs et validation côté serveur

constructeur de la classe, on l'utilise à l'initialisation de l'objet en général

pour hériter d'une classe on ajoute ":" après la création de la classe enfant et on ajoute le nom de la classe mère

si un paramètre est ajoute dans le constructeur classe mère, il faut l'ajouter dans la classe enfant

faire attention à ne pas instancier dbcontexte à chaque connexion d'un nouveau user

injection de dépendance sert à optimiser le process

les modèles en csharp sont des business objects

authentification et autorization 

packet nugget asp net Identity pour mettre le système auth 

L'utilisation d'une structure MVC monolithique est la meilleur pour démarrer la quasi totalité de projet d'aplication it, car il permet d'utiliser le strict nécessaire à n'importe quel application et aussi d'évoluer vers une stucture plus complexe pour répondre à des besoin plus spécifiques et avancés

ViewModel sert à récupérer le bon format lors de l'envoi du formulaire, vérifier que les champs requis soit remplie dans le bon format etc et ne pas réutiliser la classe teacher par exemple qui à plus de champ que cela