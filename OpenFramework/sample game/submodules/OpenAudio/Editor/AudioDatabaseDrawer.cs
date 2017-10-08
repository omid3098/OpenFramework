using OpenAudio.Database;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
namespace OpenAudio
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(AudioDatabase))]
    class AudioDatabaseDrawer : Editor
    {
        AudioDatabase audioDatabase;
        private ReorderableList list;

        public void OnEnable()
        {
            audioDatabase = (target as AudioDatabase);
            list = new ReorderableList(serializedObject, serializedObject.FindProperty("audioDBItems"), true, true, true, true);
            list.drawElementCallback = ListDrawer;
            list.onAddCallback = AddItemToList;
        }

        private void AddItemToList(ReorderableList list)
        {
            audioDatabase.audioDBItems.Add(new AudioDatabaseItem());
        }

        private void ListDrawer(Rect rect, int index, bool isActive, bool isFocused)
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            const int typeWidth = 100;
            const int audioNameWidth = 120;
            Rect audioNameRect = new Rect(rect.x, rect.y, audioNameWidth, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(
                audioNameRect,
                element.FindPropertyRelative("audioName"), GUIContent.none);
            EditorGUI.PropertyField(
                new Rect(rect.x + audioNameWidth, rect.y, typeWidth, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("type"), GUIContent.none);
            DropAreaGUI(audioNameRect, index);
        }

        public override void OnInspectorGUI()
        {
            // base.DrawDefaultInspector();
            serializedObject.Update();
            DrawPathField();
            list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawPathField()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("ResourcePath"));
        }

        private void DropAreaGUI(Rect drop_area, int _index)
        {
            Event evt = Event.current;
            switch (evt.type)
            {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                    if (!drop_area.Contains(evt.mousePosition))
                        return;

                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                    if (evt.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();

                        foreach (UnityEngine.Object dragged_object in DragAndDrop.objectReferences)
                        {
                            // Do On Drag Stuff here
                            audioDatabase.audioDBItems[_index].audioName = dragged_object.name;
                        }
                    }
                    break;
            }
        }
    }
}