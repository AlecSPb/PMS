using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace PMSDAL.Model
{
    /// <summary>
    /// ������
    /// </summary>
    public class MainOrder
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime? OrderDate { get; set; }
        [Required]
        public string Customer { get; set; }
        [Required]
        public string MaterialName { get; set; }//�淶��������

        public string MaterialNickName { get; set; }//�������ݵĲ���

        public string PO { get; set; }

        public string PMIWorkNumber { get; set; }//�ڲ������ţ��Զ�����

        public string Purity { get; set; }//���ȣ�Ĭ��4N5

        public string Size { get; set; }//�淶�ߴ�

        public string SizeDetails { get; set; }//�ߴ�ϸ��

        public string Quantity { get; set; }//�󲿷���Ƭ����ʱ����g����kg

        public int Priority { get; set; }//�������ȼ�

        public string SampleRequirement { get; set; }//��Ʒ����

        public string OrderState { get; set; }//����״̬����Ч��ȡ�����������ݲ�����ʵɾ����ֻ��ǣ�����ɾ��

        public DateTime? DeliveryDateExpect { get; set; }//Ԥ�Ʒ������ڣ�Ĭ�����ö����������ں��һ����

        public string ConsigneeInformation { get; set; }//�ջ�����Ϣ

        public bool? IsPlanFinished { get; set; }//�����Ƿ����

        public bool? IsDeliveryFinished { get; set; }//�����Ƿ����

        public DateTime? DeliveryDateFact { get; set; }//ʵ�ʵķ�������

        public string Remark { get; set; }

    }
}
