using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POLiftWcfWebRole;
using POLiftWcfWebRole.Database;
using POLiftWcfWebRole.Models;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace POLiftWcfWebRoleTest
{
    [TestClass]
    public class ServiceTest
    {
        [TestMethod]
        public void GetAllLiftingPrograms()
        {
            LiftingProgram[] Expected = new LiftingProgram[]
            {
                new LiftingProgram()
                {
                    Title="3-day-per-week Push/Pull/Legs, imperial weights (US/Canada)",
                    Description="",
                    FileName="3d-imperial.db3"
                },
                new LiftingProgram()
                {
                    Title="5-day-per-week, imperial weights (US/Canada)",
                    Description="",
                    FileName="5d-imperial.db3"
                },
                new LiftingProgram()
                {
                    Title="3-day-per-week Push/Pull/Legs, metric weights",
                    Description="",
                    FileName="3d-metric.db3"
                },
                new LiftingProgram()
                {
                    Title="5-day-per-week, metric weights",
                    Description="",
                    FileName="5d-metric.db3"
                }
            };

            Service service = new Service();
            LiftingProgram[] progs = service.GetAllLiftingPrograms();

            Assert.AreEqual(4, progs.Length);

            CollectionAssert.AreEqual(Expected, progs);
        }

        [TestMethod]
        public void LogRegistrationLookup()
        {
            DatabaseContext dbContext = new DatabaseContext(
                Effort.DbConnectionFactory.CreateTransient(), true);
                
                //Effort.EntityConnectionFactory.CreateTransient()
                //= new DatabaseContext();

            Service service = new Service(dbContext);
            string deviceId = "test_device_id";
            string ip = "127.0.0.1";
            service.LogRegistrationLookup(deviceId, ip);

            List<RegistrationLookup> lookups = 
                dbContext.RegistrationLookups.ToList();

            Assert.AreEqual(1, lookups.Count);

            RegistrationLookup first = lookups.First();
            Assert.AreEqual(deviceId, first.DeviceId);
            Assert.AreEqual(ip, first.ClientAddress);
        }

        [TestMethod]
        public void TimeLeftInTrialOrStartRegistration_Existing()
        {
            string deviceId = "test_device_id";
            
            TimeSpan expected = TimeSpan.FromDays(4);

            DateTime registrationTime = DateTime.Now - TimeSpan.FromDays(10);

            DatabaseContext dbContext = new DatabaseContext(
                Effort.DbConnectionFactory.CreateTransient(), true);

            dbContext.DeviceRegistrations.Add(
                new DeviceRegistration("other_random_device", DateTime.Now));
            dbContext.DeviceRegistrations.Add(
                new DeviceRegistration(deviceId, registrationTime));
            dbContext.SaveChanges();
            Service service = new Service(dbContext);

            TimeSpan trialRemaining = service.TimeLeftInTrialOrStartRegistration(deviceId);

            Assert.IsTrue(expected - trialRemaining < TimeSpan.FromSeconds(1));
        }

        [TestMethod]
        public void TimeLeftInTrialOrStartRegistration_New()
        {
            string deviceId = "test_device_id";

            DatabaseContext dbContext = new DatabaseContext(
                Effort.DbConnectionFactory.CreateTransient(), true);

            dbContext.DeviceRegistrations.Add(
                new DeviceRegistration("other_random_device", DateTime.Now - TimeSpan.FromDays(10)));
            dbContext.SaveChanges();
            Service service = new Service(dbContext);

            TimeSpan trialRemaining = 
                service.TimeLeftInTrialOrStartRegistration(deviceId);

            Assert.IsTrue(TimeSpan.FromDays(14) - trialRemaining < TimeSpan.FromSeconds(1));
        }

        [TestMethod]
        public void TimeLeftInTrialFromDateTime()
        {
            Service service = new Service();

            TimeSpan expected = TimeSpan.FromDays(4);

            DateTime now = DateTime.Now;
            DateTime tenDaysAgo = now - TimeSpan.FromDays(10);
            TimeSpan trialRemaining = service.TimeLeftInTrialFromDateTime(tenDaysAgo, now);

            Assert.AreEqual(expected, trialRemaining);

        }

        [TestMethod]
        public void DownloadLiftingProgram()
        {
            string fileName = "5d-imperial.db3";

            Service service = new Service();
            Stream stream = 
                service.DownloadLiftingProgram(fileName);
            Assert.IsTrue(stream.Length > 10);
            System.Diagnostics.Debug.WriteLine("stream.Length" + stream.Length);
        }
    }
}
