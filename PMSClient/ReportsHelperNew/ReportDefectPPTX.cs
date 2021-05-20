using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.Components.CscanImageProcess;
using PMSClient.MainService;
using Spire.Presentation;
using Spire.Presentation.Drawing;

namespace PMSClient.ReportsHelperNew
{
    public class ReportDefectPPTX
    {
        public void Create(DcRecordTest model)
        {
            if (model == null) return;

            string templatePath = Path.Combine(Environment.CurrentDirectory, "StandardDocs", "PMI Non-Conformance Alert.pptx");

            string tempPath = Path.Combine(Environment.CurrentDirectory, "Temp", $"PMI Non-Conformance Alert {model.ProductID}.pptx");

            string targetPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), 
                $"PMI Non-Conformance Alert {model.ProductID}-{model.Composition}.pptx");


            try
            {
                File.Copy(templatePath, tempPath, true);

                Presentation pptx = new Presentation();
                pptx.LoadFromFile(tempPath);

                //替换文字
                string s = $"{model.ProductID}-{model.Composition}";
                ReplaceTags(pptx.Slides[0], "[maintitle]", s);
                ReplaceTags(pptx.Slides[0], "[today]", DateTime.Today.ToShortDateString());

                ReplaceTags(pptx.Slides[1], "[ProductID]", model.ProductID ?? "");
                ReplaceTags(pptx.Slides[1], "[Composition]", model.Composition ?? "");
                ReplaceTags(pptx.Slides[1], "[PO]", model.PO ?? "");
                ReplaceTags(pptx.Slides[1], "[Dimension]", model.DimensionActual ?? "");
                ReplaceTags(pptx.Slides[1], "[Weight]", model.Weight ?? "");
                ReplaceTags(pptx.Slides[1], "[Density]", model.Density ?? "");
                ReplaceTags(pptx.Slides[1], "[Roughness]", model.Roughness ?? "");
                //ReplaceTags(pptx.Slides[1], "[ACSCAN]", model.CScan??"");


                //填充超声图片
                //填充图像
                var manager = new Components.CscanImageProcess.ImageManager();
                var result = manager.GetImage(model.ProductID, ImageType.Target);

                if (result.IsFound)
                {

                    //获取第3张幻灯片
                    ISlide slide = pptx.Slides[2];

                    //添加一张新图片，用于替换指定的图片
                    IImageData image = pptx.Images.Append(Image.FromFile(result.ImagePath));

                    //遍历幻灯片中的形状
                    foreach (IShape shape in slide.Shapes)
                    {
                        //判断形状是否是图片
                        if (shape is SlidePicture)
                        {
                            //判断图片的标题
                            if (shape.AlternativeText == "AAAAAA")
                            {
                                //使用新图片替换标题为“image1”的图片
                                (shape as SlidePicture).PictureFill.Picture.EmbedImage = image;
                            }
                        }
                    }
                }


                pptx.SaveToFile(targetPath, FileFormat.Pptx2007);
                System.Diagnostics.Process.Start(targetPath);

                //PMSDialogService.Show("请到桌面查看对应的PPTX文件");
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void ReplaceTags(ISlide pSlide, string oldText, string newText)
        {
            foreach (IShape curShape in pSlide.Shapes)
            {
                if (curShape is IAutoShape)
                {
                    foreach (TextParagraph tp in (curShape as IAutoShape).TextFrame.Paragraphs)
                    {
                        if (tp.Text.Contains(oldText))
                        {
                            tp.Text = tp.Text.Replace(oldText, newText);
                        }
                    }
                }
            }
        }



    }
}
