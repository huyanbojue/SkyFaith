﻿using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.Utils.MVVM;
using DPDTrack.ModelView;
using DevExpress.Utils.MVVM.Services;

namespace DPDTrack.Views
{
    public partial class CheckPointServerMgrView : XtraUserControl
    {
        public CheckPointServerMgrView()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                InitBindings();
            }
        }

        private void InitBindings()
        {
            mvvmContext1.ViewModelType = typeof(CheckPointServerMgrViewModel);
            var fluentAPI = mvvmContext1.OfType<CheckPointServerMgrViewModel>();
            MVVMContext.RegisterMessageBoxService();
            mvvmContext1.RegisterService(SplashScreenService.Create(splashScreenManager1));
            var agents = fluentAPI.ViewModel.CBXAgents.ToList();
            cbxAgentName.Properties.Items.AddRange(agents);

            fluentAPI.SetBinding(cbxAgentName, cbx => cbx.EditValue, x => x.SelectedAgent);
            fluentAPI.SetBinding(serverLogdate, date => date.EditValue, x => x.ServerLogDate);

            fluentAPI.SetBinding(gridControl1, gc => gc.DataSource, x => x.Logs);

            fluentAPI.BindCommand(btnRefresh, x => x.GetServerLogs());

            fluentAPI.SetBinding(lblServerStatus, lbl =>lbl.Text, x => x.ServerStatus);
            fluentAPI.SetBinding(lblServerStatus, lbl =>lbl.BackColor, x => x.ServerStatusBackColor);

            fluentAPI.SetBinding(btnStartServer, btn =>btn.Text, x => x.BtnStartServerText);
            fluentAPI.BindCommand(btnStartServer, x => x.UpdateServerStatus());
        }

        void OnDisposing()
        {
            var context = MVVMContext.FromControl(this);
            if (context != null) context.Dispose();
        }


    }
}
