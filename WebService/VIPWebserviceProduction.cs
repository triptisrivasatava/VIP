using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.Partner;
using WebService.WSUpdateSobject;

namespace WebService
{
	class VIPWebserviceProduction
	{
		static void Main(string[] args)
		{
			//Authenticating and Creating a Session

			String strApiUrl = (String)WebService.Properties.Settings.Default.ApiUrl;//Create the Endpoint.
			String strUsername = (String)WebService.Properties.Settings.Default.Username;//Set the instance username. [Set Username and password in App.config]
			String strPassword = (String)WebService.Properties.Settings.Default.Password;//Set password with combination of password and security token.
			//SforceService sforceService = new SforceService();//Create Partner Wsdl class SforceService object.
			WebService.Partner.SforceService sforceService = new WebService.Partner.SforceService();

			System.Console.WriteLine("Processing....");

			sforceService.Url = strApiUrl;//Set the Endpoint.

			LoginResult loginResult = sforceService.login(strUsername, strPassword);//loginResult object. 

			if (loginResult != null && !loginResult.passwordExpired)
			{
				//Consuming Force.com webservice

				WSUpdateSobject.WSUpdateSObjectsService WsUpdaService = new WSUpdateSObjectsService();//Create Webservice instance.
				WSUpdateSobject.SessionHeader header = new WSUpdateSobject.SessionHeader();//Create session header instance.
				header.sessionId = loginResult.sessionId;//Set sessionId in header.
				WsUpdaService.SessionHeaderValue = header;//Set header in Webservice. 


				List<WSUpdateSobject.LoanOpprtntyDetail> lstLoanOpp = new List<WSUpdateSobject.LoanOpprtntyDetail>();//Create list of LoanOpprtntyDetail.
				WSUpdateSobject.LoanOpprtntyDetail objLoan = new WSUpdateSobject.LoanOpprtntyDetail();
				objLoan.OpportunityID = "006i000000Tr2yp";
				objLoan.CLTV = "83";
				lstLoanOpp.Add(objLoan);

				List<WSUpdateSobject.ContactDetail> lstObjcnt = new List<WSUpdateSobject.ContactDetail>();//Create list of ContactDetail.

				WSUpdateSobject.ContactDetail objcnt = new WSUpdateSobject.ContactDetail();
				objcnt.ContactID = "003i000000hzfxg";
				objcnt.BorrowerFirstName = "Milan";
				lstObjcnt.Add(objcnt);

				

				WsUpdaService.UpdtCntctNLnOpprtntyDetail(lstLoanOpp.ToArray(), lstObjcnt.ToArray());//Call the webservice method UpdtCntctNLnOpprtntyDetail.

				WebService.WSUpdateSobject.Error[] data = WsUpdaService.UpdtCntctNLnOpprtntyDetail(lstLoanOpp.ToArray(), lstObjcnt.ToArray());
				foreach (WebService.WSUpdateSobject.Error error in data)
				{
					System.Console.WriteLine("Result---." + error.Id);
					System.Console.WriteLine("Result---." + error.ErrorMessage);
					System.Console.WriteLine("-----------------------");
				}

				Console.ReadLine();
			}
		}
	}
}
