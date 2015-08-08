using System.Collections.Generic;

namespace ContentSerialization {
    public class ApiShapesBase {

        public static IEnumerable<dynamic> Order(dynamic shape) {
            return Orchard.Core.Shapes.CoreShapes.Order(shape);
        }

        public void DisplayChildren(dynamic display, dynamic shape) {
            foreach (var item in shape) {
                display(item);
            }
        }
    }
}