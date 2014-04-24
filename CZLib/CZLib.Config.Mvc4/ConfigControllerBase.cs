namespace CZLib.Config.Mvc4
{
    using System;
    using System.Web.Mvc;

    public abstract class ConfigControllerBase<T> : Controller where T : THZConfigBase<T>
    {
        public ActionResult Index(string section, bool? result)
        {
            this.ViewBag.Section = section;
            this.ViewBag.Result = result;
            var ins = this.GetIns();
            var viewName = this.GetView();
            return View(viewName,ins);
        }
        [HttpPost]
        public ActionResult Index(FormCollection form, string section)
        {
            //ViewBag.Section = section;
            var ins = this.GetIns();
            var sectionProperty = ins.GetType().GetProperty(section);
            if (sectionProperty != null)
            {
                var sectionObj = sectionProperty.GetValue(ins, null);
                if (sectionObj != null)
                {

                    var prefix = section;
                    Predicate<string> propertyFilter = propertyName => true;
                    IModelBinder binder = this.Binders.GetBinder(sectionProperty.PropertyType);

                    ModelBindingContext bindingContext = new ModelBindingContext()
                    {
                        ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => sectionObj, sectionProperty.PropertyType),
                        ModelName = prefix,
                        ModelState = this.ModelState,
                        PropertyFilter = propertyFilter,
                        ValueProvider = this.ValueProvider
                    };
                    binder.BindModel(this.ControllerContext, bindingContext);
                }
                ins.Save(ins);
                this.TellOtherServerReloadConfig();
                return this.RedirectToAction(this.GetView(), new { section = section, result = true });
            }
            return this.RedirectToAction(this.GetView(), new { section = section, result = false });
        }

        protected abstract void TellOtherServerReloadConfig();


        protected abstract T GetIns();
        protected abstract string GetView();

    }
}
