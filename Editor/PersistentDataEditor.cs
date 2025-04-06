using CodeCatGames.HMPersistentData.Runtime;
using UnityEditor;
using UnityEngine;

namespace CodeCatGames.HMPersistentData.Editor
{
    public sealed class PersistentDataEditor : EditorWindow
    {
        #region Constants
        private const string MenuItem = "HMPersistentData/PersistentDataEditor";
        private const string Name = "Persistent Data Editor";
        private const float MinWidth = 256;
        private const float MaxWidth = 512;
        private const float MinHeight = 128;
        private const float MaxHeight = 256;
        private const string SerializerTypeSelectionLabel = "Serializer Type";
        private const string KeyLabel = "AES Key (Base64)";
        private const string IvLabel = "AES IV (Base64)";
        private const string FileExtensionSelectionLabel = "File Extension";
        private const string InitializeButtonText = "Initialize Persistent Data Service";
        private const string OpenPersistentDataButtonText = "Open Persistent Data Path";
        private const string DeleteAllSavedDataButtonText = "Delete All Saved Data";
        #endregion

        #region Fields
        private SerializerType _serializerType = SerializerType.Json;
        private string _fileExtension = "dat";
        private string _key;
        private string _iv;
        #endregion

        #region Core
        [MenuItem(MenuItem)]
        public static void ShowWindow()
        {
            PersistentDataEditor editor = GetWindow<PersistentDataEditor>(Name);
            
            editor.minSize = new Vector2(MinWidth, MinHeight);
            editor.maxSize = new Vector2(MaxWidth, MaxHeight);
        }
        private void OnGUI()
        {
            SerializerTypeSelection();
            
            FileExtensionSelection();

            InitializeButton();

            OpenPersistentDataButton();

            DeleteAllSavedDataButton();
        }
        #endregion

        #region Executes
        private void SerializerTypeSelection()
        {
            _serializerType = (SerializerType)EditorGUILayout.EnumPopup(SerializerTypeSelectionLabel, _serializerType);

            if (_serializerType != SerializerType.EncryptedJson)
                return;
            
            _key = EditorGUILayout.TextField(KeyLabel, _key);
            _iv = EditorGUILayout.TextField(IvLabel, _iv);
        }
        private void FileExtensionSelection() =>
            _fileExtension = EditorGUILayout.TextField(FileExtensionSelectionLabel, _fileExtension);
        private void InitializeButton()
        {
            if (!GUILayout.Button(InitializeButtonText))
                return;
            
            PersistentDataServiceUtilities.Initialize(_serializerType, _fileExtension, _key, _iv);
        }
        private static void OpenPersistentDataButton()
        {
            if (!GUILayout.Button(OpenPersistentDataButtonText))
                return;
            
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        }
        private static void DeleteAllSavedDataButton()
        {
            if (!GUILayout.Button(DeleteAllSavedDataButtonText))
                return;
            
            PersistentDataServiceUtilities.DeleteAll();
        }
        #endregion
    }
}