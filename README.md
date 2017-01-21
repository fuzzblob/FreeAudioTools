# Chris Tammik's free audio tools

This is a Unity project which contains some audio tools I built and hereby release them under the MIT License.

Do download the newest versions of the scrits simply navigate to \FreeAudioTools\UnityPackage\ and download the newest file (yyyymmdd).

When opening up the project for the first time, please opene the scene main.unity and hit the play button to run the scene. The main camera has a listere attached (as per default in a new scene). Another component which was added to demonstrate the tool is the PlaySoundExample.cs script. It holds a reference to the "raindrops" prefab. When the scene is played the PlaySoundExample will load the raindrops prefab and call the Play() method on the FullIndieAudioSound component attached.

PlaySoundExample has some useful comments in its code which explain how the sound is being instanciated and played. If you set the stop variable to true, the sound will stop.

The FullIndieAudioSound is a script that allows basic control over audio settings. It is an isntance based system which means audio behaviour needs to be defined per sound effect and storen in a prefab (Unitys GameObject preset). To play a sound the prefab must be instaciated.

Screenshots:
https://cloud.githubusercontent.com/assets/35717/21075982/ff078f66-bed4-11e6-98b9-16d5709822b3.png
https://cloud.githubusercontent.com/assets/35717/21075983/ff083628-bed4-11e6-93f2-6df06f300e65.png

- Play On Awake starts the sownd as soon as the script awakes. Alternatively call the Play() method on the FullIndieAudioSound component.
- The volume is being shown in decibels in a range from 0 (no volume change) over -6dB (1/2 amplidude), -12dB (1/4 amplidude) all the way down to -80 dB (silence);
- Volume randomization ranges from zero to -12 dB
- The pitch is being shown insemitones with a range of -24 (two octave lower / 0.25x playback speed) over 0 (no change in pitch) up two +24 (two octave higher / 4x playback speed)
- pitch randomisation has a range of 0 up to +12 semitones. to achieve natural sounding results for sound effects this should notb exceed 3-4 semitones of randomisation.
- The looping option will loop whatever audio is being played.
- To stop a FullIndieAudioSound from producing sound you can call the StopAll method.
- If looping is not enabled, the sound will play once and then wait for new Play() method calls.
- Low Pass filter and High Pass filter cut treble / bass frequencies respectively. This can be used to simulate a "muffled" effect such as a sound playing from another room etc.
- posistional audio lets the Unity Audio Engine pan the sound left / right across the speakers / headphones according to its position in 3D space in relation to the audio listener (should be attached tyo the main camera by default).
- If a sacialisation plugin is selected in the Unity Project Audio Settings and posistional audio is enabled, the oprion to spacialize the sound for Virtual Reality becomes effective. This will enable the player in  to track sounds that are ouside of their field of view.
- The falloff curve sould be set to "Logarithmic" if the game is a 3D game. For 2D games it is possible that the "Linear" option may sound better. Custom should not be used.
- the Min- and Max-Distance setting define how far away from the listener the sound starts to attenuate (minimum distance for falloff to take affect) / fall to silenece (maximum audible range).
- If positional audio is diabled the sound will play through the playback speakers directly. if the Sound is a stereo sound (like most music) the left channel will come out the left speaker and vice versa. If the sound is mono it will play through both speakers equally.
- Audio Clips contains a list of all the audio files that are attached to a sound. To add clips, simply drag and drop them onto the FullIndieAudioSound inspector interface. To accomplish this with mutiple clips at onec, lock the inspector after selecting the soud prefab (little lock symbol in the top right).
