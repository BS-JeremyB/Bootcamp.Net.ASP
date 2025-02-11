# ASP.NET MVC : Vue d'ensemble complète

Retour et mise en avant des éléments abordés lors du cours d'ASP MVC.
Partant de l'utilisation du Modèle Vue Controleur à l'injection de dépendance.

## Composants et Interactions

ASP.NET MVC comprend trois composants fondamentaux :

- **Modèles (Models) :** Représentent les données et la logique métier à l'aide de classes qui se mappent sur des tables de base de données ou des sources de données externes.

  **Exemple :**

  ```csharp
  public class Movie
  {
      public int Id { get; set; }
      public string Title { get; set; }
      public string Description { get; set; }
      // ... autres propriétés
  }
  ```

- **Vues (Views) :** Rendent les interfaces utilisateur (UIs) en fonction des données du modèle et répondent aux interactions de l'utilisateur. La syntaxe Razor (cshtml) fournit un langage de templating flexible.

  **Exemple :**

  ```html
  @model Movie

  <h2>@Model.Title</h2>
  <p>@Model.Description</p>
  ```

- **Contrôleurs (Controllers) :** Orchestrant le flux de l'application, gèrent les demandes des utilisateurs, récupèrent des données du modèle et mettent à jour les vues en conséquence. Ils exploitent les modèles et les vues pour offrir des expériences web dynamiques.

  **Exemple :**

  ```csharp
  public class MovieController : Controller
  {
      public IActionResult Index()
      {
          // Recupération des films via un service qui communique avec le serveur
          Movie movies = _movieService.GetAllMovies();
          return View(movies);
      }

      // Autres actions du contrôleur...
  }
  ```
Ces composants interagissent de manière transparente :

1. **L'utilisateur initie une requête :** Un utilisateur envoie une requête (par exemple, une URL, une soumission de formulaire) au serveur Web.
2. **Le routage mappe la requête :** Le système de routage (basé sur app.MapControllerRoute) interprète l'URL et identifie l'action de contrôleur appropriée pour la traiter.
3. **Le contrôleur traite la requête :** Le contrôleur récupère des données du modèle (en utilisant des services si nécessaire), effectue toute logique métier nécessaire et sélectionne la vue appropriée.
4. **La vue rend la réponse :** La vue sélectionnée reçoit les données du modèle du contrôleur et utilise la syntaxe Razor pour générer la réponse HTML envoyée de retour au navigateur de l'utilisateur.


  **Exemple :**

  ```csharp

  @foreach (var item in Model)
  {
    <div>
        <dl class="row">
            <dt class="col-sm-2">
                @Html.DisplayNameFor(model => item.PosterUrl)
            </dt>
            <dd class="col-sm-10">
                <img src="@Html.DisplayFor(model => item.PosterUrl)" alt="Movie Poster of @item.Title" />
            </dd>
            <dt class="col-sm-2">
                @Html.DisplayNameFor(model => item.Title)
            </dt>
            <dd class="col-sm-10">
                @Html.DisplayFor(modelItem => item.Title)
            </dd>

            // Autres actions dans la vue...
        </dl>
  }

  ```

## Routage et Configuration

- **`app.UseRouting()` :** Active le système de routage pour identifier les actions de contrôleur en fonction des URLs.

- **`app.MapControllerRoute(name, pattern)` :** Définit les règles de routage. Le `name` est facultatif, et le `pattern` spécifie comment les URLs se mappent sur les contrôleurs et les actions (par exemple, `{controller}/{action}/{id}`).

## Les viewbags, les viewdata, les tempdata

- **ViewBag :** ViewBag est un sac fourre-tout dynamique qui permet de transmettre des données du contrôleur à la vue. Il est utilisé pour envoyer des données temporaires à la vue et peut être utilisé pour transporter des données entre les actions d'un contrôleur.

  **Exemple :**
  ```csharp
  public IActionResult Index()
  {
      ViewBag.Message = "Bienvenue sur notre site!";
      return View();
  }
  ```
  ```html
  <p>@ViewBag.Message</p>
  ```

- **ViewData :** ViewData est un dictionnaire de données clé-valeur qui est également utilisé pour transporter des données du contrôleur à la vue. Il est similaire à ViewBag, mais il offre un typage fort.

  **Exemple :**
  ```csharp
  public IActionResult Index()
  {
      ViewData["Message"] = "Bienvenue sur notre site!";
      return View();
  }
  ```
  ```html
  <p>@ViewData["Message"]</p>
  ```

