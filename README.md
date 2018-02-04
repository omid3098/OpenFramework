# OpenFramework

![openframeworkchart](https://user-images.githubusercontent.com/6388730/31305674-30f85082-ab4c-11e7-9ccb-d25b1e73e059.png)

## About:
The idea behind this framework is to simply have a main context and some services.
 - Context: is a place to register and get services.
 - Service: anything in the game/app that offers a service. like uiService, audioService, gameService, backendService etc.

## Usage:
 - clone OpenFramework:
        ```git clone https://github.com/omid3098/OpenFramework.git```
 - Make your own context derived from GameContext.
 - Implement your services derived from IService interface or use predefined helper services.
 - In your gamecontext, register your services inside SetupGameContext method.
 - call ```Init(this);``` after registering all services. this will initialize all services one by one and pass your context as gamecontext of the services.
 - [Optional] Check sample folder. MyGameContext.cs

 So this is the basics you need to do and you are good to go!
## Sample Context:
``` cs
    public class MyContext : GameContext
    {
        // if you need, subscribe to OnReady in your services.
        public override event OnReadyHandler OnReady;

        public override void SetupGameContext()
        {
            // Register what ever service you want and/or add/remove them as you need 😄
            Register<GameService>();
            Register<InputService>();
            Register<AsyncService>();
            Init(this);
        }

        // after all services registered and initialized successfully, OnReadyCallback will execute.
        protected override void OnReadyCallback()
        {
            // do your stuffs here or subscribe to OnReady in your services.
            if (OnReady != null)
                OnReady.Invoke();
        }
    }

```
## Sample Service Surgery: 💉
Here is how you write a simple InputService:

Interface simply inherits IService. you can add any method/property as you like.
```cs
    public interface IInputService : IService
    {

    }
```

and your concrete service inherits from your own interface

```cs

public class InputService : IInputService, IUpdatable
    {
```
these are sample events used for input service
```cs
        public delegate void TouchDelegate(Vector2 pos);
        public event TouchDelegate OnMouseDown;
        public event TouchDelegate OnMouseUp;
        public event TouchDelegate OnMouse;
```
Each IService has context so you can have access to other services and ready to check if your service is ready to use yet.
```cs
        public GameContext context { get; set; }
        public bool ready { get; set; }
```
Init will execute when you register services in your contex and call Init(this);
```cs
        public IEnumerator Init()
        {
            // do anythign sync/async here and when your service is ready set that to true.
            ready = true;
            yield return null;
        }
```
You can start and stop your services as you like.
set initial valuse or dispose them on StopService. or don't use them!
```cs
        public void StartService()
        {
        }

        public void StopService()
        {
        }
```
Each service that inherit from IUpdatable will have IUpdate method so we can have update cycle in our services.
```cs
        public void IUpdate()
        {
            if (Input.GetMouseButtonDown(0)) if (OnMouseDown != null) OnMouseDown.Invoke(Input.mousePosition);
            if (Input.GetMouseButtonUp(0)) if (OnMouseUp != null) OnMouseUp.Invoke(Input.mousePosition);
            if (Input.GetMouseButton(0)) if (OnMouse != null) OnMouse.Invoke(Input.mousePosition);
        }

```
Consider checking this benchmark test between Monobehaviour Update and IUpdate:

Task was to add two numbers in update.

Iterations: 1000  </br>
OpenFramework IUpdate   =>    0.16ms  </br>
MonoBehaviour Update     =>    0.54ms

Iterations: 5000  </br>
OpenFramework IUpdate   =>    0.76ms  </br>
MonoBehaviour Update     =>    2.34ms

# Helpers:
## AsyncService:

 - Create and ```Execute()``` Tasks!
 - ```Schedule()``` tasks to run them one by one! 

## UiService:
 - Change Window 
 - Show/Hide modals inside each window 😱
 - Use transition effects for all Ui elements.
 - Too many other features on this! read more [here](https://github.com/omid3098/OpenUi).

## SaveLoadService:
- Handle everything you save in one place. use PlayerPrefs, File.Write, EasySave, or any other asset or method you want!

## AudioService:
- Easily load and play audio files! (WIP. check [OpenAudio](https://github.com/omid3098/OpenAudio) repo)

# How far you can go with this?
 - Write custom service and send a pull request.
 - You can port any asset/lib as a service and share them as gist. ie. port LeanTouch into InputService! or Tween libs into TweenService.
 - after ~year we will have a framework 


 ## TODO: 
  - Add dependency injection.
  - Use AssetBundles for asset management.
  