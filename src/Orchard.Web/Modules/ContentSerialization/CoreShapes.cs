﻿using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.DisplayManagement;
using Orchard.DisplayManagement.Descriptors;
using Orchard.DisplayManagement.Shapes;
using Orchard.Environment;
using Orchard.Localization;
using Orchard.Mvc;
using Orchard.Mvc.Html;
using Orchard.UI.Resources;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

// ReSharper disable InconsistentNaming

namespace ContentSerialization {
    public class CoreShapes : ApiShapesBase, IShapeTableProvider {
        private readonly Work<WorkContext> _workContext;
        private readonly Work<IResourceManager> _resourceManager;
        private readonly Work<IHttpContextAccessor> _httpContextAccessor;
        private readonly Work<IShapeFactory> _shapeFactory;
        private readonly Work<IContentManager> _contentManager;

        public CoreShapes(
            Work<WorkContext> workContext,
            Work<IResourceManager> resourceManager,
            Work<IHttpContextAccessor> httpContextAccessor,
            Work<IShapeFactory> shapeFactory,
            Work<IContentManager> contentManager
            ) {
            _workContext = workContext;
            _resourceManager = resourceManager;
            _httpContextAccessor = httpContextAccessor;
            _shapeFactory = shapeFactory;
            _contentManager = contentManager;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }
        public dynamic New { get { return _shapeFactory.Value; } }
        public void Discover(ShapeTableBuilder builder) {

        }

        [Shape("Translate")]
        public void Content(dynamic Display, dynamic Shape) {
            using (Display.ViewDataContainer.Model.Node("Content")) {
                if (Shape.Meta != null) {
                    Display(Shape.Meta);
                }

                Display(Shape.Header);
                Display(Shape.Content);
                if (Shape.Footer != null) {
                    Display(Shape.Footer);
                }
            }
        }

        [Shape("Translate")]
        public void ContentZone(dynamic Display, dynamic Shape) {
            foreach (var item in (IEnumerable<dynamic>)Order(Shape)) {
                Display(item);
            }
        }

        [Shape("Translate")]
        public void DocumentZone(dynamic Display, dynamic Shape) {
            foreach (var item in (IEnumerable<dynamic>)Order(Shape)) {
                Display(item);
            }
        }

        [Shape("Translate")]
        public void Pager(dynamic Shape, dynamic Display,
            int Page,
            int PageSize,
            double TotalItemCount,
            int? Quantity,
            object FirstText,
            object PreviousText,
            object NextText,
            object LastText,
            object GapText,
            string PagerId
            // parameter omitted to workaround an issue where a NullRef is thrown
            // when an anonymous object is bound to an object shape parameter
            /*object RouteValues*/) {

            var currentPage = Page;
            if (currentPage < 1)
                currentPage = 1;

            var pageSize = PageSize;
            if (pageSize < 1)
                pageSize = _workContext.Value.CurrentSite.PageSize;

            var numberOfPagesToShow = Quantity ?? 0;
            if (Quantity == null || Quantity < 0)
                numberOfPagesToShow = 7;

            var totalPageCount = (int)Math.Ceiling(TotalItemCount / pageSize);

            var pageKey = String.IsNullOrEmpty(PagerId) ? "page" : PagerId;

            using (Display.ViewDataContainer.Model.Node("Pager")) {
                Display.ViewDataContainer.Model.CurrentPage = currentPage;
                Display.ViewDataContainer.Model.TotalPageCount = totalPageCount;
                Display.ViewDataContainer.Model.TotalItemCount = TotalItemCount;
                Display.ViewDataContainer.Model.PageSize = pageSize;
                Display.ViewDataContainer.Model.PageKey = pageKey;

                if (Shape.ContentPart != null) {
                    Display.ViewDataContainer.Model.PageSizeKey = "pageSize" + Shape.ContentPart.Record.PagerSuffix;
                }
            }
        }

        [Shape("Translate")]
        public void Parts_Common_Metadata(dynamic Display, dynamic Shape) {
            string DisplayUrl = null;
            if (((IContent)Shape.ContentPart).Is<IAliasAspect>()) {
                UrlHelper urlHelper = new UrlHelper(Display.ViewContext.RequestContext);

                DisplayUrl = urlHelper.ItemDisplayUrl((IContent)Shape.ContentPart.ContentItem);
            }

            Display.ViewDataContainer.Model.Id = Shape.ContentPart.Id;
            Display.ViewDataContainer.Model.DisplayUrl = DisplayUrl;
            Display.ViewDataContainer.Model.CreatedUtc = Shape.ContentPart.CreatedUtc;
            Display.ViewDataContainer.Model.PublishedUtc = Shape.ContentPart.PublishedUtc;
        }

        [Shape("Translate")]
        public void Parts_Common_Metadata_Summary(dynamic Display, dynamic Shape) {
            string DisplayUrl = null;
            if (((IContent)Shape.ContentPart).Is<IAliasAspect>()) {
                UrlHelper urlHelper = new UrlHelper(Display.ViewContext.RequestContext);

                DisplayUrl = urlHelper.ItemDisplayUrl((IContent)Shape.ContentPart.ContentItem);
            }

            Display.ViewDataContainer.Model.Id = Shape.ContentPart.Id;
            Display.ViewDataContainer.Model.DisplayUrl = DisplayUrl;
            Display.ViewDataContainer.Model.CreatedUtc = Shape.ContentPart.CreatedUtc;
            Display.ViewDataContainer.Model.PublishedUtc = Shape.ContentPart.PublishedUtc;
        }

        [Shape("Translate")]
        public void Parts_Title(dynamic Display, dynamic Shape) {
            Display.ViewDataContainer.Model.Title = Shape.Title;
        }

        [Shape("Translate")]
        public void Parts_Title_Summary(dynamic Display, dynamic Shape) {
            Display.ViewDataContainer.Model.Title = Shape.Title;
        }

        [Shape("Translate")]
        public void Parts_Common_Body(dynamic Display, dynamic Shape) {
            Display.ViewDataContainer.Model.Body = Shape.Html.ToString();
        }

        [Shape("Translate")]
        public void Parts_Common_Body_Summary(dynamic Display, dynamic Shape) {
            Display.ViewDataContainer.Model.Body = Shape.Html.ToString();
        }


        [Shape("Translate")]
        public void Parts_Contents_Publish(dynamic Display, dynamic Shape) {

        }

        [Shape("Translate")]
        public void List(dynamic Display, dynamic Shape) {
            using (Display.ViewDataContainer.Model.List(Shape.Name ?? "Items")) {
                foreach (var item in ((IEnumerable<dynamic>)Shape.Items)) {
                    Display(item);
                }
            }
        }

        [Shape("Translate")]
        public void Parts_Container_Contained(dynamic Display, dynamic Shape) {
            Shape.List.Name = "ChildItems";
            Display(Shape.List);
        }
    }
}
