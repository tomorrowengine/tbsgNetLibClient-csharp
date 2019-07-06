﻿using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetLib.WebAPI
{
    public static class Data
    {
        public enum DataRequestType
        {
            GameData,
            UserDeck
        }
        public static JObject Request(DataRequestType dataRequestType, Session session,uint profileId = 0)
        {
            var requestData = new List<RequestData>();
            var service = Service.None;
            if (dataRequestType == DataRequestType.GameData) {
                requestData.Add(new RequestData() { Key = "projectId", Value = APIRequest.ProjectId.ToString() });
                requestData.Add(new RequestData() { Key = "token", Value = session.Token });
                service = Service.Data;
            }
            else
            {
                requestData.Add(new RequestData() { Key = "projectId", Value = APIRequest.ProjectId.ToString() });
                if(profileId == 0) { 
                    requestData.Add(new RequestData() { Key = "profileId", Value = session.ProfileId.ToString() });
                }
                else
                {
                    requestData.Add(new RequestData() { Key = "profileId", Value = profileId.ToString() });
                }
                requestData.Add(new RequestData() { Key = "token", Value = session.Token });
                service = Service.UserDeck;
            }

            var dataRequest = APIRequest.Request(service, WebMethod.POST, requestData);
            if (dataRequest.StatusCode == 200)
            {
                return JObject.Parse(dataRequest.Content);
            }

            return null;
        }

    }
}