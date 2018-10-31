using POLiftWcfWebRole.ConfigurationHandlers;
using POLiftWcfWebRole.Database;
using POLiftWcfWebRole.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;

namespace POLiftWcfWebRole
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.

#if DEBUG
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, 
        IncludeExceptionDetailInFaults = true)]
#else
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, 
        IncludeExceptionDetailInFaults = false)]
#endif
    public class Service : IService
    {
        DatabaseContext _DatabaseContext;
        public DatabaseContext DatabaseContext {
            get
            {
                return _DatabaseContext ??
                    (_DatabaseContext = new DatabaseContext("POLiftDatabase"));
            }
            set
            {
                _DatabaseContext = value;
            }
        }

        public Service()
        {

        }

        public Service(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        LiftingProgram[] _LiftingPrograms = null;

        public LiftingProgram[] GetAllLiftingPrograms()
        {
            return _LiftingPrograms ??
                (_LiftingPrograms = GetLiftingProgramsFromConfig());
        }

        LiftingProgram[] GetLiftingProgramsFromConfig()
        {
            object section = ConfigurationManager.GetSection("liftingPrograms");
            LiftingProgramSection lpsec = (LiftingProgramSection)section;
            return lpsec.LiftingPrograms.Cast<LiftingProgramElement>()
                .Select(lp => lp.ToLiftingProgram()).ToArray();
        }

        TimeSpan TrialPeriod = TimeSpan.FromDays(14);

        public TimeSpan TimeLeftInTrialFromDateTime(DateTime time, DateTime? now = null)
        {
            DateTime nowVal = now.HasValue ? now.Value : DateTime.Now;
            TimeSpan timeSinceRegistration = nowVal - time;
            return TrialPeriod - timeSinceRegistration;
        }

        public TimeSpan TimeLeftInTrial(string deviceId)
        {
            LogRegistrationLookup(deviceId, GetClientIp());

            return TimeLeftInTrialInner(deviceId);
        }

        public TimeSpan TimeLeftInTrialInner(string deviceId)
        {
            var matches = DatabaseContext
                .DeviceRegistrations.Where(
                    dr => dr.DeviceId == deviceId).ToList();

            DeviceRegistration existingRegistration = matches.FirstOrDefault();

            if (existingRegistration == null)
            {
                existingRegistration = new DeviceRegistration(deviceId, DateTime.Now);
            }

            return TimeLeftInTrialFromDateTime(existingRegistration.Time);
        }

        public void LogRegistrationLookup(string deviceId, string ip)
        {
            RegistrationLookup lookup = new RegistrationLookup(deviceId,
                DateTime.Now, ip);
            DatabaseContext.RegistrationLookups.Add(lookup);
            DatabaseContext.SaveChanges();
        }

        string GetClientIp()
        {
            OperationContext context = OperationContext.Current;
            MessageProperties properties = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            string address = string.Empty;

            if (properties.Keys.Contains(HttpRequestMessageProperty.Name))
            {
                HttpRequestMessageProperty endpointLoadBalancer = properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
                if (endpointLoadBalancer != null && endpointLoadBalancer.Headers["X-Forwarded-For"] != null)
                    address = endpointLoadBalancer.Headers["X-Forwarded-For"];
            }
            return address;
        }
    }
}
