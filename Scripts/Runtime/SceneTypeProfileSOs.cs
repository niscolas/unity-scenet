using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scenet
{
    [Serializable]
    public class SceneTypeProfileSOs : IEnumerable<SceneTypeProfileSO>
    {
        [AssetList, LabelText("Scene Type Profiles"), SerializeField]
        private List<SceneTypeProfileSO> _content = new List<SceneTypeProfileSO>();

        public void Editor_Load()
        {
            _content.ForEach(s => s.Editor_Load());
        }

        public void OnLoaded()
        {
            _content.ForEach(s => s.OnLoaded());
        }

        public IEnumerator<SceneTypeProfileSO> GetEnumerator()
        {
            return _content.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}