using BookStore.DataAccess;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.ViewModels;
using BookStore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CompanyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CompanyController(IUnitOfWork unit)
    {
        _unitOfWork = unit;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Upsert(int? id)
    {
        Company company = new();
        if(id is null || id == 0)
        {
            return View(company);
        }
        else
        {
            company = _unitOfWork.Company.GetFirstOrDefault(x=> x.Id == id);
            return View(company);
        }
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(Company obj)
    {
        if (ModelState.IsValid)
        {
            if(obj.Id == 0)
            {
                _unitOfWork.Company.Add(obj);
            }
            else
            {
                _unitOfWork.Company.Update(obj);
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        return View(obj);
    }


    #region API Calls
    [HttpGet]
    public IActionResult GetAll()
    {
        var companyList = _unitOfWork.Company.GetAll();
        return Json(new { data = companyList });
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        if (id is null)
            return Json(new { success = false });

        var obj = _unitOfWork.Company.GetFirstOrDefault(p => p.Id == id);
        _unitOfWork.Company.Remove(obj);
        _unitOfWork.Save();
        return Json(new { success = true});
    }
    #endregion
}
