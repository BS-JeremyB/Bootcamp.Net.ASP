# ASP.NET MVC : Vue d'ensemble compl�te

Retour et mise en avant des �l�ments abord�s lors du cours d'ASP MVC.
Partant de l'utilisation du Mod�le Vue Controleur � l'injection de d�pendance.

## Composants et Interactions

ASP.NET MVC comprend trois composants fondamentaux :

- **Mod�les (Models) :** Repr�sentent les donn�es et la logique m�tier � l'aide de classes qui se mappent sur des tables de base de donn�es ou des sources de donn�es externes.

  **Exemple :**

  ```csharp
  public class Movie
  {
      public int Id { get; set; }
      public string Title { get; set; }
      public string Description { get; set; }
      // ... autres propri�t�s
  }
  ```

- **Vues (Views) :** Rendent les interfaces utilisateur (UIs) en fonction des donn�es du mod�le et r�pondent aux interactions de l'utilisateur. La syntaxe Razor (cshtml) fournit un langage de templating flexible.

  **Exemple :**

  ```html
  @model Movie

  <h2>@Model.Title</h2>
  <p>@Model.Description</p>
  ```

- **Contr�leurs (Controllers) :** Orchestrant le flux de l'application, g�rent les demandes des utilisateurs, r�cup�rent des donn�es du mod�le et mettent � jour les vues en cons�quence. Ils exploitent les mod�les et les vues pour offrir des exp�riences web dynamiques.

  **Exemple :**

  ```csharp
  public class MovieController : Controller
  {
      public IActionResult Index()
      {
          // Recup�ration des films via un service qui communique avec le serveur
          Movie movies = _movieService.GetAllMovies();
          return View(movies);
      }

      // Autres actions du contr�leur...
  }
  ```
Ces composants interagissent de mani�re transparente :

1. **L'utilisateur initie une requ�te :** Un utilisateur envoie une requ�te (par exemple, une URL, une soumission de formulaire) au serveur Web.
2. **Le routage mappe la requ�te :** Le syst�me de routage (bas� sur app.MapControllerRoute) interpr�te l'URL et identifie l'action de contr�leur appropri�e pour la traiter.
3. **Le contr�leur traite la requ�te :** Le contr�leur r�cup�re des donn�es du mod�le (en utilisant des services si n�cessaire), effectue toute logique m�tier n�cessaire et s�lectionne la vue appropri�e.
4. **La vue rend la r�ponse :** La vue s�lectionn�e re�oit les donn�es du mod�le du contr�leur et utilise la syntaxe Razor pour g�n�rer la r�ponse HTML envoy�e de retour au navigateur de l'utilisateur.


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

- **`app.UseRouting()` :** Active le syst�me de routage pour identifier les actions de contr�leur en fonction des URLs.

- **`app.MapControllerRoute(name, pattern)` :** D�finit les r�gles de routage. Le `name` est facultatif, et le `pattern` sp�cifie comment les URLs se mappent sur les contr�leurs et les actions (par exemple, `{controller}/{action}/{id}`).

## Les viewbags, les viewdata, les tempdata

- **ViewBag :** ViewBag est un sac fourre-tout dynamique qui permet de transmettre des donn�es du contr�leur � la vue. Il est utilis� pour envoyer des donn�es temporaires � la vue et peut �tre utilis� pour transporter des donn�es entre les actions d'un contr�leur.

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

- **ViewData :** ViewData est un dictionnaire de donn�es cl�-valeur qui est �galement utilis� pour transporter des donn�es du contr�leur � la vue. Il est similaire � ViewBag, mais il offre un typage fort.

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

- **TempData :** TempData est un dictionnaire qui stocke des donn�es de session de mani�re temporaire. Les donn�es stock�es dans TempData sont disponibles pour la requ�te suivante et sont g�n�ralement utilis�es pour passer des donn�es entre des actions d'un contr�leur.

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