- **TempData :** TempData est un dictionnaire qui stocke des données de session de manière temporaire. Les données stockées dans TempData sont disponibles pour la requête suivante et sont généralement utilisées pour passer des données entre des actions d'un contrôleur.

  **Exemple :**
  ```csharp
  public IActionResult Index()
  {
      TempData["Message"] = "Bienvenue sur notre site!";
      return RedirectToAction("Welcome");
  }

  public IActionResult Welcome()
  {
      string message = TempData["Message"] as string;
      return View((object)message);
  }
  ```
  ```html
  <p>@Model</p>
  ```

### Les validations de formulaire et le ModelState

- **Validations de formulaire :** Les annotations de validation de données telles que `[Required]`, `[StringLength]`, `[Range]`, etc., peuvent être appliquées aux propriétés d'un modèle pour valider les données saisies par l'utilisateur dans un formulaire.

  **Exemple :**
  ```csharp
  public class MovieForm
  {
      [Required]
      [StringLength(100)]
      public string Title { get; set; }

      [Required]
      [StringLength(int.MaxValue, MinimumLength = 5)]
      public string Description { get; set; }

      [Range(0, 5, ErrorMessage = "La note du film doit être entre 0 et 5")]
      public int Score { get; set; }
  }
  ```

- **ModelState :** ModelState est une collection qui contient les résultats de la liaison de modèle et de la validation de modèle. Il est utilisé pour vérifier si un modèle est valide ou non et pour afficher des messages d'erreur associés aux propriétés du modèle.

  **Exemple :**
  ```csharp
  [HttpPost]
  public IActionResult Create(MovieForm movie)
  {
      if (ModelState.IsValid)
      {
          return View(movie);
      }
      // Autre logique de création du film
  }
  ```

### Déroulement d'une Requête (Exemple : Edit)

Lorsqu'une requête est envoyée pour éditer une ressource, telle qu'un film, voici l'ordre de passage typique :

1. **Contrôleur (Controller) :** Le contrôleur reçoit la demande HTTP et appelle la méthode appropriée pour gérer cette demande. Dans notre cas, `Edit(int id)` est appelée.

2. **Service :** Le contrôleur utilise un service pour effectuer des opérations métier, telles que la récupération des détails du film à éditer.

3. **Modèle (Model) :** Le service interagit avec le modèle pour obtenir les données nécessaires, telles que les détails du film à éditer.

4. **Vue (View) :** Une fois que le modèle a été mis à jour, le contrôleur sélectionne la vue appropriée pour afficher les détails du film à éditer.

5. **Affichage de la Vue :** La vue est rendue avec les détails du film à éditer, prêts à être modifiés par l'utilisateur.

## Opérations CRUD avec l'exemple de Movie

Considérons une application "Movie" démontrant des opérations CRUD (Créer, Lire, Mettre à jour, Supprimer) :

**Modèles (Models) :**

```csharp
public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    // ... autres propriétés
}
```

**Vues (Views) :**

* **`Index.cshtml` :** Liste tous les films dans un format de tableau.

  **Exemple :**

  ```html
  @model List<Movie>

  <table>
      @foreach (var movie in Model)
      {
          <tr>
              <td>@movie.Title</td>
              <td>@movie.Description</td>
              <!-- Autres colonnes... -->
          </tr>
      }
  </table>
  ```

* **`Create.cshtml` :** Fournit un formulaire pour créer un nouveau film.

  **Exemple :**

  ```html
  <form asp-action="Create" method="post">
      <input asp-for="Title" />
      <input asp-for="Description" />
      <!-- Autres champs... -->
      <button type="submit">Create</button>
  </form>
  ```

* **`Edit.cshtml` :** Affiche les détails d'un film existant pour l'édition.

  **Exemple :**

  ```html
  @model Movie

  <form asp-action="Edit" method="post">
      <input asp-for="Title" />
      <input asp-for="Description" />
      <!-- Autres champs... -->
      <button type="submit">Edit</button>
  </form>
  ```

* **`Details.cshtml` :** Affiche des informations détaillées sur un film spécifique.

  **Exemple :**

 

 ```html
  @model Movie

  <h2>@Model.Title</h2>
  <p>@Model.Description</p>
  ```

**Contrôleurs (Controllers) :**

