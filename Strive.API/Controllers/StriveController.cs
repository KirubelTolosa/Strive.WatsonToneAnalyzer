using Strive.BLL.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Strive.BLL.Service.Models;
using System.Net;
using System.Web.Http;

namespace Strive.API.Controllers
{ 
    /// <summary>
    /// Endpoints for strive user services
    /// </summary>
    [ApiController]
    [Microsoft.AspNetCore.Components.Route("[controller]")]   
    public class StriveController : ControllerBase
    {
        private readonly IStriveBLService _striveBLService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="striveBLService"></param>
        public StriveController(IStriveBLService striveBLService)
        {
            this._striveBLService = striveBLService;
        }
        // <summary>
        /// This endpoint can be used analyze the tone of a text and get a analysis records for that user
        /// </summary>
        /// <param name="statusUpdate"></param>
        /// <returns>A status update string</returns>
        ///
        [Microsoft.AspNetCore.Mvc.HttpGet("GetToneAnalysis")]
        public List<ToneAnalysisRecordBLDto> Get (int userId, string emotionDescription)
        {
            try
            {
                return _striveBLService.GetAllAnalysisResults(userId, emotionDescription);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }            
        }
    }
}
