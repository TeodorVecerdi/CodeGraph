using System.Collections.Generic;
using Scripts.Layout;
using UnityEngine;

namespace Examples.FancyScrollView._01_Basic
{
    public class Example01ScrollView : FancyScrollView<Example01CellDto>
    {
        [SerializeField]
        ScrollPositionController scrollPositionController;

        new void Awake()
        {
            base.Awake();
            scrollPositionController.OnUpdatePosition.AddListener(UpdatePosition);
        }

        public void UpdateData(List<Example01CellDto> data)
        {
            cellData = data;
            scrollPositionController.SetDataCount(cellData.Count);
            UpdateContents();
        }
    }
}