* **`MovieController` :** Gère les demandes liées aux films, y compris :
    * **`Index` :** Récupère tous les films et les passe à `Index.cshtml`.
    * **`Create` :** Crée un nouveau film en fonction des données du formulaire et redirige vers `Index`.
    * **`Edit` :** Récupère un film par ID, l'envoie à `Edit.cshtml` et gère les mises à jour.
    * **`Delete` :** Supprime un film de la base de données et redirige vers `Index`.

- **`Program.cs` :** Tient lieu de fichier de configuration principal, incluant l'injection de dépendances, les services et les middlewares.

## Accès aux Données avec ADO.NET (Optionnel)

Bien que Entity Framework ne soit pas couvert dans cet exemple spécifique, si vous utilisez ADO.NET :

- **`IDbConnection` :** Représente une connexion à la base de données.
- **Services (par exemple, `MovieService`) :** Encapsulent la logique d'accès aux données.

  **Exemple :**

  ```csharp
  public interface IMovieService
  {
      List<Movie> GetAllMovies();
      void AddMovie(Movie movie);
      // Autres méthodes...
  }

  public class MovieService : IMovieService
  {

      // Injection de dependance de connection à la DB
      private readonly IDbConnection _connection;

      public MovieService(IDbConnection connection)
      {
          _connection = connection;
      }

      public List<Movie> GetAllMovies()
      {
          // Implémentation pour récupérer tous les films depuis la base de données
      }

      public void AddMovie(Movie movie)
      {
          // Implémentation pour ajouter un film à la base de données
      }

      // Autres méthodes...
  }
  ```

- **Injection de Dépendance :** Fournit des services aux contrôleurs. Injection de dépendance permet à une class A
à faire appel à des instances d'une classe B sans devoir l'instancier elle même, c'est à dire qu'il n'y aura pas d'instance de la classe B dans la classe A, mais plutôt un appel d'une instance generer par la classe B lorsque celle-ci est nécéssaire (exemple : lors de l'appel d'un service).

- Mais pourquoi utiliser une injection de dépendance ? Pour permettre de gérer la séparation des responsabilités, améliorer le testing, la maintenabilité et donc également sécurité. Plus en détail, la class A sera une containeur IoC
des instances de la classe B

Voir : https://learn.microsoft.com/fr-fr/dotnet/core/extensions/dependency-injection

  **Exemple :**

  ```csharp
  public void ConfigureServices(IServiceCollection services)
  {
      services.AddScoped<IMovieService, MovieService>();
  }
  ```

## Balises d'Aide (Tag Helpers) et HTML Helpers

Bien sûr ! Voici les explications de trois tag helpers et trois HTML helpers, suivies d'un exemple de mise en place d'un mapper avec explication :

### Tag Helpers

1. **`asp-controller` :** Ce tag helper est utilisé pour spécifier le contrôleur cible d'un lien ou d'une action de formulaire. Il génère automatiquement l'URL appropriée pour l'action spécifiée dans le contrôleur spécifié.

   **Exemple :**
   ```html
   <a asp-controller="Movie" asp-action="Index">Liste des Films</a>
   ```

2. **`asp-action` :** Utilisé pour spécifier l'action à exécuter dans un contrôleur spécifique. Il génère l'URL correspondante pour cette action.

   **Exemple :**
   ```html
   <form asp-controller="Movie" asp-action="Create" method="post">
       <!-- Form content -->
   </form>
   ```

3. **`asp-for` :** Ce tag helper est utilisé pour lier un élément de formulaire à une propriété du modèle. Il génère automatiquement l'attribut `name` correspondant pour que les données du formulaire soient correctement transmises au modèle lors de la soumission du formulaire.

   **Exemple :**
   ```html
   <input asp-for="Title" />
   ```

### HTML Helpers

1. **`TextBoxFor` :** Génère un élément `<input>` de type texte lié à une propriété spécifiée du modèle. Il inclut également les données de validation appropriées.

   **Exemple :**
   ```html
   @Html.TextBoxFor(m => m.Title)
   ```

2. **`ValidationMessageFor` :** Génère un message de validation pour la propriété spécifiée du modèle. Il affiche les erreurs de validation associées à cette propriété.

   **Exemple :**
   ```html
   @Html.ValidationMessageFor(m => m.Title)
   ```

3. **`DropDownListFor` :** Génère un élément `<select>` contenant une liste déroulante d'options basées sur une propriété spécifiée du modèle.

   **Exemple :**
   ```html
   @Html.DropDownListFor(m => m.Genre, new SelectList(Model.AvailableGenres))
   ```

### Exemple de Mise en Place d'un Mapper

