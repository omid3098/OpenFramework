# OpenAudio
We provide an asset to map all audio names to audioTypes.
so when you want to play an audio file, you only need to use audioType and forget about file names and strings.
you only need to define an audio type for each new audio file you add to your project and update audio databse file.

## Usage 
- Add your audio type in **AudioType.cs**. (ie. buttonPress or menuAmbient)
- Create a new AudioDatabase in resources folder and set Resources Path for this AudioDatabase. this string represent the resource path to search for audio files. 
- Add an new database item, Drag Audio file into name field and select audio type for it.


## Commands
``` 
    // Play audio type
    AudioManager.instance.Play(AudioType audioType, bool loop);

    // Stop audio type
    AudioManager.instance.Stop(AudioType audioType);

    // Pause all audios
    AudioManager.instance.StopAll();

    // Pause audio type
    AudioManager.instance.Pause(AudioType audioType);

    // Set audio volume
    AudioManager.instance.SetVolume(AudioType audioType, float volume);

    // Set all audio volumes
    AudioManager.instance.SetVolume(float volume);

    // Mute audio manager
    AudioManager.instance.Mute();

    // UnMute audio manager
    AudioManager.instance.UnMute();


```
