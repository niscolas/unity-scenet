using System.Collections.Generic;
using System.Linq;
using niscolas.OdinCompositeAttributes;
using niscolas.UnityUtils.Core.Extensions;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityAtoms.SceneMgmt;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Scenet
{
    [CreateAssetMenu(
        menuName = Constants.CreateAssetMenuPrefix + "Scene Profile",
        order = Constants.CreateAssetMenuOrder)]
    public class SceneProfileSO : ScriptableObject
    {
        [SerializeField]
        private SceneFieldReference _scene;

        [SerializeField]
        private SceneFieldReference[] _additiveScenes;

        [ExtractContent, SerializeField]
        private SceneTypeProfileSOs _typeProfiles;

        [SerializeField]
        private AtomCollection _additionalData;

        [Title("Events")]
        [SerializeField]
        private UnityEvent _loaded;

        public SceneField Scene => _scene.Value;

        public AtomCollection AdditionalData => _additionalData;

        public IEnumerable<SceneTypeProfileSO> TypeProfiles => _typeProfiles;

        public void Editor_Load()
        {
            SceneManagerUtility.LoadScene(Scene, LoadSceneMode.Single);

            if (!_typeProfiles.IsNullOrEmpty())
            {
                _typeProfiles.Editor_Load();
            }

            LoadAdditiveScenes();
        }

        public void OnLoaded()
        {
            if (!_typeProfiles.IsNullOrEmpty())
            {
                _typeProfiles.OnLoaded();
            }

            if (ScenetManager.ShouldLoadAdditiveScenes)
            {
                LoadAdditiveScenes();
            }

            _loaded?.Invoke();
        }

        public void LoadAdditiveScenes()
        {
            SceneManagerUtility.LoadScenes(
                _additiveScenes.Select(additiveScene => additiveScene.Value),
                LoadSceneMode.Additive);
        }
    }
}