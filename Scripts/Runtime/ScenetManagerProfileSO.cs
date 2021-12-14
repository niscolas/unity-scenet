using UnityEngine;

namespace Scenet
{
    [CreateAssetMenu(
        menuName = Constants.CreateAssetMenuPrefix + "Manager Profile",
        order = Constants.CreateAssetMenuOrder)]
    public class ScenetManagerProfileSO : ScriptableObject
    {
        [SerializeField]
        private SceneProfileSOs _sceneProfiles;

        public SceneProfileSOs SceneProfiles => _sceneProfiles;
    }
}