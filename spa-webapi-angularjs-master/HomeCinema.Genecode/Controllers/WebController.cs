using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HomeCinema.Genecode.Controllers
{
    public class WebController : Controller
    {
        // GET: WebController
        public ActionResult Controllers(string table)
        {
            
            #region[Get latest]
            string b = "public HttpResponseMessage Get(HttpRequestMessage request){return CreateHttpResponse(request, () =>{HttpResponseMessage response = null;var $s = _$sRepository.GetAll().OrderByDescending(m => m.ReleaseDate).Take(6).ToList();IEnumerable<#ViewModel> $sVM = Mapper.Map<IEnumerable<#>, IEnumerable<#ViewModel>>($s);response = request.CreateResponse<IEnumerable<#ViewModel>>(HttpStatusCode.OK, $sVM);return response;});}";
            #endregion
            #region[details/{id:int}]
            string c = "public HttpResponseMessage Get(HttpRequestMessage request, int id){return CreateHttpResponse(request, () =>{HttpResponseMessage response = null;var $ = _$sRepository.GetSingle(id);#ViewModel $VM = Mapper.Map<#, #ViewModel>($);response = request.CreateResponse<#ViewModel>(HttpStatusCode.OK, $VM);return response;});}";
            #endregion
            #region[Get {page:int=0}/{pageSize=3}/{filter?}]
            string a = "public HttpResponseMessage Get(HttpRequestMessage request, int? page, int? pageSize, string filter = null)";
            a += "{";
            a += "int currentPage = page.Value;";
            a += "int currentPageSize = pageSize.Value;";

            a += "return CreateHttpResponse(request, () =>";
            a += "{";
            a += "HttpResponseMessage response = null;";
            a += "List<#> $s = null;";
            a += "int total#s = new int();";
            a += "if (!string.IsNullOrEmpty(filter))";
            a += "{";
            a += "$s = _$sRepository";
            a += ".FindBy(m => m.Title.ToLower()";
            a += ".Contains(filter.ToLower().Trim()))";
            a += ".OrderBy(m => m.ID)";
            a += ".Skip(currentPage * currentPageSize)";
            a += ".Take(currentPageSize)";
            a += ".ToList();";
            a += "total#s = _$sRepository";
            a += ".FindBy(m => m.Title.ToLower()";
            a += ".Contains(filter.ToLower().Trim()))";
            a += ".Count();";
            a += "}else{$s = _$sRepository.GetAll().OrderBy(m => m.ID).Skip(currentPage * currentPageSize).Take(currentPageSize).ToList();";
            a += "total#s = _$sRepository.GetAll().Count();}";
            a += "IEnumerable<#ViewModel> $sVM = Mapper.Map<IEnumerable<#>, IEnumerable<#ViewModel>>($s);";
            a += "PaginationSet<#ViewModel> pagedSet = new PaginationSet<#ViewModel>(){Page = currentPage,TotalCount = total#s,TotalPages = (int)Math.Ceiling((decimal)total#s / currentPageSize),Items = $sVM};response = request.CreateResponse<PaginationSet<#ViewModel>>(HttpStatusCode.OK, pagedSet);";
            a += "return response;});}";


            #endregion
            #region[add]
            string d = "public HttpResponseMessage Add(HttpRequestMessage request, #ViewModel $){return CreateHttpResponse(request, () =>{HttpResponseMessage response = null;if (!ModelState.IsValid){response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);}else{# new# = new #();new#.Update#($);for (int i = 0; i < $.NumberOfStocks; i++){Stock stock = new Stock(){IsAvailable = true,# = new#,UniqueKey = Guid.NewGuid()};new#.Stocks.Add(stock);}_$sRepository.Add(new#);_unitOfWork.Commit(); view model $ = Mapper.Map<#, #ViewModel>(new#);response = request.CreateResponse<#ViewModel>(HttpStatusCode.Created, $);}return response;});}"; 
            #endregion
            #region[update]
            string e = "public HttpResponseMessage Update(HttpRequestMessage request, #ViewModel $){return CreateHttpResponse(request, () =>{HttpResponseMessage response = null;if (!ModelState.IsValid){response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);}else{var $Db = _$sRepository.GetSingle($.ID);if ($Db == null)response = request.CreateErrorResponse(HttpStatusCode.NotFound, \"Invalid $.\");else{$Db.Update#($);$.Image = $Db.Image;_$sRepository.Edit($Db);_unitOfWork.Commit();response = request.CreateResponse<#ViewModel>(HttpStatusCode.OK, $);}}return response;});}";
            #endregion

            #region[images/upload]
            string f = " public HttpResponseMessage Post(HttpRequestMessage request, int $Id){return CreateHttpResponse(request, () =>{HttpResponseMessage response = null;var $Old = _$sRepository.GetSingle($Id);if ($Old == null)response = request.CreateErrorResponse(HttpStatusCode.NotFound, \"Invalid $.\");else{var uploadPath = HttpContext.Current.Server.MapPath(\"~/Content/images/$s\");var multipartFormDataStreamProvider = new UploadMultipartFormProvider(uploadPath);Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);string _localFileName = multipartFormDataStreamProvider.FileData.Select(multiPartData => multiPartData.LocalFileName).FirstOrDefault();FileUploadResult fileUploadResult = new FileUploadResult{LocalFilePath = _localFileName,FileName = Path.GetFileName(_localFileName),FileLength = new FileInfo(_localFileName).Length};$Old.Image = fileUploadResult.FileName;_$sRepository.Edit($Old);_unitOfWork.Commit();response = request.CreateResponse(HttpStatusCode.OK, fileUploadResult);}return response;});}";
            #endregion
            if (table != "" && table != null)
            {
                string lower = table.ToLower();
                string uper = char.ToUpper(table[0]) + table.Substring(1);

                a = a.Replace("$", lower).Replace("#", uper);
                b = b.Replace("$", lower).Replace("#", uper);
                c = c.Replace("$", lower).Replace("#", uper);
                d = d.Replace("$", lower).Replace("#", uper);
                e = e.Replace("$", lower).Replace("#", uper);
                f = f.Replace("$", lower).Replace("#", uper);
                ViewData["Controllersr"] = b + "\n \n \n \n" + c + "\n \n \n \n" + a + "\n \n \n \n" + d + "\n \n \n \n" + e + "\n \n \n \n" + f;
            }
            
            return View();
        }


        
    }
}