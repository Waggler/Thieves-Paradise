using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ////////////////////
/// 
/// FORMATTING CODE
/// 
/// ////////////////////
/// 
/// SPACING 
/// 
/// Give Regions and Classes 2 lines
/// 1 Line between General Logic
/// 
/// HEADERS
/// 
/// Utilize these for assigning variables, and summaries (detailed later) for larger explanations
/// 
/// MONOBEHAVIOURS
/// 
/// Should not include any low level logic, pass work onto dedicated voids and functions
/// 
/// FOOTERS
/// 
/// Comment // END ScriptName at the end of a script/method
/// 
/// EVENTS/CALLBACKS
/// 
/// Events have the EVENT_ prefix and CALLBACK_ prefixes, for example: EVENT_StubbingToe, CALLBACK_TomScream
/// 
/// BOOLS
/// 
/// Use the prefix "is," for example: isFlying, isDriving, etc.
/// 
/// IENUMERATORS/COROUTINES
/// 
/// Use the "I" prefix, for example: IDoingSomething
/// 
/// CONTROLLERS
/// 
/// Should only ever have one that hanldes Input/Interaction, or remains persistent through a singleton, for example: PlayerController, GameController, MouseController, etc.
/// 
/// MANAGERS
/// 
/// Passes high level logic to Handlers or Helpers, NOT persistent like a controller: EnemyManager, LevelManager
/// 
/// HELPERS
/// 
/// Helps with extra logic related to functions: MovieManager-> ProjectorHelper
/// 
/// HANDLERS
/// 
/// Deals with high level programming handed to them from managers: MovieHandler, 
/// 
/// DATA CONTAINERS
/// 
/// Empty Classes that only contain variables, doesn't extend from Monobehaviour, used to store data or serve as a template
/// 
/// REGIONS
/// 
/// Keeps code clean and helps with oprganization, keeps variables, Monobehaviours, Methods, and more seperated, made with #region and #endregion, can be given names
/// 
/// INTERFACES
/// 
/// Always uses the "I" prefix: IClickable, IInteractable
/// 
/// NAMESPACES
/// 
/// Used to handle extra logic from assembly paths
/// 
/// COMMENTS
/// 
/// GCC: Good Code Comments Itself, aka: Code when written well should be easy to read and understandable by other programmers without requiring much commenting. 
/// When making comments, keep in mind the following: 
/// /* 
/// this comments out as many lines as you need in between
/// */
/// 
/// //Comments out a single line of code
/// 
/// <summary>
/// Similar to /* */ but is retained in documentation, meant to be used for summarizing
/// </summary>
/// 
/// OTHER NOTES
/// 
/// We want to avoid as much "spaghetti code" as possible, which is why we organize and name things well, along with their related prefixes
/// This also helps us to avoid something called "blackboxing" where code is so unorganized it becomes unreadable and unfixable, making debugging a nightmare
/// Lastly we also want to avoid Monolithic scripts, which can number into the thousands of lines of code, typically at 1000 lines or more we want to consider passing off functionality,
/// however for smaller projects we want to make sure each script is related to its job, and not numbering into the thousands, but rather a few tens or hundred code
/// 
/// </summary>

public class TemplateScript : MonoBehaviour
{
    #region Variables

    //This is how you declare a header
    [Header("Variables")]

    //This is how you serialize a field, and make private things available to the inspector
    [SerializeField] private float privateFloat;

    public float examplefloat;

    #endregion


    #region Monobehaviors

    //This is where things like Start(), Update(), and Awake() will go, in order of what starts first
    private void Start()
    {
        //This is how we pass off logic from monobehaviours
        Init();
    }


    #endregion


    #region Methods

    //Here is where we place all our functions and methods, whether passed from one another or Mono's
    public void Init()
    {

    }
    //We'll likely use regions to further categorize things based on how they interact with each other

    #endregion


    #region Coroutines

    //This is where any IEnumerators would go

    #endregion


    #region Callbacks

    //When we use EVENTS_ they require a CALLBACK_, this is where those go

    #endregion
} //END TemplateScript
