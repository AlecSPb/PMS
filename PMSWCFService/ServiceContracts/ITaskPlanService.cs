using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;

namespace PMSWCFService.ServiceContracts
{
    public interface ITaskPlanService
    {
        List<DcTaskPlan> GetTaskPlan(string title, string person, int skip, int take);
        int GetTaskPlanCount(string title, string person);
        int AddTaskPlan(DcTaskPlan model);
        int UpdateTaskPlan(DcTaskPlan model);
        int DeleteTaskPlan(DcTaskPlan model);
    }
}