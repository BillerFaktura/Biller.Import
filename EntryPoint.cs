using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Import_Biller
{
    public class EntryPoint : Biller.UI.Interface.IPlugIn
    {
        public EntryPoint(Biller.UI.ViewModel.MainWindowViewModel parentViewModel)
        {
            this.ParentViewModel = parentViewModel;
            internalViewModels = new List<Biller.UI.Interface.IViewModel>();
        }

        public Biller.UI.ViewModel.MainWindowViewModel ParentViewModel { get; private set; }

        public string Name
        {
            get { return "ImportModules @ Biller"; }
        }

        public string Description
        {
            get { return "Implementiert Module zum importieren von externen Daten"; }
        }

        public double Version
        {
            get { return 1.20140817; }
        }

        public void Activate()
        {
            ParentViewModel.SettingsTabViewModel.SettingsList.Add(new UI.Tab() { DataContext = this });
            ParentViewModel.UpdateManager.Register(new Biller.Core.Models.AppModel() { Title = Name, Description = Description, GuID = ((GuidAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(GuidAttribute), true)[0]).Value.ToLower(), Version = 1.20140817, UpdateSource = "https://raw.githubusercontent.com/LastElb/BillerV2/master/update.json" });
        }

        private List<Biller.UI.Interface.IViewModel> internalViewModels;
        public List<Biller.UI.Interface.IViewModel> ViewModels()
        {
            return internalViewModels;
        }
    }
}
