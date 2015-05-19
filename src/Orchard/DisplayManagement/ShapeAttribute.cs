using System;
﻿using Orchard.DisplayManagement.Descriptors;

namespace Orchard.DisplayManagement {
    public class ShapeAttribute : Attribute {
        public ShapeAttribute() { }
        public ShapeAttribute(string shapeType) { this.ShapeType = shapeType; }
        public ShapeAttribute(BindingAction bindingAction) { this.BindingAction = bindingAction; }

        public BindingAction BindingAction { get; private set; }
        public string ShapeType { get; private set; }
    }
}