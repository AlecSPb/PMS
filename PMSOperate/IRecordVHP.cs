using PMSModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSIRepository
{
    public interface IRecordVHP
    {
        IList<RecordVHP> GetAll();
        IList<RecordVHP> GetBySearch();
        RecordVHP GetByID(Guid id);
        int Add(RecordVHP recordVHP);
        int Update(RecordVHP recordVHP);
        int Disable(RecordVHP recordVHP);
    }
}
