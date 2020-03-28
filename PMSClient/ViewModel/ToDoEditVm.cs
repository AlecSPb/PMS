using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.ExtraService;
using PMSClient.MainService;

namespace PMSClient.ViewModel
{
    public class ToDoEditVM : BaseViewModelEdit
    {
        public ToDoEditVM()
        {
            InitializeCommands();
            Statuses = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.ToDoStatus>(Statuses);

            Priorities = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.ToDoPriority>(Priorities);
            Types = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.ToDoType>(Types);
        }


        private void InitializeCommands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
        }


        public void SetNew()
        {
            IsNew = true;
            var model = new DcToDo();
            #region 初始化
            IsNew = true;
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.Status = PMSCommon.ToDoStatus.未完成.ToString();

            model.Title = "无";
            model.Content = "无";
            model.Priority = PMSCommon.ToDoPriority.普通.ToString();
            model.DeadLine = DateTime.Now.AddDays(7);
            model.Progress = 0;
            model.PersonInCharge = "暂无";

            model.FinishTime = DateTime.Now.AddDays(7);
            model.Remark = "";

            #endregion
            CurrentToDo = model;
        }

        public void SetEdit(DcToDo model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentToDo = model;
            }
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.ToDo);
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentToDo.Status == PMSCommon.ToDoStatus.作废.ToString())
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定要作废吗？"))
                {
                    return;
                }
            }
            try
            {
                if (CurrentToDo.Progress > 100)
                    CurrentToDo.Progress = 100;

                if (CurrentToDo.Progress == 100)
                {
                    CurrentToDo.Status = PMSCommon.ToDoStatus.最终完成.ToString();
                    CurrentToDo.FinishTime = DateTime.Now;
                }

                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                var service = new ToDoServiceClient();
                if (IsNew)
                {
                    service.AddToDo(CurrentToDo);
                }
                else
                {
                    service.UpdateToDo(CurrentToDo);
                }
                service.Close();
                PMSHelper.ViewModels.ToDo.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        public List<string> Priorities { get; set; }
        public List<string> Statuses { get; set; }
        public List<string> Types { get; set; }

        private DcToDo currentToDo;
        public DcToDo CurrentToDo
        {
            get { return currentToDo; }
            set
            {
                currentToDo = value;
                RaisePropertyChanged(nameof(CurrentToDo));
            }
        }

        public RelayCommand Select { get; set; }

    }
}
