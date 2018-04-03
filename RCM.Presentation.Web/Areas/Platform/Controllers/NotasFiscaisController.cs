﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RCM.Application.ApplicationInterfaces;
using RCM.Application.ViewModels;
using RCM.Domain.DomainNotificationHandlers;
using RCM.Domain.Models.NotaFiscalModels;
using RCM.Presentation.Web.Controllers;
using System;

namespace RCM.Presentation.Web.Areas.Platform.Controllers
{
    [Authorize]
    [Area("Platform")]
    public class NotasFiscaisController : BaseController
    {
        private readonly INotaFiscalApplicationService _notaFiscalApplicationService;

        public NotasFiscaisController(INotaFiscalApplicationService notaFiscalApplicationService, IDomainNotificationHandler domainNotificationHandler) : 
                                                                                                                                base(domainNotificationHandler)
        {
            _notaFiscalApplicationService = notaFiscalApplicationService;
        }

        public IActionResult Index(decimal? minValor, decimal? maxValor, string numeroDocumento = null)
        {
            var valorSpecification = new NotaFiscalValorSpecification(minValor, maxValor);
            var numeroDocumentoSpecification = new NotaFiscalNumeroDocumentoSpecification(numeroDocumento);

            var list = _notaFiscalApplicationService.Get(valorSpecification
                .And(numeroDocumentoSpecification)
                .ToExpression());

            return View(list);
        }

        public IActionResult Details(Guid id)
        {
            var notaFiscal = _notaFiscalApplicationService.GetById(id);
            if (notaFiscal == null)
                return NotFound();

            return View(notaFiscal);
        }

        [Authorize(Policy = "ActiveUser")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Policy = "ActiveUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NotaFiscalViewModel notaFiscal)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return View(notaFiscal);
            }

            _notaFiscalApplicationService.Add(notaFiscal);

            if (Success())
                return RedirectToAction(nameof(Index));
            else
                return View(notaFiscal);
        }

        [Authorize(Policy = "ActiveUser")]
        public IActionResult Edit(Guid id)
        {
            var notaFiscal = _notaFiscalApplicationService.GetById(id);
            if (notaFiscal == null)
                return NotFound();

            return View(notaFiscal);
        }

        [Authorize(Policy = "ActiveUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, NotaFiscalViewModel notaFiscal)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return View(notaFiscal);
            }

            _notaFiscalApplicationService.Update(notaFiscal);

            if (Success())
                return RedirectToAction(nameof(notaFiscal));
            else
                return View(notaFiscal);
        }

        [Authorize(Policy = "ActiveUser")]
        public IActionResult Delete(Guid id)
        {
            var notaFiscal = _notaFiscalApplicationService.GetById(id);
            if (notaFiscal == null)
                return NotFound();

            return View(notaFiscal);
        }

        [Authorize(Policy = "ActiveUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id, NotaFiscalViewModel notaFiscal)
        {
            _notaFiscalApplicationService.Remove(notaFiscal);

            if (Success())
                return RedirectToAction(nameof(Index));
            else
                return View(notaFiscal);
        }
    }
}
