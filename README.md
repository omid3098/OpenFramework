# OpenFramework

![openframeworkchart](https://user-images.githubusercontent.com/6388730/31305674-30f85082-ab4c-11e7-9ccb-d25b1e73e059.png)

## About:
The idea behind this framework is to simply have a main context and some services.
 - Context: is a place to register and get services.
 - Service: anything in the game/app that offers a service. like uiService, audioService, gameService, backendService etc.

## Usage:
 - clone OpenFramework:
        ```git clone https://github.com/omid3098/OpenFramework.git```
 - [Optional] Check sample folder. MyGameContext.cs
 - Implement your own services derived from IService interface or use predefined services.
 - Make your own context derived from GameContext.
 - In your own gamecontext, register your services inside SetupGameContext method.
 - call ```Init(this);``` after registering all services. this will initialize all services one by one and pass your context as gamecontext of the services.

 ## TODO: 
  - Use AssetBundles for asset management.
  