using System;
using niscolas.UnityUtils.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenet
{
    public static class ScenetManager
    {
        public static event Action<SceneProfileSO> BeforeSceneProfileLoaded;
        public static event Action<SceneProfileSO> AfterSceneProfileLoaded;

        public static bool ShouldLoadAdditiveScenes =>
            !Application.isEditor ||
            Application.isEditor && _enteredPlayMode;

        private const string ProfilePath = "MasterLoaderProfile";

        private static ScenetManagerProfileSO _profile;

        private static bool _enteredPlayMode;

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        private static void Init()
        {
            _enteredPlayMode = false;

            EditorApplication.playModeStateChanged += PlayModeStateChanged;
        }
#endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void RuntimeInit()
        {
            if (!TryFindProfile(out _profile))
            {
                return;
            }

            SceneManager.sceneLoaded += SceneManager_OnSceneLoaded;
        }

#if UNITY_EDITOR
        private static void PlayModeStateChanged(PlayModeStateChange playModeStateChange)
        {
            if (playModeStateChange == PlayModeStateChange.EnteredPlayMode)
            {
                _enteredPlayMode = true;
            }
            else
            {
                SceneManager.sceneLoaded -= SceneManager_OnSceneLoaded;
                _enteredPlayMode = false;
            }
        }

#endif

        public static bool TryFindProfile(out ScenetManagerProfileSO profile)
        {
            profile = _profile;
            if (_profile)
            {
                return true;
            }

            profile = Resources.Load<ScenetManagerProfileSO>(ProfilePath);
            return profile;
        }

        private static void SceneManager_OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (loadSceneMode == LoadSceneMode.Additive)
            {
                return;
            }

            OnSceneLoaded(scene);
        }

        private static void OnSceneLoaded(Scene scene)
        {
            if (!_profile ||
                !_profile.SceneProfiles.TryGetValue(scene, out SceneProfileSO sceneProfile))
            {
                return;
            }

            BeforeSceneProfileLoaded?.Invoke(sceneProfile);
            sceneProfile.OnLoaded();
            AfterSceneProfileLoaded?.Invoke(sceneProfile);
        }

        public static bool TryFindCurrentSceneProfile(out SceneProfileSO currentSceneProfile)
        {
            currentSceneProfile = default;

            Scene currentScene = SceneManager.GetActiveScene();

            if (!TryFindProfile(out ScenetManagerProfileSO profile) ||
                !profile.SceneProfiles.TryGetValue(
                    currentScene, out currentSceneProfile))
            {
                TheBugger.LogRealWarning(
                    $"couldn't load {nameof(SceneProfileSO)}...");
                return false;
            }

            return currentSceneProfile;
        }
    }
}