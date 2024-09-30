using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

namespace Demo.Optimizes
{
    /// <summary>
    /// 需要手动释放的组件
    /// </summary>
    public class ManualDestory : MonoBehaviour
    {
        //这些组件都是需要手动释放的
        private Texture2D _texture;
        private Sprite _sprite;
        private Material _material;
        private PlayableGraph _graph;

        void Start()
        {
            _texture = new Texture2D(8, 8);
            _sprite = Sprite.Create(_texture, new Rect(0, 0, 8, 8), Vector2.zero);
            _material = new Material(default(Shader));
            _graph = PlayableGraph.Create();

            //注意：通过该方式获取material/mesh时是使用克隆的方式，所以需要手动销毁
            var _mesh = GetComponent<MeshFilter>().mesh;
            _material = GetComponent<Renderer>().material;
        }
        void OnDestroy()
        {
            Destroy(_texture);
            Destroy(_sprite);
            Destroy(_material);
            if (_graph.IsValid())
            {
                _graph.Destroy();
            }
        }
    }
}