Un mapper est utilisé pour transformer des objets d'un type à un autre. Par exemple, pour transformer un modèle de données en un modèle de vue spécifique à une vue.

Voici comment vous pouvez mettre en place un mapper dans votre application :

1. **Création de l'Interface du Mapper :**
   ```csharp
   public interface IMapper<TSource, TDestination>
   {
       TDestination Map(TSource source);
   }
   ```

2. **Implémentation du Mapper :**
   ```csharp
   public static class MovieMapper : IMapper<Movie, MovieViewModel>
   {
       public static MovieViewModel Map(this Movie movie)
       {
           return new MovieViewModel
           {
               Id = movie.Id,
               Title = movie.Title,
               Description = movie.Description,
               // Autres propriétés à mapper...
           };
       }
   }
   ```

3. **Utilisation du Mapper :**
   ```csharp
   public class MovieController : Controller
   {
       private readonly IMapper<Movie, MovieViewModel> _movieMapper;

       public MovieController(IMapper<Movie, MovieViewModel> movieMapper)
       {
           _movieMapper = movieMapper;
       }

       public IActionResult Details(int id)
       {
           MovieViewModel viewModel = _movieService.GetMovieById(id).Map();
           return View(viewModel);
       }
   }
   ```

Dans cet exemple, le `MovieMapper` prend un objet de type `Movie` et le transforme en un objet de type `MovieViewModel`, qui est ensuite utilisé dans la vue correspondante. Cela permet de séparer clairement la logique de transformation des données de la logique métier principale.


Bien sûr, voici une synthèse enrichie avec des exemples de code pour illustrer chaque concept :

## Sessions

Les sessions dans ASP.NET Core permettent de stocker des données utilisateur spécifiques pendant toute la durée de la session de l'utilisateur sur le site. Dans l'application fournie, la gestion des sessions est réalisée à l'aide de la classe `SessionManager`.

**Exemple :**

```csharp
// Utilisation de la session pour stocker l'utilisateur connecté
public class SessionManager
{
    private readonly ISession _session;

    public SessionManager(IHttpContextAccessor httpContext)
    {
        _session = httpContext.HttpContext.Session;
    }

    public User? ConnectedUser
    {
        get
        {
            return
                (string.IsNullOrEmpty(_session.GetString("connectedUser"))) ?
                null :
                JsonConvert.DeserializeObject<User>(_session.GetString("connectedUser"));
        }
        set
        {
            _session.SetString("connectedUser", JsonConvert.SerializeObject(value));
        }
    }
}
```

### Sérialisation et Désérialisation avec Newtonsoft.Json

Newtonsoft.Json est utilisé pour la sérialisation et la désérialisation des objets utilisateur. La sérialisation consiste à convertir un objet en une chaîne JSON pour le stockage ou la transmission, tandis que la désérialisation reconstruit l'objet à partir de sa représentation sérialisée.

**Exemple :**

```csharp
// Sérialisation de l'utilisateur pour stockage en session
_session.SetString("connectedUser", JsonConvert.SerializeObject(value));

// Désérialisation de l'utilisateur à partir des données de session
JsonConvert.DeserializeObject<User>(_session.GetString("connectedUser"));
```

### Autorisation Personnalisée

L'autorisation personnalisée est mise en œuvre à l'aide de filtres d'autorisation dans l'application. Ces filtres contrôlent l'accès aux actions de contrôleur en fonction de l'état de connexion de l'utilisateur.

**Exemple :**

```csharp
// Filtre d'autorisation personnalisé
public class CustomAuthorizeAttribute : TypeFilterAttribute
{
    public CustomAuthorizeAttribute() : base(typeof(AuthRequiredFilter))
    {
    }
}

public class AuthRequiredFilter : IAuthorizationFilter
{
    private readonly SessionManager _session;
    public AuthRequiredFilter(SessionManager session)
    {
        _session = session;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (_session.ConnectedUser is null)
        {
            context.Result = new RedirectToRouteResult(new { action = "Index", Controller = "Home" });
        }
    }
}
```

### Utilisation dans les Contrôleurs

Les sessions et l'autorisation personnalisée sont utilisées dans les contrôleurs pour gérer l'accès aux fonctionnalités spécifiques en fonction de l'état de connexion de l'utilisateur.

**Exemple dans UserController :**

```csharp
// Utilisation de la session pour stocker l'utilisateur connecté après connexion
public IActionResult Login(LoginForm loginForm)
{
    User user = _service.login(loginForm.Username, loginForm.Password).ToAsp();
    _session.ConnectedUser = user;
    return RedirectToAction("Index");
}
```

