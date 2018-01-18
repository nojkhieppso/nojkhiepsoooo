using AutoMapper;
using HomeCinema.Data.Infrastructure;
using HomeCinema.Data.Repositories;
using HomeCinema.Entities;
using HomeCinema.Web.Infrastructure.Core;
using HomeCinema.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using HomeCinema.Web.Infrastructure.Extensions;
using HomeCinema.Genecode.GeneData;
using WebMatrix.WebData;
using HomeCinema.Services;

namespace HomeCinema.Web.Controllers
{
    [RoutePrefix("api/sysunit")]
    public class Sys_UnitsController : ApiControllerBase
    {
        private readonly IEntitySys_UnitRepository<Sys_Unit> _sys_unitsRepository;
        private readonly IEntityBaseRepository<Key_Sys_Unit> _key_sys_unitsRepository;
        private readonly IEntitySys_UserRepository<Sys_User> _sys_userRepository;
        public Sys_UnitsController(IEntitySys_UnitRepository<Sys_Unit> sys_unitsRepository,
            IEntityBaseRepository<Key_Sys_Unit> key_sys_unitsRepository,
            IEntitySys_UserRepository<Sys_User> sys_userRepository,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _sys_unitsRepository = sys_unitsRepository;
            _key_sys_unitsRepository = key_sys_unitsRepository;
            _sys_userRepository = sys_userRepository;
        }

        [Route("getkey/{Key:guid}")]
        public HttpResponseMessage Get(HttpRequestMessage request, string Key)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string noti = string.Empty;
                var keysysunit = _key_sys_unitsRepository.GetAll().Where(m => m.Key == Key).ToList();
                if (keysysunit.Count > 0)
                {
                    var Sys_Unit = _sys_unitsRepository.GetSingle(keysysunit[0].UnitID);
                    if (Sys_Unit != null)
                    {
                        Domain_netschool<DomainViewModel> DomainSet = new Domain_netschool<DomainViewModel>()
                        {
                            domain = "netschool.vn",
                            hkey= keysysunit[0].Key.ToMD5(),
                            sub= keysysunit[0].Encodedomain
                        };
                        response = request.CreateResponse(HttpStatusCode.OK, DomainSet);
                    }
                    else
                    {
                        response = request.CreateResponse(HttpStatusCode.NotFound, "Key không tồn tại tiên miền");
                    }
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.NotFound, "Key hết hạn");
                }

                return response;
            });
        }

        [Route("getallkey")]
        public HttpResponseMessage Getall(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var keysysunits = _key_sys_unitsRepository.GetAll();

                IEnumerable<Key_Sys_UnitViewModel> keysysunitsVM = Mapper.Map<IEnumerable<Key_Sys_Unit>, IEnumerable<Key_Sys_UnitViewModel>>(keysysunits);

                response = request.CreateResponse(HttpStatusCode.OK, keysysunitsVM);

                return response;
            });
        }
        [Route("GenerateKey_Sys_Units")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var Data = GenerateData.GenerateKey_Sys_Units();
                try
                {
                    foreach (Key_Sys_Unit key_sys_unit in Data)
                    {
                        Sys_Unit newsys_unit = new Sys_Unit();
                        
                        newsys_unit.Website =  key_sys_unit.Key.ToMD5() + ".netschool.vn";
                        newsys_unit.ParentUnitID = 40;
                        newsys_unit.Note = key_sys_unit.Encodedomain;
                        newsys_unit.CreatedDate = DateTime.Now;
                        newsys_unit.ModifiedDate = DateTime.Now;
                        newsys_unit.ModifiedBy = 1;
                        newsys_unit.CreatedBy = 1;
                        newsys_unit.Infor = "Generate Data";
                        //add sys_unit
                        _sys_unitsRepository.Add(newsys_unit);
                        _unitOfWork.Commit();
                        key_sys_unit.UnitID = newsys_unit.UnitID;
                        //add key_sys_unit
                        _key_sys_unitsRepository.Add(key_sys_unit);

                        WebSecurity.CreateUserAndAccount(key_sys_unit.Encodedomain, "12345678");

                        //Get UserByName
                        int UserId = WebSecurity.GetUserId(key_sys_unit.Encodedomain);
                        if (UserId > 0)
                        {
                            Sys_User newsys_user = new Sys_User();
                            newsys_user.DateOfBirth = DateTime.Now;
                            newsys_user.iType = key_sys_unit.UnitID;
                            newsys_user.CreatedDate = DateTime.Now;
                            newsys_user.ModifiedDate = DateTime.Now;
                            newsys_user.ModifiedBy = 1;
                            newsys_user.CreatedBy = 1;
                            newsys_user.Infor = "Generate Data";
                            newsys_user.Role = 7;
                            newsys_user.UserID = UserId.ToString();
                            newsys_user.UserName = key_sys_unit.Encodedomain;
                            newsys_user.LoginName = key_sys_unit.Encodedomain;
                            newsys_user.LastIPAddress = null;
                            newsys_user.LastLoginDate = DateTime.Now;
                            newsys_user.Year = DateTime.Now.AddYears(1);
                            _sys_userRepository.Add(newsys_user);
                        }
                        _unitOfWork.Commit();
                    }
                    response = request.CreateResponse(HttpStatusCode.OK, "Successful");
                }
                catch (Exception ex)
                {
                    response = request.CreateResponse(HttpStatusCode.ExpectationFailed, ex);
                }

                return response;
            });
        }
    }
}
