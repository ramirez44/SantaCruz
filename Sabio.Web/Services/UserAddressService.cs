using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sabio.Data;
using System.Data;
using System.Data.SqlClient;
using Sabio.Web.Models.Requests;
using Sabio.Web.Domain;

namespace Sabio.Web.Services
{
    public class UserAddressService : BaseService
    {
        public static Guid InsertAppUsersAddress(UserAddressModelRequest model, string currentUserId)
        {
            int appuserid = UserService.GetAppUserId(currentUserId);
            Guid uid = Guid.Empty;//000-0000-0000-0000

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppUserAddresses_Create"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@AppUserId", appuserid); //This needs to be updated to retrieve session ID
                   paramCollection.AddWithValue("@Address1", model.Address1);
                   paramCollection.AddWithValue("@Address2", model.Address2);
                   paramCollection.AddWithValue("@City", model.City);
                   paramCollection.AddWithValue("@State", model.State);
                   paramCollection.AddWithValue("@Zip", model.Zip);


                   SqlParameter userAddressUId = new SqlParameter("@UId", System.Data.SqlDbType.UniqueIdentifier);
                   userAddressUId.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(userAddressUId);

               }, returnParameters: delegate(SqlParameterCollection param)
               {
                   Guid.TryParse(param["@Uid"].Value.ToString(), out uid);
               }
               );


            return uid;
        }


        public static List<UserAddress> SelectUserAddresses(string currentUserId)
        {
            int appuserid = UserService.GetAppUserId(currentUserId);
            List<UserAddress> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.AppUserAddresses_Select"
                , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@AppUserId", appuserid);
                }
               , map: delegate(IDataReader reader, short set)
                {
                    UserAddress p = new UserAddress();
                    int startingIndex = 0;


                    p.Address1 = reader.GetSafeString(startingIndex++);
                    p.Address2 = reader.GetSafeString(startingIndex++);
                    p.City = reader.GetSafeString(startingIndex++);
                    p.State = new StateDomainModel();
                    //instantiate properties of state domain model
                    p.Zip = reader.GetSafeString(startingIndex++);
                    p.State.StateProvinceId = reader.GetInt32(startingIndex++);
                    p.State.StateProvinceCode = reader.GetString(startingIndex++);
                    p.State.CountryRegionCode = reader.GetString(startingIndex++);
                    p.State.Name = reader.GetString(startingIndex++);
                    p.UId = reader.GetGuid(startingIndex++);

                    if (list == null)
                    {
                        list = new List<UserAddress>();
                    }

                    list.Add(p);

                });

            return list;
        }

        public static UserAddress SelectSingleUserAddress(Guid addressGuid)
        {
            List<UserAddress> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.AppUserAddresses_SelectByUid"
                , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@UId", addressGuid);
                }
               , map: delegate(IDataReader reader, short set)
               {
                   UserAddress p = new UserAddress();
                   int startingIndex = 0;

                   p.UId = reader.GetGuid(startingIndex++);
                   p.Address1 = reader.GetSafeString(startingIndex++);
                   p.Address2 = reader.GetSafeString(startingIndex++);
                   p.Zip = reader.GetSafeString(startingIndex++);
                   p.City = reader.GetSafeString(startingIndex++);
                   p.State = new StateDomainModel();
                   //instantiate properties of state domain model
                   p.State.StateProvinceId = reader.GetInt32(startingIndex++);
                   p.State.StateProvinceCode = reader.GetString(startingIndex++);
                   p.State.CountryRegionCode = reader.GetString(startingIndex++);
                   p.State.Name = reader.GetString(startingIndex++);


                   //                 [UId]
                   //, [Address1]
                   //, [Address2]
                   //, [City]
                   //, [Zip]
                   // [State]

                   if (list == null)
                   {
                       list = new List<UserAddress>();
                   }

                   list.Add(p);

               });

            if (list.Count > 0)
            {
                return list.First();
            }

            return null;
        }

        public static void UpdateAppUsersAddress(UserAddressUpdateRequest model, Guid addressGuid)
        {

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppUserAddresses_Update"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Address1", model.Address1);
                   paramCollection.AddWithValue("@Address2", model.Address2);
                   paramCollection.AddWithValue("@City", model.City);
                   paramCollection.AddWithValue("@State", model.State);
                   paramCollection.AddWithValue("@Zip", model.Zip);
                   paramCollection.AddWithValue("@UId", addressGuid);

               }, returnParameters: delegate(SqlParameterCollection param)
               {
                   //empty space. Use if I decide to pass through another parameter for OUTPUT if Stored Proc needs to return something. 
               }
               );
        }
    }
}