**Exemple dans MovieController :**

```csharp
// Utilisation de l'attribut d'autorisation personnalisé pour restreindre l'accès
[CustomAuthorize]
public IActionResult Delete(int id)
{
    _service.DeleteMovie(id);
    return RedirectToAction("Index");
}
```

Cela assure la sécurité de l'application en limitant l'accès aux fonctionnalités sensibles aux utilisateurs authentifiés, tout en facilitant la gestion de la session utilisateur et la sérialisation/désérialisation des données.


## Système d'Authentification Automatique avec ASP.NET MVC

L'authentification automatique avec ASP.NET MVC est facilitée grâce à l'utilisation de l'Identity Framework, qui permet de gérer les utilisateurs, les rôles et les autorisations. Voici les étapes principales pour mettre en place ce système :

1. **Création de la classe AppUser :** La classe `AppUser` est créée en héritant de `IdentityUser`. On ajoute des propriétés supplémentaires comme `LastName` et `FirstName`.

   **Exemple :**
   ```csharp
   public class AppUser : IdentityUser
   {
       public string LastName { get; set; }
       public string FirstName { get; set; }
   }
   ```

2. **Changement du contexte :** On met à jour le contexte de la base de données en utilisant `IdentityDbContext<AppUser>` pour prendre en compte la classe `AppUser`.

3. **Mise à jour du Builder Service :** Dans le fichier `Program.cs`, on modifie la configuration du service pour inclure `Identity` avec `AppUser`.

4. **Création des contrôleurs et vues :** On peut alors créer les contrôleurs et les vues pour gérer l'authentification, comme le formulaire de connexion.

### Async Task et Lazy Loading

- **Async Task :** L'utilisation de `async` et `Task` permet de gérer les opérations asynchrones de manière efficace, en évitant le blocage de l'interface utilisateur pendant les opérations longues. Par exemple, dans le contrôleur `UserMoviesController`, les méthodes `AddFavorite` et `Index` sont marquées comme `async Task`.

- **Lazy Loading :** Entity Framework Core prend en charge le chargement différé (lazy loading) par défaut, ce qui signifie que les données associées à une entité ne sont chargées qu'au moment où elles sont accédées pour la première fois. Cela permet d'optimiser les performances en ne chargeant que les données nécessaires.

### Controller UserMovies

Le contrôleur `UserMoviesController` gère les actions liées aux films préférés d'un utilisateur connecté. On récupère l'utilisateur à partir de la session pour ajouter ou afficher ses films favoris.

**Exemple :**

```csharp
[Authorize]
public class UserMoviesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<AppUser> _session;

    public UserMoviesController(ApplicationDbContext context, UserManager<AppUser> session)
    {
        _context = context;
        _session = session;
    }

    public async Task<IActionResult> AddFavorite(int id)
    {
        Movie movie = await _context.Movies.FindAsync(id);
        AppUser user = await _session.Users.Include(u => u.Movies).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

        if (user == null || movie == null)
        {
            return NotFound();
        }

        bool existFavorite = user.Movies.Any(m => m.Id == id);

        if (!existFavorite)
        {
            user.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Index()
    {
        AppUser user = await _session.Users.Include(u => u.Movies).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
        return View(user.Movies.ToList());
    }
}
```

### Avantages et Inconvénients de l'Utilisation Automatique d'ASP.NET MVC

**Avantages :**
- Gain de temps : La mise en place automatique de fonctionnalités comme l'authentification et la gestion des utilisateurs permet d'accélérer le développement.
- Sécurité renforcée : L'Identity Framework offre des fonctionnalités robustes pour gérer l'authentification et les autorisations, ce qui améliore la sécurité de l'application.

**Inconvénients :**
- Complexité accrue : L'automatisation peut rendre certaines parties de l'application difficiles à personnaliser ou à comprendre.
- Surcharge de code généré : L'utilisation du scaffolding peut générer un grand nombre de fichiers et de code, ce qui peut rendre l'application plus difficile à maintenir.

En résumé, l'utilisation automatique d'ASP.NET MVC, notamment avec l'Identity Framework, offre une solution rapide et sécurisée pour gérer l'authentification et les utilisateurs, mais elle peut également introduire une certaine complexité et surcharge de code. Il est important de peser les avantages et les inconvénients en fonction des besoins spécifiques de l'application.
