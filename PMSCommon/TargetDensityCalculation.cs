using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsefulPackage
{
    /// <summary>
    /// 靶材密度计算类
    /// </summary>
    public static class TargetDensityCalculation
    {
        /// <summary>
        /// 阿基米德法-各种形状靶材密度
        /// </summary>
        /// <param name="w1">空气中靶材重量</param>
        /// <param name="w2">排水中靶材重量</param>
        /// <param name="theoryDensity">靶材理论密度</param>
        /// <returns>靶材密度计算结果</returns>
        public static TargetDensityResult GetDensityInArchimedesWay(double w1, double w2, double theoryDensity=5)
        {
            if (w1<=0)
            {
                throw new ArgumentException("W1 must be bigger than 0");
            }
            if (w2<=0)
            {
                throw new ArgumentException("W2 must be bigger than 0");
            }
            if (theoryDensity<=0)
            {
                throw new ArgumentException("TheoryDensity must be bigger than 0");
            }
            if (w1<=w2)
            {
                throw new ArgumentException("W1 must be bigger than W2");
            }
            TargetDensityResult result = new TargetDensityResult();
            double v = w1 - w2;
            result.CalculateDensity = w1 / v;
            result.RelativeDensity = result.CalculateDensity / theoryDensity;
            return result;
        }
        
        /// <summary>
        /// 计算法-圆形靶材密度
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static TargetDensityResult GetDensityInCalculationWay(TargetDensityCalculationItem item)
        {
            //输入异常判断


            TargetDensityResult result = new TargetDensityResult();
            double averageOD = item.OD.Average();
            double averageH = item.H.Average()-item.PaperThickness;

            double v = Math.PI * averageOD * averageOD * averageH / 1000 / 4;

            result.CalculateDensity = (item.TargetWeight-item.PaperWeight) / v;
            result.RelativeDensity = result.CalculateDensity / item.TheoryDensity;
            return result;
        }


    }
}
