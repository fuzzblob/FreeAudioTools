using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FullIndieAudioSound))]
public class FullIndieAudioSound_Editor : Editor
{
    FullIndieAudioSound sound;

    bool ShowDefaultInspector;
    bool clipView = true;
    bool audioView = true;
    bool spacialView = true;


    public override void OnInspectorGUI()
    {
        sound = target as FullIndieAudioSound;
        ClipDropArea();

        EditorGUILayout.BeginVertical("box");
        EditorGUI.indentLevel++;
        EditorGUILayout.Space();

        audioView = EditorGUILayout.Foldout(audioView, "Audio Settings");
        if (audioView)
        {
            sound.playOnAwake = EditorGUILayout.ToggleLeft("Play On Awake", sound.playOnAwake);
            EditorGUILayout.Space();

            sound.volume = EditorGUILayout.Slider("Main Volume (dB)", sound.volume, -80, 0);
            sound.randomVolume = EditorGUILayout.Slider("Random Volume (dB)", sound.randomVolume, -12, 0);
            EditorGUILayout.Space();

            sound.pitch = EditorGUILayout.Slider("Main Pitch (st)", sound.pitch, -24, 24);
            sound.randomPitch = EditorGUILayout.Slider("Random Pitch (st)", sound.randomPitch, 0, 12);
            EditorGUILayout.Space();

            sound.retrigger = EditorGUILayout.ToggleLeft("Retrigger Continously", sound.retrigger);
            if (sound.retrigger == true)
            {
                sound.looping = false;
                sound.triggerRate = EditorGUILayout.IntSlider("Trigger Rate (frames)", sound.triggerRate, 0, 500);
                sound.randomTriggerStart = EditorGUILayout.IntSlider("Random Trigger Reset", sound.randomTriggerStart, 0, 500);
            }
            else
            {
                sound.looping = EditorGUILayout.ToggleLeft("Sound Looping", sound.looping);
            }
            EditorGUILayout.Space();

            sound.lowPassFilter = EditorGUILayout.Slider("Low Pass Filter", sound.lowPassFilter, 10, 22000);
            sound.highPassFilter = EditorGUILayout.Slider("High Pass Filter", sound.highPassFilter, 10, 22000);
        }
        EditorGUILayout.Space();

        spacialView = EditorGUILayout.Foldout(spacialView, "Spacial Settings");
        if (spacialView)
        {
            sound.spacial = EditorGUILayout.ToggleLeft("Positional Audio", sound.spacial);
            if (sound.spacial)
            {
                sound.vr = EditorGUILayout.ToggleLeft("VR Spacialization", sound.vr);
                EditorGUILayout.Space();
                sound.rolloff = (AudioRolloffMode) EditorGUILayout.EnumPopup(sound.rolloff);
                sound.mindDistance = EditorGUILayout.Slider(sound.mindDistance, 0, 200);
                sound.maxDistance = EditorGUILayout.Slider(sound.maxDistance, 0, 200);
            }
            else { sound.vr = false; }
        }
        EditorGUILayout.Space();

        clipView = EditorGUILayout.Foldout(clipView, "Audio Clips");
        if (clipView)
        {
            for(int i = 0; i < sound.clips.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                sound.clips[i] = EditorGUILayout.ObjectField(sound.clips[i], typeof(AudioClip), false) as AudioClip;
                if(GUILayout.Button("Remove"))
                {
                    sound.clips.Remove(sound.clips[i]);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.Space();

        if (GUILayout.Button("Play Sound"))
        {
            sound.Play();
        }
        
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
    }

    public void ClipDropArea()
    {
        var e = Event.current.type;
        if (e == EventType.DragUpdated)
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
        }
        else if (e == EventType.DragPerform)
        {
            DragAndDrop.AcceptDrag();
            foreach (UnityEngine.Object draggedObject in DragAndDrop.objectReferences)
            {
                if (draggedObject is AudioClip)
                {
                    sound.clips.Add(draggedObject as AudioClip);
                }
            }
        }
        return;
    }

    /*
    EditorGUILayout.BeginHorizontal();
    EditorGUILayout.LabelField("FadeIn time: ");
    target.fadeInTime = EditorGUILayout.Slider(target.fadeInTime, 0, 10);
    EditorGUILayout.EndHorizontal(); 
         
    */
}
