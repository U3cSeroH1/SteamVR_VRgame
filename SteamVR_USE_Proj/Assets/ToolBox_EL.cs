using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolBox_EL
{

    public static class ToolBox_EL
    {

        //追加コード
        /// <summary>
        /// 自分自身を”含まない”すべての子オブジェクトのレイヤーを設定します
        /// </summary>
        public static void SetLayerRecursively(
            GameObject self,
            int layer
        )
        {
            self.layer = layer;

            foreach (Transform n in self.transform)
            {
                SetLayerRecursively(n.gameObject, layer);
            }
        }

    }
}
