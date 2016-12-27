using PMSModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSOperate
{
    public interface IRecordVHPItem
    {
        IList<RecordVHPItem> GetByRecordVHP(RecordVHP recordVHP);
        RecordVHPItem GetByID(Guid id);
        int Add(RecordVHPItem recordVHPItem);
        int Update(RecordVHPItem recordVHPItem);
        int Disable(RecordVHPItem recordVHPItem);
    }
}
