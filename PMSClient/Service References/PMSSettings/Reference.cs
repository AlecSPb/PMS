﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace PMSClient.PMSSettings {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="PMSSettings.IPMSSettingService")]
    public interface IPMSSettingService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPMSSettingService/GetValueByKey", ReplyAction="http://tempuri.org/IPMSSettingService/GetValueByKeyResponse")]
        string GetValueByKey(string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPMSSettingService/GetValueByKey", ReplyAction="http://tempuri.org/IPMSSettingService/GetValueByKeyResponse")]
        System.Threading.Tasks.Task<string> GetValueByKeyAsync(string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPMSSettingService/AddSettings", ReplyAction="http://tempuri.org/IPMSSettingService/AddSettingsResponse")]
        void AddSettings(string key, string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPMSSettingService/AddSettings", ReplyAction="http://tempuri.org/IPMSSettingService/AddSettingsResponse")]
        System.Threading.Tasks.Task AddSettingsAsync(string key, string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPMSSettingService/UpdateSettings", ReplyAction="http://tempuri.org/IPMSSettingService/UpdateSettingsResponse")]
        void UpdateSettings(string key, string newValue);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPMSSettingService/UpdateSettings", ReplyAction="http://tempuri.org/IPMSSettingService/UpdateSettingsResponse")]
        System.Threading.Tasks.Task UpdateSettingsAsync(string key, string newValue);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPMSSettingServiceChannel : PMSClient.PMSSettings.IPMSSettingService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PMSSettingServiceClient : System.ServiceModel.ClientBase<PMSClient.PMSSettings.IPMSSettingService>, PMSClient.PMSSettings.IPMSSettingService {
        
        public PMSSettingServiceClient() {
        }
        
        public PMSSettingServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PMSSettingServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PMSSettingServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PMSSettingServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetValueByKey(string key) {
            return base.Channel.GetValueByKey(key);
        }
        
        public System.Threading.Tasks.Task<string> GetValueByKeyAsync(string key) {
            return base.Channel.GetValueByKeyAsync(key);
        }
        
        public void AddSettings(string key, string value) {
            base.Channel.AddSettings(key, value);
        }
        
        public System.Threading.Tasks.Task AddSettingsAsync(string key, string value) {
            return base.Channel.AddSettingsAsync(key, value);
        }
        
        public void UpdateSettings(string key, string newValue) {
            base.Channel.UpdateSettings(key, newValue);
        }
        
        public System.Threading.Tasks.Task UpdateSettingsAsync(string key, string newValue) {
            return base.Channel.UpdateSettingsAsync(key, newValue);
        }
    }
}