- **Validations de formulaire :** Les annotations de validation de donn�es telles que `[Required]`, `[StringLength]`, `[Range]`, etc., peuvent �tre appliqu�es aux propri�t�s d'un mod�le pour valider les donn�es saisies par l'utilisateur dans un formulaire.

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

      [Range(0, 5, ErrorMessage = "La note du film doit �tre entre 0 et 5")]
      public int Score { get; set; }
  }
  ```

- **ModelState :** ModelState est une collection qui contient les r�sultats de la liaison de mod�le et de la validation de mod�le. Il est utilis� pour v�rifier si un mod�le est valide ou non et pour afficher des messages d'erreur associ�s aux propri�t�s du mod�le.

  **Exemple :**
  ```csharp
  [HttpPost]
  public IActionResult Create(MovieForm movie)
  {
      if (ModelState.IsValid)
      {
          return View(movie);
      }
      // Autre logique de cr�ation du film
  }
  ```

### D�roulement d'une Requ�te (Exemple : Edit)

Lorsqu'une requ�te est envoy�e pour �diter une ressource, telle qu'un film, voici l'ordre de passage typique :

1. **Contr�leur (Controller) :** Le contr�leur re�oit la demande HTTP et appelle la m�thode appropri�e pour g�rer cette demande. Dans notre cas, `Edit(int id)` est appel�e.

2. **Service :** Le contr�leur utilise un service pour effectuer des op�rations m�tier, telles que la r�cup�ration des d�tails du film � �diter.

3. **Mod�le (Model) :** Le service interagit avec le mod�le pour obtenir les donn�es n�cessaires, telles que les d�tails du film � �diter.

4. **Vue (View) :** Une fois que le mod�le a �t� mis � jour, le contr�leur s�lectionne la vue appropri�e pour afficher les d�tails du film � �diter.

5. **Affichage de la Vue :** La vue est rendue avec les d�tails du film � �diter, pr�ts � �tre modifi�s par l'utilisateur.

## Op�rations CRUD avec l'exemple de Movie

Consid�rons une application "Movie" d�montrant des op�rations CRUD (Cr�er, Lire, Mettre � jour, Supprimer) :

**Mod�les (Models) :**

```csharp
public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    // ... autres propri�t�s
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

* **`Create.cshtml` :** Fournit un formulaire pour cr�er un nouveau film.

  **Exemple :**

  ```html
  <form asp-action="Create" method="post">
      <input asp-for="Title" />
      <input asp-for="Description" />
      <!-- Autres champs... -->
      <button type="submit">Create</button>
  </form>
  ```

* **`Edit.cshtml` :** Affiche les d�tails d'un film existant pour l'�dition.

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

* **`Details.cshtml` :** Affiche des informations d�taill�es sur un film sp�cifique.

  **Exemple :**

 

 ```html
  @model Movie

  <h2>@Model.Title</h2>
  <p>@Model.Description</p>
  ```

**Contr�leurs (Controllers) :**

* **`MovieController` :** G�re les demandes li�es aux films, y compris :
    * **`Index` :** R�cup�re tous les films et les passe � `Index.cshtml`.
    * **`Create` :** Cr�e un nouveau film en fonction des donn�es du formulaire et redirige vers `Index`.
    * **`Edit` :** R�cup�re un film par ID, l'envoie � `Edit.cshtml` et g�re les mises � jour.
    * **`Delete` :** Supprime un film de la base de donn�es et redirige vers `Index`.

- **`Program.cs` :** Tient lieu de fichier de configuration principal, incluant l'injection de d�pendances, les services et les middlewares.

## Acc�s aux Donn�es avec ADO.NET (Optionnel)

Bien que Entity Framework ne soit pas couvert dans cet exemple sp�cifique, si vous utilisez ADO.NET :

- **`IDbConnection` :** Repr�sente une connexion � la base de donn�es.
- **Services (par exemple, `MovieService`) :** Encapsulent la logique d'acc�s aux donn�es.

  **Exemple :**

  ```csharp
  public interface IMovieService
  {
      List<Movie> GetAllMovies();
      void AddMovie(Movie movie);
      // Autres m�thodes...
  }

  public class MovieService : IMovieService
  {

      // Injection de dependance de connection � la DB
      private readonly IDbConnection _connection;

      public MovieService(IDbConnection connection)
      {
          _connection = connection;
      }

      public List<Movie> GetAllMovies()
      {
          // Impl�mentation pour r�cup�rer tous les films depuis la base de donn�es
      }

      public void AddMovie(Movie movie)
      {
          // Impl�mentation pour ajouter un film � la base de donn�es
      }

      // Autres m�thodes...
  }
  ```

- **Injection de D�pendance :** Fournit des services aux contr�leurs. Injection de d�pendance permet � une class A
� faire appel � des instances d'une classe B sans devoir l'instancier elle m�me, c'est � dire qu'il n'y aura pas d'instance de la classe B dans la classe A, mais plut�t un appel d'une instance generer par la classe B lorsque celle-ci est n�c�ssaire (exemple : lors de l'appel d'un service).

- Mais pourquoi utiliser une injection de d�pendance ? Pour permettre de g�rer la s�paration des responsabilit�s, am�liorer le testing, la maintenabilit� et donc �galement s�curit�. Plus en d�tail, la class A sera une containeur IoC
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

