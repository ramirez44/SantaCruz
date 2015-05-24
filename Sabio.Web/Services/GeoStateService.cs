using Sabio.Data;
using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services
{
    public class GeoStateService : BaseService
    {
        public static List<StateDomainModel> GetUsStates()//List<...> is a composite type bcuz its made up of multiple types
        {
            List<StateDomainModel> list = null;
            //Wrapper
            DataProvider.ExecuteCmd(GetConnection, "dbo.StateProvince_SelectUsStates"
               , inputParamMapper: null
               , map: delegate(IDataReader reader, short set)//(bottom level bun)map the rows that the database returns(suitcase within suitcase)
               {   //loops through the rows which returns one domain model (ie one row)that comes out
                   StateDomainModel usStateRowModel = new StateDomainModel();
                   int startingIndex = 0; //startingOrdinal   type var = new type

                   usStateRowModel.StateProvinceId = reader.GetInt32(startingIndex++);
                   usStateRowModel.StateProvinceCode = reader.GetString(startingIndex++);
                   usStateRowModel.CountryRegionCode = reader.GetString(startingIndex++);
                   usStateRowModel.Name = reader.GetString(startingIndex++);


                   if (list == null)
                   {
                       list = new List<StateDomainModel>();
                   }

                   list.Add(usStateRowModel);
               }
               );


            return list;
        }
    }
}