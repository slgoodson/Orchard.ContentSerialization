using System.Web.Mvc;
﻿using Orchard.DisplayManagement.Descriptors;

namespace Orchard.DisplayManagement.Implementation {
    public class DisplayContext  {
        public DisplayHelper Display { get; set; }
        public ViewContext ViewContext { get; set; }
        public IViewDataContainer ViewDataContainer { get; set; }
        public BindingAction BindingAction { get; set; }
        public object Value { get; set; }
    }
}