Bien s�r ! Voici les explications de trois tag helpers et trois HTML helpers, suivies d'un exemple de mise en place d'un mapper avec explication :

### Tag Helpers

1. **`asp-controller` :** Ce tag helper est utilis� pour sp�cifier le contr�leur cible d'un lien ou d'une action de formulaire. Il g�n�re automatiquement l'URL appropri�e pour l'action sp�cifi�e dans le contr�leur sp�cifi�.

   **Exemple :**
   ```html
   <a asp-controller="Movie" asp-action="Index">Liste des Films</a>
   ```

2. **`asp-action` :** Utilis� pour sp�cifier l'action � ex�cuter dans un contr�leur sp�cifique. Il g�n�re l'URL correspondante pour cette action.

   **Exemple :**
   ```html
   <form asp-controller="Movie" asp-action="Create" method="post">
       <!-- Form content -->
   </form>
   ```

3. **`asp-for` :** Ce tag helper est utilis� pour lier un �l�ment de formulaire � une propri�t� du mod�le. Il g�n�re automatiquement l'attribut `name` correspondant pour que les donn�es du formulaire soient correctement transmises au mod�le lors de la soumission du formulaire.

   **Exemple :**
   ```html
   <input asp-for="Title" />
   ```

### HTML Helpers

1. **`TextBoxFor` :** G�n�re un �l�ment `<input>` de type texte li� � une propri�t� sp�cifi�e du mod�le. Il inclut �galement les donn�es de validation appropri�es.

   **Exemple :**
   ```html
   @Html.TextBoxFor(m => m.Title)
   ```

2. **`ValidationMessageFor` :** G�n�re un message de validation pour la propri�t� sp�cifi�e du mod�le. Il affiche les erreurs de validation associ�es � cette propri�t�.

   **Exemple :**
   ```html
   @Html.ValidationMessageFor(m => m.Title)
   ```

3. **`DropDownListFor` :** G�n�re un �l�ment `<select>` contenant une liste d�roulante d'options bas�es sur une propri�t� sp�cifi�e du mod�le.

   **Exemple :**
   ```html
   @Html.DropDownListFor(m => m.Genre, new SelectList(Model.AvailableGenres))
   ```

### Exemple de Mise en Place d'un Mapper

Un mapper est utilis� pour transformer des objets d'un type � un autre. Par exemple, pour transformer un mod�le de donn�es en un mod�le de vue sp�cifique � une vue.

Voici comment vous pouvez mettre en place un mapper dans votre application :

1. **Cr�ation de l'Interface du Mapper :**
   ```csharp
   public interface IMapper<TSource, TDestination>
   {
       TDestination Map(TSource source);
   }
   ```

2. **Impl�mentation du Mapper :**
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
               // Autres propri�t�s � mapper...
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

Dans cet exemple, le `MovieMapper` prend un objet de type `Movie` et le transforme en un objet de type `MovieViewModel`, qui est ensuite utilis� dans la vue correspondante. Cela permet de s�parer clairement la logique de transformation des donn�es de la logique m�tier principale.


Bien s�r, voici une synth�se enrichie avec des exemples de code pour illustrer chaque concept :

## Sessions

Les sessions dans ASP.NET Core permettent de stocker des donn�es utilisateur sp�cifiques pendant toute la dur�e de la session de l'utilisateur sur le site. Dans l'application fournie, la gestion des sessions est r�alis�e � l'aide de la classe `SessionManager`.

**Exemple :**

```csharp
// Utilisation de la session pour stocker l'utilisateur connect�
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

### S�rialisation et D�s�rialisation avec Newtonsoft.Json

Newtonsoft.Json est utilis� pour la s�rialisation et la d�s�rialisation des objets utilisateur. La s�rialisation consiste � convertir un objet en une cha�ne JSON pour le stockage ou la transmission, tandis que la d�s�rialisation reconstruit l'objet � partir de sa repr�sentation s�rialis�e.

**Exemple :**

```csharp
// S�rialisation de l'utilisateur pour stockage en session
_session.SetString("connectedUser", JsonConvert.SerializeObject(value));

// D�s�rialisation de l'utilisateur � partir des donn�es de session
JsonConvert.DeserializeObject<User>(_session.GetString("connectedUser"));
```

### Autorisation Personnalis�e

L'autorisation personnalis�e est mise en �uvre � l'aide de filtres d'autorisation dans l'application. Ces filtres contr�lent l'acc�s aux actions de contr�leur en fonction de l'�tat de connexion de l'utilisateur.

**Exemple :**

```csharp
// Filtre d'autorisation personnalis�
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

