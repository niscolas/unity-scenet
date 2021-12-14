using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenet
{
    [Serializable]
    public class SceneProfileSOs : IEnumerable<SceneProfileSO>
    {
        [SerializeField]
        private List<SceneProfileSO> _content;

        public bool TryGetValue(Scene scene, out SceneProfileSO profile)
        {
            profile = _content.FirstOrDefault(currentProfile => currentProfile.Scene.SceneName == scene.name);
            return profile;
        }

        public IEnumerator<SceneProfileSO> GetEnumerator()
        {
            return _content.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
