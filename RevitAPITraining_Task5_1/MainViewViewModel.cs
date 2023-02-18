using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITraining_Task5_1
{
    public class MainViewViewModel
    {
        private ExternalCommandData _commandData;



        public event EventHandler ShowRequest;
        private void RaiseShowRequest()
        {
            ShowRequest?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler HideRequest;
        private void RaiseHideRequest()
        {
            HideRequest?.Invoke(this, EventArgs.Empty);
        }


        public DelegateCommand PipesCountCommand { get; private set; }
        public DelegateCommand DorsCountCommand { get; private set; }
        public DelegateCommand VolumeAllWalsCommand { get; private set; }
        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            PipesCountCommand = new DelegateCommand(OnPipesCountCommand);
            DorsCountCommand = new DelegateCommand(OnDorsCountCommand);
            VolumeAllWalsCommand = new DelegateCommand(OnVolumeAllWalsCommand);
        }


        private void OnPipesCountCommand()
        {
            RaiseHideRequest();
            TaskUtils.DisplayPipesCount(_commandData);
            RaiseShowRequest();
        }
        private void OnDorsCountCommand()
        {
            RaiseHideRequest();
            TaskUtils.DisplayDorsCount(_commandData);
            RaiseShowRequest();
        }
        private void OnVolumeAllWalsCommand()
        {
            RaiseHideRequest();
            TaskUtils.DisplayVolumeAllWals(_commandData);
            RaiseShowRequest();
        }

    }
}
