﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 制粉记录
    /// </summary>
    public class RecordMillingHistory
    {
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string State { get; set; }
        //需要记录的信息
        public string Composition { get; set; }
        public string VHPPlanLot { get; set; }
        public string MaterialSource { get; set; }//MaterialSource
        public string Remark { get; set; }
        public string MillingTool { get; set; }
        public string GasProtection { get; set; }
        public double WeightIn { get; set; }
        public double WeightOut { get; set; }
        public double WeightRemain { get; set; }
        public double Ratio { get; set; }
        public string MillingTime { get; set; }
        //操作者和操作时间
        public string Operator { get; set; }
        public string OperateTime { get; set; }
    }
}