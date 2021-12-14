using System.Collections.Generic;
using UnityAtoms.SceneMgmt;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenet
{
    public static class SceneManagerUtility
    {
#if UNITY_EDITOR
        private static readonly Dictionary<LoadSceneMode, OpenSceneMode> LoadSceneToOpenSceneModeMapping =
            new Dictionary<LoadSceneMode, OpenSceneMode>
            {
                {LoadSceneMode.Single, OpenSceneMode.Single},
                {LoadSceneMode.Additive, OpenSceneMode.Additive}
            };
#endif

        public static void LoadScenes(IEnumerable<SceneField> scenes, LoadSceneMode loadSceneMode)
        {
            foreach (SceneField scene in scenes)
            {
                LoadScene(scene, loadSceneMode);
            }
        }

        public static void LoadScene(SceneField scene, LoadSceneMode loadSceneMode)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                EditorSceneManager.OpenScene(scene.ScenePath, LoadSceneToOpenSceneModeMapping[loadSceneMode]);
                return;
            }
#endif
            SceneManager.LoadScene(scene.SceneName, loadSceneMode);
        }
    }
}