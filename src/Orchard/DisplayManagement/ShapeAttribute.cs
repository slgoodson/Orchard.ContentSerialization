using System;
﻿using Orchard.DisplayManagement.Descriptors;

namespace Orchard.DisplayManagement {
    public class ShapeAttribute : Attribute {
        public ShapeAttribute() { this.BindingType = "Display"; }
    //    public ShapeAttribute(string shapeType) { this.ShapeType = shapeType; }
        public ShapeAttribute(string bindingType) { this.BindingType = bindingType; }

        public string BindingType { get; private set; }
        public string ShapeType { get; private set; }
    }
}