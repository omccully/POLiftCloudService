using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using POLiftWcfWebRole.Models;
using System.IO;

namespace POLiftWcfWebRole
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        LiftingProgram[] GetAllLiftingPrograms();

        [OperationContract]
        TimeSpan TimeLeftInTrial(string deviceId);

        [OperationContract]
        Stream DownloadLiftingProgram(string fileName);
    }
}
