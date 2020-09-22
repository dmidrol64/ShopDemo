using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcCore.Data;
using MvcCore.Models;

namespace MvcCore.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class ProductTypesController : Controller
    {
        // 2. Создаём приватную и доступную только для чтения переменную для взаимодействия с базой данных
        private readonly ApplicationDbContext _db;

        // 1. Объявляем конструктор класса по умолчанию
        public ProductTypesController(ApplicationDbContext db)
        {
            // 3. Заполняем данными приватную переменную
            _db = db;
        }


        public IActionResult Index()
        {
            // Задача - получить из базы данных и вернуть в представление все типы продуктов

            
            return View(_db.ProductTypes.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductType productType)
        {
            // 1. Проверить модель на валидность
            if (ModelState.IsValid)
            {
                // 1.1 Если модель валидная, то добавляем данные модели в сущности Entity Framework
                _db.Add(productType);

                // 1.2 Сохраняем все данные в базу
                await _db.SaveChangesAsync();

                // 1.3 Добавить сообщение о успешном добавлении
                TempData["SM"] = $"Product type: {productType.Name} added successful!";

                // 1.4 Переадресовываем на страницу вывода всех типов продуктов (Index)
                return RedirectToAction(nameof(Index));
            }

            // 2. Если модель не валидная, возвращаем текущее представление (Create) с моделью для исправление ошибок
            return View(productType);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();

            }
            var edit = await _db.ProductTypes.FindAsync(id);

            if(edit == null)
            {
                return NotFound();
            }
            return View(edit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(ProductType productType, int? id)
        {
            if(productType.Id != id)
            {
                return NotFound();
            }
            //var edit = _db.ProductTypes.Where(ed => ed.Id == id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                _db.Update(productType);

                await _db.SaveChangesAsync();

                TempData["SM"] = $"Product type: {productType.Name} Updated successful!";

                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }
    }
}