### Utilisation dans les Contr�leurs

Les sessions et l'autorisation personnalis�e sont utilis�es dans les contr�leurs pour g�rer l'acc�s aux fonctionnalit�s sp�cifiques en fonction de l'�tat de connexion de l'utilisateur.

**Exemple dans UserController :**

```csharp
// Utilisation de la session pour stocker l'utilisateur connect� apr�s connexion
public IActionResult Login(LoginForm loginForm)
{
    User user = _service.login(loginForm.Username, loginForm.Password).ToAsp();
    _session.ConnectedUser = user;
    return RedirectToAction("Index");
}
```

**Exemple dans MovieController :**

```csharp
// Utilisation de l'attribut d'autorisation personnalis� pour restreindre l'acc�s
[CustomAuthorize]
public IActionResult Delete(int id)
{
    _service.DeleteMovie(id);
    return RedirectToAction("Index");
}
```

Cela assure la s�curit� de l'application en limitant l'acc�s aux fonctionnalit�s sensibles aux utilisateurs authentifi�s, tout en facilitant la gestion de la session utilisateur et la s�rialisation/d�s�rialisation des donn�es.


## Syst�me d'Authentification Automatique avec ASP.NET MVC

L'authentification automatique avec ASP.NET MVC est facilit�e gr�ce � l'utilisation de l'Identity Framework, qui permet de g�rer les utilisateurs, les r�les et les autorisations. Voici les �tapes principales pour mettre en place ce syst�me :

1. **Cr�ation de la classe AppUser :** La classe `AppUser` est cr��e en h�ritant de `IdentityUser`. On ajoute des propri�t�s suppl�mentaires comme `LastName` et `FirstName`.

   **Exemple :**
   ```csharp
   public class AppUser : IdentityUser
   {
       public string LastName { get; set; }
       public string FirstName { get; set; }
   }
   ```

2. **Changement du contexte :** On met � jour le contexte de la base de donn�es en utilisant `IdentityDbContext<AppUser>` pour prendre en compte la classe `AppUser`.

3. **Mise � jour du Builder Service :** Dans le fichier `Program.cs`, on modifie la configuration du service pour inclure `Identity` avec `AppUser`.

4. **Cr�ation des contr�leurs et vues :** On peut alors cr�er les contr�leurs et les vues pour g�rer l'authentification, comme le formulaire de connexion.

### Async Task et Lazy Loading

- **Async Task :** L'utilisation de `async` et `Task` permet de g�rer les op�rations asynchrones de mani�re efficace, en �vitant le blocage de l'interface utilisateur pendant les op�rations longues. Par exemple, dans le contr�leur `UserMoviesController`, les m�thodes `AddFavorite` et `Index` sont marqu�es comme `async Task`.

- **Lazy Loading :** Entity Framework Core prend en charge le chargement diff�r� (lazy loading) par d�faut, ce qui signifie que les donn�es associ�es � une entit� ne sont charg�es qu'au moment o� elles sont acc�d�es pour la premi�re fois. Cela permet d'optimiser les performances en ne chargeant que les donn�es n�cessaires.

### Controller UserMovies

Le contr�leur `UserMoviesController` g�re les actions li�es aux films pr�f�r�s d'un utilisateur connect�. On r�cup�re l'utilisateur � partir de la session pour ajouter ou afficher ses films favoris.

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

### Avantages et Inconv�nients de l'Utilisation Automatique d'ASP.NET MVC

**Avantages :**
- Gain de temps : La mise en place automatique de fonctionnalit�s comme l'authentification et la gestion des utilisateurs permet d'acc�l�rer le d�veloppement.
- S�curit� renforc�e : L'Identity Framework offre des fonctionnalit�s robustes pour g�rer l'authentification et les autorisations, ce qui am�liore la s�curit� de l'application.

**Inconv�nients :**
- Complexit� accrue : L'automatisation peut rendre certaines parties de l'application difficiles � personnaliser ou � comprendre.
- Surcharge de code g�n�r� : L'utilisation du scaffolding peut g�n�rer un grand nombre de fichiers et de code, ce qui peut rendre l'application plus difficile � maintenir.

En r�sum�, l'utilisation automatique d'ASP.NET MVC, notamment avec l'Identity Framework, offre une solution rapide et s�curis�e pour g�rer l'authentification et les utilisateurs, mais elle peut �galement introduire une certaine complexit� et surcharge de code. Il est important de peser les avantages et les inconv�nients en fonction des besoins sp�cifiques de l'application.
